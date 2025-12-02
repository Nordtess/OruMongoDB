using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OruMongoDB.BusinessLayer;
using OruMongoDB.Domain;
using OruMongoDB.Core.Helpers;

namespace UI
{
    public partial class MainUiForm : Form
    {
        // Services and in-memory UI state
        private readonly IPoddService _poddService;
        private readonly CategoryService _categoryService;

        private List<Poddflöden> _allSavedFeeds = new();
        private Poddflöden? _currentFlode;
        private List<PoddAvsnitt> _currentEpisodes = new();
        private List<Kategori> _allCategories = new();

        public MainUiForm()
        {
            InitializeComponent();
            // Show hint text in rename field; user types new name without erasing current name
            txtCustomName.PlaceholderText = "Enter new name";

            // Category text fields: placeholders + clear-on-deselect behavior
            txtNewCategoryName.PlaceholderText = "Enter new category name";
            txtEditCategoryName.PlaceholderText = "Enter category name to change to";
            lblNewCategoryName.Text = "New category name:";

            // Set label3 text and wire up clear-on-deselect behavior
            label3.Text = "Save as:";
            txtCustomName.Leave += ClearTextBoxOnLeave;
            textBox1.Leave += ClearTextBoxOnLeave;
            txtNewCategoryName.Leave += ClearTextBoxOnLeave;
            txtEditCategoryName.Leave += ClearTextBoxOnLeave;

            ApplyTheme();
            _poddService = ServiceFactory.CreatePoddService();
            _categoryService = ServiceFactory.CreateCategoryService();
        }

        // Form initialization: load categories and previously saved feeds
        private async void MainUiForm_Load(object? sender, EventArgs e)
        {
            try
            {
                // Ensure the selected-feed label is initialized on startup
                UpdateSelectedFeedLabel();

                await LoadCategoriesAsync();
                await LoadSavedFeedsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during startup: " + ex.Message, "Startup error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Open episode external link in the system default browser
        private void btnOpenExternalLink_Click(object? sender, EventArgs e)
        {
            if (!TryGetSelectedEpisode(out var ep)) return;
            if (string.IsNullOrWhiteSpace(ep.link))
            {
                MessageBox.Show("Episode has no link.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                Process.Start(new ProcessStartInfo { FileName = ep.link, UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open link: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateFetchedFeedLabel()
        {
            if (_currentFlode != null && !_currentFlode.IsSaved && !string.IsNullOrWhiteSpace(_currentFlode.displayName))
            {
                label2.Text = $"Fetched feed: {_currentFlode.displayName}";
                label2.Visible = true;
            }
            else
            {
                label2.Visible = false;
                label2.Text = string.Empty;
            }
        }

        // Fetch a feed either from database cache or from the internet
        private async void btnFetch_Click(object? sender, EventArgs e)
        {
            var url = txtRssUrl.Text.Trim();
            try
            {
                PoddValidator.ValidateRssUrl(url);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Reset UI state for a new load
                _currentFlode = null;
                _currentEpisodes = new List<PoddAvsnitt>();
                dgvEpisodes.Rows.Clear();
                lblEpisodeTitle.Text = "No episodes.";
                lblEpisodeCount.Text = "Episodes:0";
                txtDescription.Clear();
                UpdateSelectedFeedLabel();
                UpdateFetchedFeedLabel();
                try
                {
                    // Try read-through cache from DB first (fast path)
                    var dbResult = await _poddService.FetchFromDatabaseAsync(url);
                    _currentFlode = dbResult.poddflode;
                    _currentEpisodes = dbResult.avsnitt ?? new List<PoddAvsnitt>();
                    _currentFlode.IsSaved = true;
                    SyncFeedCategoryFromFeed(_currentFlode);
                    FillEpisodesGrid(_currentEpisodes);
                    UpdateSelectedFeedLabel();
                    UpdateFetchedFeedLabel();
                    MessageBox.Show(
                        $"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}'.",
                        "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                catch (ValidationException)
                {
                }

                // Fallback to internet fetch (slow path)
                var netResult = await _poddService.FetchPoddFeedAsync(url);
                _currentFlode = netResult.poddflode;
                _currentEpisodes = netResult.avsnitt ?? new List<PoddAvsnitt>();
                _currentFlode.IsSaved = false;
                if (cmbFeedCategory.Items.Count > 0) cmbFeedCategory.SelectedIndex = 0;
                FillEpisodesGrid(_currentEpisodes);
                UpdateSelectedFeedLabel();
                UpdateFetchedFeedLabel();
                MessageBox.Show(
                    $"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}'.",
                    "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidationException)
            {
                // Show a clean message if URL cannot be accessed (e.g.,4xx/5xx or network error)
                MessageBox.Show("Could not access the URL, please check the link and try again!", "Validation error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while fetching feed: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Persist current feed and all its episodes
        private async void btnSaveFeed_Click(object? sender, EventArgs e)
        {
            try
            {
                PoddValidator.EnsureFeedSelected(_currentFlode!);
                PoddValidator.EnsureEpisodesExist(_currentEpisodes);

                // If already saved, inform user and stop. Use Rename for name changes.
                if (_currentFlode!.IsSaved)
                {
                    MessageBox.Show("This feed is already saved.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Determine custom name for a new (unsaved) feed
                var unsavedName = textBox1.Text.Trim();
                var customName = string.IsNullOrWhiteSpace(unsavedName) ? _currentFlode.displayName : unsavedName;
                PoddValidator.ValidateFeedName(customName);

                _currentFlode!.displayName = customName;
                foreach (var ep in _currentEpisodes)
                {
                    ep.feedId = _currentFlode.Id!;
                    ep.description = HtmlCleaner.ToPlainText(ep.description);
                }
                await _poddService.SavePoddSubscriptionAsync(_currentFlode, _currentEpisodes);
                _currentFlode.IsSaved = true;
                _currentFlode.SavedAt = DateTime.UtcNow;
                UpdateSelectedFeedLabel();
                UpdateFetchedFeedLabel();
                MessageBox.Show("Feed and episodes saved.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadSavedFeedsAsync();

                // Clear inputs after successful save to keep UI clean
                textBox1.Clear();
                txtCustomName.Clear();
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async Task LoadCategoriesAsync()
        {
            _allCategories = (await _categoryService.GetAllCategoriesAsync()).ToList();
            cmbCategoryFilter.Items.Clear();
            cmbFeedCategory.Items.Clear();
            cmbCategoryEdit.Items.Clear();
            lstCategoriesRight.Items.Clear();
            cmbCategoryFilter.Items.Add("All categories");
            cmbFeedCategory.Items.Add("(no category)");
            cmbCategoryEdit.Items.Add("(select category)");
            foreach (var cat in _allCategories)
            {
                cmbCategoryFilter.Items.Add(cat);
                cmbFeedCategory.Items.Add(cat);
                cmbCategoryEdit.Items.Add(cat);
                lstCategoriesRight.Items.Add(cat);
            }
            cmbCategoryFilter.DisplayMember = "Namn";
            cmbFeedCategory.DisplayMember = "Namn";
            cmbCategoryEdit.DisplayMember = "Namn";
            lstCategoriesRight.DisplayMember = "Namn";
            if (cmbCategoryFilter.Items.Count > 0) cmbCategoryFilter.SelectedIndex = 0;
            if (cmbFeedCategory.Items.Count > 0) cmbFeedCategory.SelectedIndex = 0;
            if (cmbCategoryEdit.Items.Count > 0) cmbCategoryEdit.SelectedIndex = 0;
        }

        // Load saved feeds and apply any active category filter
        private async Task LoadSavedFeedsAsync()
        {
            _allSavedFeeds = await _poddService.GetAllSavedFeedsAsync();
            ApplyCategoryFilter();
        }

        // Filter feeds by selected category and bind to list
        private void ApplyCategoryFilter()
        {
            lstPodcasts.DataSource = null;
            lstPodcasts.Items.Clear();
            IEnumerable<Poddflöden> filtered = _allSavedFeeds;
            if (cmbCategoryFilter.SelectedItem is Kategori selectedCat)
                filtered = filtered.Where(f => f.categoryId == selectedCat.Id);
            var list = filtered.ToList();
            if (list.Count == 0)
                lstPodcasts.Items.Add("No podcasts found.");
            else
            {
                lstPodcasts.DataSource = list;
                lstPodcasts.DisplayMember = "displayName";
            }
        }

        private void cmbCategoryFilter_SelectedIndexChanged(object? sender, EventArgs e) => ApplyCategoryFilter();


        private void SyncFeedCategoryFromFeed(Poddflöden feed)
        {
            if (string.IsNullOrEmpty(feed.categoryId))
            {
                if (cmbFeedCategory.Items.Count > 0) cmbFeedCategory.SelectedIndex = 0;
                return;
            }
            var cat = _allCategories.FirstOrDefault(c => c.Id == feed.categoryId);
            cmbFeedCategory.SelectedItem = cat ?? (cmbFeedCategory.Items.Count > 0 ? cmbFeedCategory.Items[0] : null);
        }

        // Selecting a podcast loads its episodes from DB
        private async void lstPodcasts_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected || string.IsNullOrWhiteSpace(selected.rssUrl))
            {
                UpdateSelectedFeedLabel();
                UpdateFetchedFeedLabel();
                return;
            }
            _currentFlode = selected;
            _currentFlode.IsSaved = true;
            txtRssUrl.Text = selected.rssUrl;
            SyncFeedCategoryFromFeed(selected);
            UpdateSelectedFeedLabel();
            UpdateFetchedFeedLabel();
            try
            {
                var result = await _poddService.FetchFromDatabaseAsync(selected.rssUrl);
                _currentEpisodes = result.avsnitt ?? new List<PoddAvsnitt>();
                FillEpisodesGrid(_currentEpisodes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading episodes: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Assign a category to the currently selected (and saved) feed
        private async void btnSetCategory_Click(object? sender, EventArgs e)
        {
            try
            {
                PoddValidator.EnsureFeedSelected(_currentFlode);
                if (!_currentFlode!.IsSaved)
                    throw new ValidationException("Feed must be saved before assigning category.");
                PoddValidator.EnsureCategorySelected(cmbFeedCategory.SelectedItem as Kategori, "Feed category");
                var selectedCategory = (Kategori)cmbFeedCategory.SelectedItem!;
                PoddValidator.EnsureCategoryAssignmentAllowed(_currentFlode, selectedCategory);

                await _poddService.AssignCategoryAsync(_currentFlode.Id!, selectedCategory.Id);
                _currentFlode.categoryId = selectedCategory.Id;
                var match = _allSavedFeeds.FirstOrDefault(f => f.Id == _currentFlode.Id);
                if (match != null) match.categoryId = selectedCategory.Id;

                MessageBox.Show($"Category '{selectedCategory.Namn}' assigned.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ApplyCategoryFilter();
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error setting category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Remove category assignment from the current feed
        private async void btnRemoveCategory_Click(object? sender, EventArgs e)
        {
            try
            {
                PoddValidator.EnsureFeedSelected(_currentFlode);
                if (!_currentFlode!.IsSaved)
                    throw new ValidationException("Feed must be saved before removing category.");
                PoddValidator.EnsureFeedHasCategory(_currentFlode!);

                var confirm = MessageBox.Show("Remove category from this feed?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No) return;

                await _poddService.RemoveCategoryAsync(_currentFlode.Id!);
                _currentFlode.categoryId = string.Empty;
                var match = _allSavedFeeds.FirstOrDefault(f => f.Id == _currentFlode.Id);
                if (match != null) match.categoryId = string.Empty;
                if (cmbFeedCategory.Items.Count > 0) cmbFeedCategory.SelectedIndex = 0;

                MessageBox.Show("Category removed from feed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ApplyCategoryFilter();
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete the selected feed and all its episodes
        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected)
            {
                MessageBox.Show("Select a feed to remove.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show(
                $"Remove feed '{selected.displayName}' and its episodes?",
                "Confirm feed removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;
            try
            {
                await _poddService.DeleteFeedAndEpisodesAsync(selected.rssUrl);
                _allSavedFeeds.Remove(selected);
                ApplyCategoryFilter();
                if (_currentFlode?.rssUrl == selected.rssUrl)
                {
                    _currentFlode = null;
                    _currentEpisodes = new List<PoddAvsnitt>();
                    dgvEpisodes.Rows.Clear();
                    lblEpisodeTitle.Text = "No episodes.";
                    lblEpisodeCount.Text = "Episodes:0";
                    txtDescription.Clear();
                }
                UpdateSelectedFeedLabel();
                UpdateFetchedFeedLabel();
                MessageBox.Show("Feed removed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing feed: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Rename the selected feed
        private async void btnRename_Click(object? sender, EventArgs e)
        {
            try
            {
                if (lstPodcasts.SelectedItem is not Poddflöden selected)
                    throw new ValidationException("Select a feed first.");

                // Enforce: rename only for saved feeds
                if (!_currentFlode?.IsSaved ?? false)
                    throw new ValidationException("You can only rename feeds that are saved.");

                var newName = txtCustomName.Text.Trim();
                PoddValidator.EnsureFeedRenameValid(selected, newName);
                await _poddService.RenameFeedAsync(selected.Id!, newName);
                selected.displayName = newName;
                // Refresh data binding to show the new display name
                lstPodcasts.DisplayMember = "";
                lstPodcasts.DisplayMember = "displayName";
                UpdateSelectedFeedLabel();
                UpdateFetchedFeedLabel();
                MessageBox.Show("Feed renamed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear rename box after successful rename
                txtCustomName.Clear();
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error renaming feed: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Create a new category
        private async void btnCreateCategory_Click(object? sender, EventArgs e)
        {
            try
            {
                var name = txtNewCategoryName.Text.Trim();
                PoddValidator.ValidateCategoryName(name);
                if (_allCategories.Any(c => c.Namn.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    throw new ValidationException($"Category '{name}' already exists.");
                await _categoryService.CreateCategoryAsync(name);
                txtNewCategoryName.Clear();
                await LoadCategoriesAsync();
                MessageBox.Show("Category created.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Rename an existing category
        private async void btnRenameCategory_Click(object? sender, EventArgs e)
        {
            try
            {
                if (cmbCategoryEdit.SelectedItem is not Kategori selectedCat)
                    throw new ValidationException("Select a category to rename.");
                var newName = txtEditCategoryName.Text.Trim();
                PoddValidator.EnsureCategoryRenameValid(selectedCat, newName, _allCategories);
                var confirm = MessageBox.Show(
                    $"Rename '{selectedCat.Namn}' to '{newName}'?",
                    "Confirm rename", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No) return;
                await _categoryService.UpdateCategoryNameAsync(selectedCat.Id, newName);
                txtEditCategoryName.Clear();
                await LoadCategoriesAsync();
                if (_currentFlode != null)
                {
                    SyncFeedCategoryFromFeed(_currentFlode);
                }
                MessageBox.Show("Category renamed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error renaming category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete a category
        private async void btnDeleteCategory_Click(object? sender, EventArgs e)
        {
            try
            {
                PoddValidator.EnsureCategorySelected(cmbCategoryEdit.SelectedItem as Kategori, "Category to delete");
                var selectedCat = (Kategori)cmbCategoryEdit.SelectedItem!;
                var confirm = MessageBox.Show(
                    $"Delete category '{selectedCat.Namn}'?",
                    "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.No) return;
                await _categoryService.DeleteCategoryAsync(selectedCat.Id);
                txtEditCategoryName.Clear();
                await LoadCategoriesAsync();
                MessageBox.Show("Category deleted.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Rebind episodes to the grid and update selection
        private void FillEpisodesGrid(List<PoddAvsnitt> avsnitt)
        {
            dgvEpisodes.Rows.Clear();
            foreach (var ep in avsnitt)
                dgvEpisodes.Rows.Add(ep.title, ep.publishDate);
            lblEpisodeCount.Text = $"Episodes: {avsnitt.Count}";
            if (dgvEpisodes.Rows.Count > 0)
            {
                dgvEpisodes.ClearSelection();
                dgvEpisodes.Rows[0].Selected = true;
                UpdateEpisodeDetailsFromGrid();
            }
            else
            {
                lblEpisodeTitle.Text = "No episodes.";
                txtDescription.Clear();
            }
        }

        private void dgvEpisodes_SelectionChanged(object? sender, EventArgs e) => UpdateEpisodeDetailsFromGrid();


        private bool TryGetSelectedEpisode(out PoddAvsnitt ep)
        {
            ep = null!;
            if (_currentEpisodes.Count == 0) return false;
            if (dgvEpisodes.SelectedRows.Count == 0) return false;
            int index = dgvEpisodes.SelectedRows[0].Index;
            if (index < 0 || index >= _currentEpisodes.Count) return false;
            ep = _currentEpisodes[index];
            return true;
        }

        // Update episode detail panel from current grid selection
        private void UpdateEpisodeDetailsFromGrid()
        {
            if (!TryGetSelectedEpisode(out var ep)) return;
            lblEpisodeTitle.Text = ep.title;
            var cleanDesc = HtmlCleaner.ToPlainText(ep.description);
            txtDescription.Text = $"Title: {ep.title}\r\nPublished: {ep.publishDate}\r\n\r\n{cleanDesc}";
        }


        private void lblFeedCategory_Click(object sender, EventArgs e) { }
        private void lblCustomName_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void lblEpisodeCount_Click(object sender, EventArgs e) { }
        private void lblEpisodeTitle_Click(object sender, EventArgs e) { }

        private void ApplyTheme()
        {
            UiHelpers.ApplyTheme(
            this,
            new[] { grpMyPodcasts, grpEpisodes, grpCategories },
            new[] { lblRssUrl, lblCategoryFilter, lblCustomName, lblFeedCategory, lblNewCategory, lblCategoryEdit, lblNewCategoryName, lblSelectedFeed, label2, label3 },
            new[] { txtRssUrl, txtCustomName, txtNewCategoryName, txtEditCategoryName, txtDescription, textBox1 },
            new[] { lstPodcasts, lstCategoriesRight },
            new[] { cmbCategoryFilter, cmbFeedCategory, cmbCategoryEdit },
            new[] { btnFetch, btnSaveFeed, btnOpenExternalLink, btnSetCategory, btnRemoveCategory, btnDelete, btnRename, btnCreateCategory, btnRenameCategory, btnDeleteCategory },
            dgvEpisodes,
            pictureBox1);
        }


        private void ClearTextBoxOnLeave(object? sender, EventArgs e)
        {
            if (sender is not TextBox tb) return;
            var next = this.ActiveControl;
            if (next == btnSaveFeed || next == btnRename || next == btnCreateCategory || next == btnRenameCategory) return;
            tb.Clear();
        }

        private void lblSelectedFeed_Click(object sender, EventArgs e)
        {

        }


        private void UpdateSelectedFeedLabel()
        {
            string text;
            var isSaved = _currentFlode != null && (_currentFlode.IsSaved || (_currentFlode.Id != null && _allSavedFeeds.Any(f => f.Id == _currentFlode.Id)));
            if (isSaved && !string.IsNullOrWhiteSpace(_currentFlode!.displayName))
            {
                text = $"Selected feed: {_currentFlode.displayName}";
            }
            else
            {
                text = "(No saved feed selected)";
            }

            lblSelectedFeed.Text = text;
        }

        private void txtCustomName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCategoryFilter_Click(object sender, EventArgs e)
        {

        }

        private void txtRssUrl_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
