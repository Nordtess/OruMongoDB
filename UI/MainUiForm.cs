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
using OruMongoDB.Core.Validation;
using System.Drawing;

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
            ApplyTheme();

            var connector = MongoConnector.Instance;
            var db = connector.GetDatabase();
            var poddRepo = new PoddflodeRepository(db);
            var avsnittRepo = new PoddAvsnittRepository(db);
            var rssParser = new RssParser();
            _poddService = new PoddService(poddRepo, avsnittRepo, rssParser, connector);
            _categoryService = new CategoryService(new CategoryRepository(db), connector);
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
                _currentFlode = null;
                _currentEpisodes = new List<PoddAvsnitt>();
                dgvEpisodes.Rows.Clear();
                lblEpisodeTitle.Text = "No episodes.";
                lblEpisodeCount.Text = "Episodes: 0";
                txtDescription.Clear();

                Log($"Trying to load feed from MongoDB for URL: {url}");
                try
                {
                    var dbResult = await _jamieService.HamtaPoddflodeFranUrlAsync(url);
                    _currentFlode = dbResult.Flode;
                    _currentEpisodes = dbResult.Avsnitt ?? new List<PoddAvsnitt>();
                    _currentFlode.IsSaved = true;
                    txtCustomName.Text = _currentFlode.displayName;
                    SyncFeedCategoryFromFeed(_currentFlode);
                    FillEpisodesGrid(_currentEpisodes);
                    Log($"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}' from database.");
                    MessageBox.Show(
                        $"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}'.",
                        "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                catch (ValidationException vex)
                {
                    Log("Not found in DB. Falling back to internet. " + vex.Message);
                }

                Log($"Fetching feed from Internet: {url}");
                var netResult = await _poddService.FetchPoddFeedAsync(url);
                _currentFlode = netResult.poddflode;
                _currentEpisodes = netResult.avsnitt ?? new List<PoddAvsnitt>();
                _currentFlode.IsSaved = false;
                txtCustomName.Text = _currentFlode.displayName;
                if (cmbFeedCategory.Items.Count > 0) cmbFeedCategory.SelectedIndex = 0;
                FillEpisodesGrid(_currentEpisodes);
                MessageBox.Show(
                    $"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}'.",
                    "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                PoddValidator.EnsureFeedSelected(_currentFlode!);
                PoddValidator.EnsureFeedNotAlreadySaved(_currentFlode!);
                PoddValidator.EnsureEpisodesExist(_currentEpisodes);

                var customName = txtCustomName.Text.Trim();
                PoddValidator.ValidateFeedName(customName);
                _currentFlode!.displayName = customName;

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
            try
            {
                PoddValidator.EnsureFeedSelected(_currentFlode);
                var feed = _currentFlode!;
                PoddValidator.EnsureCategorySelected(cmbFeedCategory.SelectedItem as Kategori, "Feed category");
                var selectedCategory = (Kategori)cmbFeedCategory.SelectedItem!;
                PoddValidator.EnsureCategoryAssignmentAllowed(feed, selectedCategory);

                await _poddService.AssignCategoryAsync(feed.Id!, selectedCategory.Id);
                feed.categoryId = selectedCategory.Id;
                var match = _allSavedFeeds.FirstOrDefault(f => f.Id == feed.Id);
                if (match != null) match.categoryId = selectedCategory.Id;

                MessageBox.Show($"Category '{selectedCategory.Namn}' assigned.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log($"Set category '{selectedCategory.Namn}' for '{feed.displayName}'.");
                ApplyCategoryFilter();
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation (set category): " + vex.Message);
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
            try
            {
                PoddValidator.EnsureFeedSelected(_currentFlode);
                PoddValidator.EnsureFeedHasCategory(_currentFlode!);

                var confirm = MessageBox.Show("Remove category from this feed?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.No) return;

                await _poddService.RemoveCategoryAsync(_currentFlode!.Id!);
                _currentFlode.categoryId = string.Empty;
                var match = _allSavedFeeds.FirstOrDefault(f => f.Id == _currentFlode.Id);
                if (match != null) match.categoryId = string.Empty;
                if (cmbFeedCategory.Items.Count > 0) cmbFeedCategory.SelectedIndex = 0;

                MessageBox.Show("Category removed from feed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log($"Removed category from '{_currentFlode.displayName}'.");
                ApplyCategoryFilter();
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation (remove category): " + vex.Message);
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
                    lblEpisodeCount.Text = "Episodes: 0";
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
            try
            {
                if (lstPodcasts.SelectedItem is not Poddflöden selected)
                    throw new ValidationException("Select a feed first.");

                var newName = txtCustomName.Text.Trim();
                PoddValidator.EnsureFeedRenameValid(selected, newName);

                await _alexService.AndraNamnPaPoddflodeAsync(selected.Id!, newName);
                selected.displayName = newName;
                lstPodcasts.DisplayMember = "";
                lstPodcasts.DisplayMember = "displayName";

                MessageBox.Show("Feed renamed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log($"Renamed feed '{selected.Id}' to '{newName}'.");
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation (rename feed): " + vex.Message);
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
            try
            {
                var name = txtNewCategoryName.Text.Trim();
                PoddValidator.ValidateCategoryName(name);

                if (_allCategories.Any(c => c.Namn.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    throw new ValidationException($"Category '{name}' already exists.");

                await _categoryService.CreateCategoryAsync(name);
                Log($"Created category '{name}'.");
                txtNewCategoryName.Clear();
                await LoadCategoriesAsync();
                MessageBox.Show("Category created.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation (create category): " + vex.Message);
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
                Log($"Renamed category '{selectedCat.Namn}' to '{newName}'.");
                txtEditCategoryName.Clear();
                await LoadCategoriesAsync();
                MessageBox.Show("Category renamed.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation (rename category): " + vex.Message);
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
            try
            {
                PoddValidator.EnsureCategorySelected(cmbCategoryEdit.SelectedItem as Kategori, "Category to delete");
                var selectedCat = (Kategori)cmbCategoryEdit.SelectedItem!;

                var confirm = MessageBox.Show(
                    $"Delete category '{selectedCat.Namn}'?",
                    "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.No) return;

                await _alexService.RaderaKategoriAsync(selectedCat.Id);
                Log($"Deleted category '{selectedCat.Namn}'.");
                txtEditCategoryName.Clear();
                await LoadCategoriesAsync();
                MessageBox.Show("Category deleted.", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation (delete category): " + vex.Message);
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

        private void UpdateEpisodeDetailsFromGrid()
        {
            if (!TryGetSelectedEpisode(out var ep)) return;
            lblEpisodeTitle.Text = ep.title;
            txtDescription.Text = $"Title: {ep.title}\r\nPublished: {ep.publishDate}\r\n\r\n{ep.description}";
        }

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

        private void lblFeedCategory_Click(object sender, EventArgs e) { }
        private void lblCustomName_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void ApplyTheme()
        {
            Color bg = Color.FromArgb(22, 22, 22);
            Color panelBg = Color.FromArgb(30, 30, 30);
            Color borderGray = Color.FromArgb(55, 55, 55);
            Color btnGray = Color.FromArgb(45, 45, 45);
            Color btnHover = Color.FromArgb(60, 60, 60);
            Color btnDown = Color.FromArgb(35, 35, 35);

            Color textWhite = Color.WhiteSmoke;
            Color textGray = Color.FromArgb(200, 200, 200);

            this.BackColor = bg;
            this.ForeColor = textWhite;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            StyleGroupBox(grpMyPodcasts, panelBg, textGray);
            StyleGroupBox(grpEpisodes, panelBg, textGray);
            StyleGroupBox(grpCategories, panelBg, textGray);

            foreach (var lbl in new[]
            {
                lblRssUrl, lblCategoryFilter, lblCustomName, lblFeedCategory,
                lblNewCategory, lblCategoryEdit, lblNewCategoryName
            })
            {
                lbl.ForeColor = textGray;
            }

            lblEpisodeTitle.ForeColor = textWhite;
            lblEpisodeTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            lblEpisodeCount.ForeColor = textWhite;
            lblEpisodeCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            StyleTextBox(txtRssUrl, panelBg, textWhite, borderGray);
            StyleTextBox(txtCustomName, panelBg, textWhite, borderGray);
            StyleTextBox(txtNewCategoryName, panelBg, textWhite, borderGray);
            StyleTextBox(txtEditCategoryName, panelBg, textWhite, borderGray);

            txtDescription.BackColor = bg;
            txtDescription.ForeColor = textWhite;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;

            txtLog.BackColor = Color.FromArgb(18, 18, 18);
            txtLog.ForeColor = Color.FromArgb(210, 210, 210);
            txtLog.BorderStyle = BorderStyle.FixedSingle;

            StyleListBox(lstPodcasts, bg, textWhite, borderGray);
            StyleListBox(lstCategoriesRight, bg, textWhite, borderGray);

            StyleComboBox(cmbCategoryFilter, panelBg, textWhite);
            StyleComboBox(cmbFeedCategory, panelBg, textWhite);
            StyleComboBox(cmbCategoryEdit, panelBg, textWhite);

            StyleButton(btnFetch, btnGray, btnHover, btnDown, textWhite, borderGray);
            StyleButton(btnSaveFeed, btnGray, btnHover, btnDown, textWhite, borderGray);
            StyleButton(btnOpenExternalLink, btnGray, btnHover, btnDown, textWhite, borderGray);

            StyleButton(btnSetCategory, btnGray, btnHover, btnDown, textWhite, borderGray);
            StyleButton(btnRemoveCategory, btnGray, btnHover, btnDown, textWhite, borderGray);
            StyleButton(btnDelete, btnGray, btnHover, btnDown, textWhite, borderGray);
            StyleButton(btnRename, btnGray, btnHover, btnDown, textWhite, borderGray);

            StyleButton(btnCreateCategory, btnGray, btnHover, btnDown, textWhite, borderGray);
            StyleButton(btnRenameCategory, btnGray, btnHover, btnDown, textWhite, borderGray);
            StyleButton(btnDeleteCategory, btnGray, btnHover, btnDown, textWhite, borderGray);

            StyleGrid(dgvEpisodes, bg, panelBg, textWhite);

            pictureBox1.BackColor = bg;
        }

        private void StyleButton(Button btn, Color bg, Color hover, Color down, Color text, Color border)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = border;
            btn.BackColor = bg;
            btn.ForeColor = text;
            btn.FlatAppearance.MouseOverBackColor = hover;
            btn.FlatAppearance.MouseDownBackColor = down;
            btn.Cursor = Cursors.Hand;
        }

        private void StyleGroupBox(GroupBox g, Color bg, Color text)
        {
            g.BackColor = bg;
            g.ForeColor = text;
        }

        private void StyleListBox(ListBox lst, Color bg, Color text, Color border)
        {
            lst.BackColor = bg;
            lst.ForeColor = text;
            lst.BorderStyle = BorderStyle.FixedSingle;
        }

        private void StyleComboBox(ComboBox cmb, Color bg, Color text)
        {
            cmb.BackColor = bg;
            cmb.ForeColor = text;
            cmb.FlatStyle = FlatStyle.Flat;
        }

        private void StyleTextBox(TextBox txt, Color bg, Color text, Color border)
        {
            txt.BackColor = bg;
            txt.ForeColor = text;
            txt.BorderStyle = BorderStyle.FixedSingle;
        }

        private void StyleGrid(DataGridView dgv, Color bg, Color headerBg, Color text)
        {
            dgv.BackgroundColor = bg;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = headerBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = text;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.DefaultCellStyle.BackColor = bg;
            dgv.DefaultCellStyle.ForeColor = text;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 50, 50);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.GridColor = Color.FromArgb(60, 60, 60);
        }

        private void lblEpisodeCount_Click(object sender, EventArgs e) { }
        private void lblEpisodeTitle_Click(object sender, EventArgs e) { }
    }
}
