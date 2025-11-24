using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        // === SERVICES / REPOS ======================================

        private readonly JamiesPoddService _jamieService = new JamiesPoddService();
        private readonly PoddService _poddService;
        private readonly CategoryService _categoryService;
        private readonly AlexKrav _alexService = new AlexKrav();

        // === STATE ==================================================

        private List<Poddflöden> _allSavedFeeds = new();
        private Poddflöden? _currentFlode;
        private List<PoddAvsnitt> _currentEpisodes = new();
        private List<Kategori> _allCategories = new();

        // ============================================================
        // CTOR
        // ============================================================

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

        // ============================================================
        // FORM LOAD
        // ============================================================

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
                MessageBox.Show(
                    "Error during startup: " + ex.Message,
                    "Startup error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // TOP: FETCH VIA RSS  (DB först, sedan internet)
        // ============================================================

        private async void btnFetch_Click(object? sender, EventArgs e)
        {
            var url = txtRssUrl.Text.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show(
                    "Please enter an RSS URL first.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _currentFlode = null;
                _currentEpisodes = new List<PoddAvsnitt>();
                dgvEpisodes.Rows.Clear();
                lblEpisodeTitle.Text = "No episodes.";
                txtDescription.Clear();
                btnSaveFeed.Enabled = false;

                // 1) Försök läsa från MongoDB
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
                        "Done",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Feed finns redan i DB – men man får gärna spara om den
                    _currentFlode.IsSaved = true;
                    btnSaveFeed.Enabled = true;
                    return;
                }
                catch (ValidationException vex)
                {
                    Log("No feed found in MongoDB for that URL. Falling back to internet. Details: " + vex.Message);
                }

                // 2) Fallback: hämta från internet
                Log($"Fetching feed from Internet: {url}");

                var netResult = await _poddService.FetchPoddFeedAsync(url);

                _currentFlode = netResult.poddflode;
                _currentEpisodes = netResult.avsnitt ?? new List<PoddAvsnitt>();

                txtCustomName.Text = _currentFlode.displayName;

                if (cmbFeedCategory.Items.Count > 0)
                    cmbFeedCategory.SelectedIndex = 0; // (no category)

                FillEpisodesGrid(_currentEpisodes);

                MessageBox.Show(
                    $"Fetched {_currentEpisodes.Count} episodes from '{_currentFlode.displayName}' (Internet).",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Log($"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}' from Internet.");

                _currentFlode.IsSaved = false;
                btnSaveFeed.Enabled = true;
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(
                    vex.Message,
                    "Validation error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Log("Validation error while fetching: " + vex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while fetching feed: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error while fetching feed: " + ex);
            }
        }

        // ============================================================
        // SAVE FEED (endast spara flöde + avsnitt)
        // ============================================================

        private async void btnSaveFeed_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_currentFlode == null)
                {
                    MessageBox.Show(
                        "No feed has been loaded yet.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (_currentEpisodes == null || _currentEpisodes.Count == 0)
                {
                    MessageBox.Show(
                        "There are no episodes to save for this feed.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Uppdatera namn från textbox
                var customName = txtCustomName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(customName))
                {
                    _currentFlode.displayName = customName;
                }

                // Se till att alla avsnitt pekar på rätt feedId
                foreach (var ep in _currentEpisodes)
                {
                    ep.feedId = _currentFlode.Id!;
                }

                // Spara flöde + avsnitt i en transaktion
                await _jamieService.SparaPoddflodeOchAvsnittAsync(_currentFlode, _currentEpisodes);

                _currentFlode.IsSaved = true;
                _currentFlode.SavedAt = DateTime.UtcNow;

                MessageBox.Show(
                    "Podcast feed and its episodes have been saved in your register.",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Log($"Saved feed '{_currentFlode.displayName}' and {_currentEpisodes.Count} episodes.");

                LoadSavedFeeds();
                btnSaveFeed.Enabled = true; // man kan spara om
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(
                    vex.Message,
                    "Validation error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Log("Validation error while saving: " + vex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while saving feed: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error while saving feed and episodes: " + ex);
            }
        }

        // ============================================================
        // CATEGORIES: Ladda + sync
        // ============================================================

        private async Task LoadCategoriesAsync()
        {
            _allCategories = (await _categoryService.GetAllCategoriesAsync()).ToList();

            // Rensa alla comboboxar
            cmbCategoryFilter.Items.Clear();
            cmbFeedCategory.Items.Clear();
            cmbCategoryEdit.Items.Clear();
            lstCategoriesRight.Items.Clear();

            // Filter
            cmbCategoryFilter.Items.Add("All categories");

            // Feed category
            cmbFeedCategory.Items.Add("(no category)");

            // Edit-combo
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
            {
                filtered = filtered.Where(f => f.categoryId == selectedCat.Id);
            }

            var list = filtered.ToList();

            if (list.Count == 0)
            {
                lstPodcasts.Items.Add("No podcasts found.");
                lstPodcasts.Enabled = false;
            }
            else
            {
                lstPodcasts.Enabled = true;
                lstPodcasts.DataSource = list;
                lstPodcasts.DisplayMember = "displayName";
            }
        }

        private void cmbCategoryFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyCategoryFilter();
        }

        private void SyncFeedCategoryFromFeed(Poddflöden feed)
        {
            if (string.IsNullOrEmpty(feed.categoryId))
            {
                if (cmbFeedCategory.Items.Count > 0)
                    cmbFeedCategory.SelectedIndex = 0;
                return;
            }

            var cat = _allCategories.FirstOrDefault(c => c.Id == feed.categoryId);
            if (cat != null)
            {
                cmbFeedCategory.SelectedItem = cat;
            }
            else if (cmbFeedCategory.Items.Count > 0)
            {
                cmbFeedCategory.SelectedIndex = 0;
            }
        }

        // ============================================================
        // LEFT: PODCAST LIST
        // ============================================================

        private async void lstPodcasts_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected)
                return;

            if (string.IsNullOrWhiteSpace(selected.rssUrl))
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
                Log($"Loaded {_currentEpisodes.Count} episodes for '{selected.displayName}' from MongoDB.");

                _currentFlode.IsSaved = true;
                btnSaveFeed.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading episodes: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error loading episodes for selected podcast: " + ex);
            }
        }

        // ============================================================
        // FEED CATEGORY: Set / Remove
        // ============================================================

        private async void btnSetCategory_Click(object? sender, EventArgs e)
        {
            if (_currentFlode == null)
            {
                MessageBox.Show(
                    "No feed is selected. Select a feed in the list or fetch one first.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!_currentFlode.IsSaved)
            {
                MessageBox.Show(
                    "You must save the feed before assigning a category.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (cmbFeedCategory.SelectedItem is not Kategori selectedCategory)
            {
                MessageBox.Show(
                    "Please select a category in the 'Feed category' dropdown.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _poddService.AssignCategoryAsync(_currentFlode.Id, selectedCategory.Id);

                _currentFlode.categoryId = selectedCategory.Id;
                var match = _allSavedFeeds.FirstOrDefault(f => f.Id == _currentFlode.Id);
                if (match != null) match.categoryId = selectedCategory.Id;

                MessageBox.Show(
                    $"Category '{selectedCategory.Namn}' has been set for this feed.",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Log($"Set category '{selectedCategory.Namn}' for feed '{_currentFlode.displayName}'.");
                ApplyCategoryFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while setting category: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error while setting category: " + ex);
            }
        }

        private async void btnRemoveCategory_Click(object? sender, EventArgs e)
        {
            if (_currentFlode == null)
            {
                MessageBox.Show(
                    "No feed is selected. Select a feed in the list or fetch one first.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!_currentFlode.IsSaved)
            {
                MessageBox.Show(
                    "You must save the feed before removing its category.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "This will remove the category from the selected feed. Continue?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
                return;

            try
            {
                // Tom sträng = ingen kategori
                await _poddService.AssignCategoryAsync(_currentFlode.Id, string.Empty);

                _currentFlode.categoryId = string.Empty;
                var match = _allSavedFeeds.FirstOrDefault(f => f.Id == _currentFlode.Id);
                if (match != null) match.categoryId = string.Empty;

                if (cmbFeedCategory.Items.Count > 0)
                    cmbFeedCategory.SelectedIndex = 0;

                MessageBox.Show(
                    "Category has been removed from this feed.",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Log($"Removed category from feed '{_currentFlode.displayName}'.");
                ApplyCategoryFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while removing category: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error while removing category: " + ex);
            }
        }

        // ============================================================
        // FEED: Remove / Rename
        // ============================================================

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected)
            {
                MessageBox.Show(
                    "Select a saved podcast to remove.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"This will remove the feed '{selected.displayName}' and all its saved episodes from the database.\r\n\r\nAre you sure?",
                "Confirm feed removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
                return;

            try
            {
                await _jamieService.TaBortSparatFlodeAsync(selected.rssUrl);
                Log($"Removed feed '{selected.displayName}' and its episodes from MongoDB.");

                _allSavedFeeds.Remove(selected);
                ApplyCategoryFilter();

                if (_currentFlode != null && _currentFlode.rssUrl == selected.rssUrl)
                {
                    _currentFlode = null;
                    _currentEpisodes = new List<PoddAvsnitt>();
                    dgvEpisodes.Rows.Clear();
                    lblEpisodeTitle.Text = "No episodes.";
                    txtDescription.Clear();
                    btnSaveFeed.Enabled = false;
                }

                MessageBox.Show(
                    "Podcast feed and its episodes have been removed from the database.",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while removing feed: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error while removing feed: " + ex);
            }
        }

        private async void btnRename_Click(object? sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected)
            {
                MessageBox.Show(
                    "Select a podcast first.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var newName = txtCustomName.Text.Trim();
            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show(
                    "Please enter a new name in the 'Custom name' field.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _alexService.AndraNamnPaPoddflodeAsync(selected.Id, newName);

                selected.displayName = newName;
                lstPodcasts.DisplayMember = "";
                lstPodcasts.DisplayMember = "displayName";

                MessageBox.Show(
                    "Podcast name updated.",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Log($"Renamed podcast '{selected.Id}' to '{newName}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while renaming: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error while renaming feed: " + ex);
            }
        }

        // ============================================================
        // CATEGORY MANAGEMENT (höger panel)
        // ============================================================

        private async void btnCreateCategory_Click(object? sender, EventArgs e)
        {
            var name = txtNewCategoryName.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(
                    "Please enter a name for the new category.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _categoryService.CreateCategoryAsync(name);
                Log($"Created category '{name}'.");
                txtNewCategoryName.Clear();

                await LoadCategoriesAsync();

                MessageBox.Show(
                    "Category created.",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while creating category: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error while creating category: " + ex);
            }
        }

        private async void btnRenameCategory_Click(object? sender, EventArgs e)
        {
            if (cmbCategoryEdit.SelectedItem is not Kategori selectedCat)
            {
                MessageBox.Show(
                    "Please select a category to rename.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var newName = txtEditCategoryName.Text.Trim();
            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show(
                    "Please enter a new name for the selected category.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to rename category '{selectedCat.Namn}' to '{newName}'?",
                "Confirm rename",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
                return;

            try
            {
                await _categoryService.UpdateCategoryNameAsync(selectedCat.Id, newName);

                MessageBox.Show(
                    "Category name updated.",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Log($"Renamed category '{selectedCat.Namn}' to '{newName}'.");

                txtEditCategoryName.Clear();
                await LoadCategoriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while renaming category: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error while renaming category: " + ex);
            }
        }

        private async void btnDeleteCategory_Click(object? sender, EventArgs e)
        {
            if (cmbCategoryEdit.SelectedItem is not Kategori selectedCat)
            {
                MessageBox.Show(
                    "Please select a category to delete.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"This will delete the category '{selectedCat.Namn}'.\r\n" +
                "Podcasts linked to this category will keep their categoryId value, " +
                "but the category itself will no longer exist.\r\n\r\n" +
                "Are you sure?",
                "Confirm category deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.No)
                return;

            try
            {
                await _alexService.RaderaKategoriAsync(selectedCat.Id);

                MessageBox.Show(
                    "Category deleted.",
                    "Done",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Log($"Deleted category '{selectedCat.Namn}'.");

                txtEditCategoryName.Clear();
                await LoadCategoriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error while deleting category: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error while deleting category: " + ex);
            }
        }

        // ============================================================
        // EPISODES
        // ============================================================

        private void FillEpisodesGrid(List<PoddAvsnitt> avsnitt)
        {
            dgvEpisodes.Rows.Clear();

            foreach (var ep in avsnitt)
            {
                dgvEpisodes.Rows.Add(
                    ep.title,
                    ep.publishDate,
                    string.Empty // duration finns inte i modellen
                );
            }

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

        private void dgvEpisodes_SelectionChanged(object? sender, EventArgs e)
        {
            UpdateEpisodeDetailsFromGrid();
        }

        private void UpdateEpisodeDetailsFromGrid()
        {
            if (_currentEpisodes == null || _currentEpisodes.Count == 0)
                return;

            if (dgvEpisodes.SelectedRows.Count == 0)
                return;

            int index = dgvEpisodes.SelectedRows[0].Index;
            if (index < 0 || index >= _currentEpisodes.Count)
                return;

            var ep = _currentEpisodes[index];

            lblEpisodeTitle.Text = ep.title;
            txtDescription.Text =
                $"Title: {ep.title}\r\n" +
                $"Published: {ep.publishDate}\r\n\r\n" +
                $"{ep.description}";
        }

        private void btnOpenExternalLink_Click(object? sender, EventArgs e)
        {
            if (_currentEpisodes == null || _currentEpisodes.Count == 0)
                return;

            if (dgvEpisodes.SelectedRows.Count == 0)
                return;

            int index = dgvEpisodes.SelectedRows[0].Index;
            if (index < 0 || index >= _currentEpisodes.Count)
                return;

            var ep = _currentEpisodes[index];
            if (string.IsNullOrWhiteSpace(ep.link))
            {
                MessageBox.Show(
                    "This episode does not have a link.",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = ep.link,
                    UseShellExecute = true
                });

                Log($"Opened external link for episode '{ep.title}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not open link: " + ex.Message,
                    "Technical error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Log("Error opening external link: " + ex);
            }
        }

        // ============================================================
        // LOG
        // ============================================================

        private void Log(string message)
        {
            var line = $"[{DateTime.Now:HH:mm}] {message}";
            txtLog.AppendText(line + Environment.NewLine);
        }
    }
}

