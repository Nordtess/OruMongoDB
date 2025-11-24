using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OruMongoDB.BusinessLayer;
using OruMongoDB.BusinessLayer.Rss;
using OruMongoDB.Core;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;

namespace UI
{
    public partial class MainUiForm : Form
    {
        private readonly JamiesPoddService _jamieService = new JamiesPoddService();
        private readonly PoddService _poddService;
        private readonly CategoryService _categoryService;
        private readonly AlexKrav _alexService = new AlexKrav();

        private List<Poddflöden> _allSavedFeeds = new();
        private Poddflöden? _currentFlode;
        private List<PoddAvsnitt> _currentEpisodes = new();
        private List<Kategori> _allCategories = new();

        public MainUiForm()
        {
            InitializeComponent();
            var connector = MongoConnector.Instance;
            var db = connector.GetDatabase();
            var poddRepo = new PoddflodeRepository(db);
            var avsnittRepo = new PoddAvsnittRepository(db);
            var rssParser = new RssParser();
            _poddService = new PoddService(poddRepo, avsnittRepo, rssParser, connector);
            _categoryService = new CategoryService(new CategoryRepository(db));
        }

        private async void MainUiForm_Load(object? sender, EventArgs e)
        {
            try
            {
                await LoadCategoriesAsync();
                LoadSavedFeeds();
                Log("Application started.");
            }
            catch (Exception ex)
            {
                Log("Error on startup: " + ex.Message);
                MessageBox.Show("Error during startup: " + ex.Message, "Startup error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnFetch_Click(object? sender, EventArgs e)
        {
            var url = txtRssUrl.Text.Trim();
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter an RSS URL first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _currentFlode = null;
                _currentEpisodes = new List<PoddAvsnitt>();
                dgvEpisodes.Rows.Clear();
                lblEpisodeTitle.Text = "No episodes.";
                txtDescription.Clear();

                Log($"Trying to load feed from MongoDB for URL: {url}");
                try
                {
                    var dbResult = await _jamieService.HamtaPoddflodeFranUrlAsync(url);
                    _currentFlode = dbResult.Flode;
                    _currentEpisodes = dbResult.Avsnitt ?? new List<PoddAvsnitt>();
                    txtCustomName.Text = _currentFlode.displayName;
                    SyncFeedCategoryFromFeed(_currentFlode);
                    FillEpisodesGrid(_currentEpisodes);
                    Log($"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}' from MongoDB.");
                    MessageBox.Show(
                        $"Loaded {_currentEpisodes.Count} episodes from '{_currentFlode.displayName}' (database).",
                        "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                catch (ValidationException vex)
                {
                    Log("No feed found in MongoDB. Internet fallback. " + vex.Message);
                }

                Log($"Fetching feed from Internet: {url}");
                var netResult = await _poddService.FetchPoddFeedAsync(url);
                _currentFlode = netResult.poddflode;
                _currentEpisodes = netResult.avsnitt ?? new List<PoddAvsnitt>();
                txtCustomName.Text = _currentFlode.displayName;
                if (cmbFeedCategory.Items.Count > 0) cmbFeedCategory.SelectedIndex = 0;
                FillEpisodesGrid(_currentEpisodes);
                MessageBox.Show(
                    $"Fetched {_currentEpisodes.Count} episodes from '{_currentFlode.displayName}' (Internet).",
                    "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log($"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}' from Internet.");
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation error while fetching: " + vex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while fetching feed: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error while fetching feed: " + ex);
            }
        }

        private async void btnSaveFeed_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_currentFlode == null)
                {
                    MessageBox.Show("No feed has been loaded yet.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_currentEpisodes.Count == 0)
                {
                    MessageBox.Show("There are no episodes to save.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customName = txtCustomName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(customName))
                    _currentFlode.displayName = customName;

                foreach (var ep in _currentEpisodes)
                    ep.feedId = _currentFlode.Id!;

                await _jamieService.SparaPoddflodeOchAvsnittAsync(_currentFlode, _currentEpisodes);
                _currentFlode.IsSaved = true;
                _currentFlode.SavedAt = DateTime.UtcNow;

                MessageBox.Show("Feed and episodes saved.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log($"Saved feed '{_currentFlode.displayName}' and {_currentEpisodes.Count} episodes.");
                LoadSavedFeeds();
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation error while saving: " + vex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error while saving feed and episodes: " + ex);
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

        private void LoadSavedFeeds()
        {
            _allSavedFeeds = _jamieService.HamtaAllaFloden()?.ToList() ?? new List<Poddflöden>();
            ApplyCategoryFilter();
        }

        private void ApplyCategoryFilter()
        {
            lstPodcasts.DataSource = null;
            lstPodcasts.Items.Clear();
            IEnumerable<Poddflöden> filtered = _allSavedFeeds;

            if (cmbCategoryFilter.SelectedItem is Kategori selectedCat)
                filtered = filtered.Where(f => f.categoryId == selectedCat.Id);

            var list = filtered.ToList();
            if (list.Count == 0)
            {
                lstPodcasts.Items.Add("No podcasts found.");
            }
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

        private async void lstPodcasts_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected || string.IsNullOrWhiteSpace(selected.rssUrl))
                return;

            _currentFlode = selected;
            txtCustomName.Text = selected.displayName;
            txtRssUrl.Text = selected.rssUrl;
            Log($"Selected saved podcast: {selected.displayName}");
            SyncFeedCategoryFromFeed(selected);

            try
            {
                var result = await _jamieService.HamtaPoddflodeFranUrlAsync(selected.rssUrl);
                _currentEpisodes = result.Avsnitt ?? new List<PoddAvsnitt>();
                FillEpisodesGrid(_currentEpisodes);
                Log($"Loaded {_currentEpisodes.Count} episodes for '{selected.displayName}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading episodes: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error loading episodes: " + ex);
            }
        }

        private async void btnSetCategory_Click(object? sender, EventArgs e)
        {
            if (_currentFlode == null)
            {
                MessageBox.Show("Select a feed first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbFeedCategory.SelectedItem is not Kategori selectedCategory)
            {
                MessageBox.Show("Select a category in feed category.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_currentFlode.categoryId == selectedCategory.Id)
            {
                MessageBox.Show("Feed already has this category.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                await _poddService.AssignCategoryAsync(_currentFlode.Id!, selectedCategory.Id);
                _currentFlode.categoryId = selectedCategory.Id;
                var match = _allSavedFeeds.FirstOrDefault(f => f.Id == _currentFlode.Id);
                if (match != null) match.categoryId = selectedCategory.Id;
                Log($"Set category '{selectedCategory.Namn}' for '{_currentFlode.displayName}'.");
                ApplyCategoryFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error setting category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error setting category: " + ex);
            }
        }

        private async void btnRemoveCategory_Click(object? sender, EventArgs e)
        {
            if (_currentFlode == null)
            {
                MessageBox.Show("Select a feed first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(_currentFlode.categoryId))
            {
                MessageBox.Show("Feed has no category.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var confirm = MessageBox.Show("Remove category from this feed?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                await _poddService.AssignCategoryAsync(_currentFlode.Id!, string.Empty);
                _currentFlode.categoryId = string.Empty;
                var match = _allSavedFeeds.FirstOrDefault(f => f.Id == _currentFlode.Id);
                if (match != null) match.categoryId = string.Empty;
                if (cmbFeedCategory.Items.Count > 0) cmbFeedCategory.SelectedIndex = 0;
                Log($"Removed category from '{_currentFlode.displayName}'.");
                ApplyCategoryFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error removing category: " + ex);
            }
        }

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
                await _jamieService.TaBortSparatFlodeAsync(selected.rssUrl);
                Log($"Removed feed '{selected.displayName}'.");
                _allSavedFeeds.Remove(selected);
                ApplyCategoryFilter();
                if (_currentFlode?.rssUrl == selected.rssUrl)
                {
                    _currentFlode = null;
                    _currentEpisodes = new List<PoddAvsnitt>();
                    dgvEpisodes.Rows.Clear();
                    lblEpisodeTitle.Text = "No episodes.";
                    txtDescription.Clear();
                }
                MessageBox.Show("Feed removed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing feed: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error removing feed: " + ex);
            }
        }

        private async void btnRename_Click(object? sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected)
            {
                MessageBox.Show("Select a feed first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var newName = txtCustomName.Text.Trim();
            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Enter a new name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.Equals(newName, selected.displayName, StringComparison.Ordinal))
            {
                MessageBox.Show("Name unchanged.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                await _alexService.AndraNamnPaPoddflodeAsync(selected.Id!, newName);
                selected.displayName = newName;
                lstPodcasts.DisplayMember = "";
                lstPodcasts.DisplayMember = "displayName";
                MessageBox.Show("Feed renamed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log($"Renamed feed '{selected.Id}' to '{newName}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error renaming feed: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error renaming feed: " + ex);
            }
        }

        private async void btnCreateCategory_Click(object? sender, EventArgs e)
        {
            var name = txtNewCategoryName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Enter category name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_allCategories.Any(c => c.Namn.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Category exists.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                await _categoryService.CreateCategoryAsync(name);
                Log($"Created category '{name}'.");
                txtNewCategoryName.Clear();
                await LoadCategoriesAsync();
                MessageBox.Show("Category created.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error creating category: " + ex);
            }
        }

        private async void btnRenameCategory_Click(object? sender, EventArgs e)
        {
            if (cmbCategoryEdit.SelectedItem is not Kategori selectedCat || selectedCat.Id == null)
            {
                MessageBox.Show("Select a category to rename.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var newName = txtEditCategoryName.Text.Trim();
            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Enter new category name.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.Equals(newName, selectedCat.Namn, StringComparison.Ordinal))
            {
                MessageBox.Show("Name unchanged.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_allCategories.Any(c => c.Namn.Equals(newName, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Category already exists.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show(
                $"Rename '{selectedCat.Namn}' to '{newName}'?",
                "Confirm rename", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No) return;

            try
            {
                await _categoryService.UpdateCategoryNameAsync(selectedCat.Id, newName);
                Log($"Renamed category '{selectedCat.Namn}' to '{newName}'.");
                txtEditCategoryName.Clear();
                await LoadCategoriesAsync();
                MessageBox.Show("Category renamed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error renaming category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error renaming category: " + ex);
            }
        }

        private async void btnDeleteCategory_Click(object? sender, EventArgs e)
        {
            if (cmbCategoryEdit.SelectedItem is not Kategori selectedCat)
            {
                MessageBox.Show("Select a category to delete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirm = MessageBox.Show(
                $"Delete category '{selectedCat.Namn}'?",
                "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.No) return;

            try
            {
                await _alexService.RaderaKategoriAsync(selectedCat.Id);
                Log($"Deleted category '{selectedCat.Namn}'.");
                txtEditCategoryName.Clear();
                await LoadCategoriesAsync();
                MessageBox.Show("Category deleted.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting category: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error deleting category: " + ex);
            }
        }

        private void FillEpisodesGrid(List<PoddAvsnitt> avsnitt)
        {
            dgvEpisodes.Rows.Clear();
            foreach (var ep in avsnitt)
                dgvEpisodes.Rows.Add(ep.title, ep.publishDate, string.Empty);

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

        private void UpdateEpisodeDetailsFromGrid()
        {
            if (_currentEpisodes.Count == 0) return;
            if (dgvEpisodes.SelectedRows.Count == 0) return;
            int index = dgvEpisodes.SelectedRows[0].Index;
            if (index < 0 || index >= _currentEpisodes.Count) return;
            var ep = _currentEpisodes[index];
            lblEpisodeTitle.Text = ep.title;
            txtDescription.Text = $"Title: {ep.title}\r\nPublished: {ep.publishDate}\r\n\r\n{ep.description}";
        }

        private void btnOpenExternalLink_Click(object? sender, EventArgs e)
        {
            if (_currentEpisodes.Count == 0) return;
            if (dgvEpisodes.SelectedRows.Count == 0) return;
            int index = dgvEpisodes.SelectedRows[0].Index;
            if (index < 0 || index >= _currentEpisodes.Count) return;
            var ep = _currentEpisodes[index];
            if (string.IsNullOrWhiteSpace(ep.link))
            {
                MessageBox.Show("Episode has no link.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                Process.Start(new ProcessStartInfo { FileName = ep.link, UseShellExecute = true });
                Log($"Opened external link for '{ep.title}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open link: " + ex.Message, "Technical error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error opening link: " + ex);
            }
        }

        private void Log(string message)
        {
            var line = $"[{DateTime.Now:HH:mm}] {message}";
            txtLog.AppendText(line + Environment.NewLine);
        }
    }
}
