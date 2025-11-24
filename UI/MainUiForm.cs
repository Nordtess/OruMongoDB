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
        // === SERVICES / REPOS ===

        // Jamies service – centralt nav för sparade flöden + hämta via URL
        private readonly JamiesPoddService _jamieService = new JamiesPoddService();

        // Abdis/Diyars Internet/DB-service (bl.a. AssignCategoryAsync, FetchPoddFeedAsync)
        private readonly PoddService _poddService;

        // Diyars kategoritjänst
        private readonly CategoryService _categoryService;

        // Alex krav – t.ex. byta namn på poddflöde, radera kategori
        private readonly AlexKrav _alexService = new AlexKrav();

        // === STATE ===
        private List<Poddflöden> _allSavedFeeds = new();   // alla sparade poddar från DB
        private Poddflöden? _currentFlode;                 // flödet som just nu är aktivt
        private List<PoddAvsnitt> _currentEpisodes = new(); // avsnitt för _currentFlode
        private List<Kategori> _allCategories = new();     // alla kategorier

        public MainUiForm()
        {
            InitializeComponent();

            // Bygg gemensam MongoConnector
            var connector = MongoConnector.Instance;
            var db = connector.GetDatabase();

            var poddRepo = new PoddflodeRepository(db);
            var avsnittRepo = new PoddAvsnittRepository(db);
            var rssParser = new RssParser();

            // PoddService används till t.ex. AssignCategoryAsync, FetchPoddFeedAsync
            _poddService = new PoddService(poddRepo, avsnittRepo, rssParser, connector);

            // CategoryService för CRUD på Kategori
            _categoryService = new CategoryService(new CategoryRepository(db));
        }

        // ============================================================
        // FORM LOAD
        // ============================================================

        private async void MainUiForm_Load(object sender, EventArgs e)
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
                MessageBox.Show("Error during startup: " + ex.Message,
                    "Startup error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // TOP: FETCH VIA RSS + SAVE
        // ============================================================



        private async void btnFetch_Click(object sender, EventArgs e)
        {
            string url = txtRssUrl.Text.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Please enter an RSS URL first.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Poddflöden? dbFlode = null;
                List<PoddAvsnitt> dbEpisodes = new();

                // ==========================
                // 1) FÖRSÖK LÄSA FRÅN MONGODB
                // ==========================
                try
                {
                    Log($"Trying to load feed from MongoDB for URL: {url}");

                    var dbResult = await _jamieService.HamtaPoddflodeFranUrlAsync(url);

                    dbFlode = dbResult.Flode;
                    dbEpisodes = dbResult.Avsnitt ?? new List<PoddAvsnitt>();

                    if (dbEpisodes.Count > 0)
                    {
                        // Flöde + avsnitt finns redan i DB → använd dem
                        _currentFlode = dbFlode;
                        _currentEpisodes = dbEpisodes;

                        txtCustomName.Text = _currentFlode.displayName;
                        FillEpisodesGrid(_currentEpisodes);

                        // Om IsSaved = true → ingen idé att spara igen
                        btnSaveFeed.Enabled = !_currentFlode.IsSaved;

                        MessageBox.Show(
                            $"Loaded {_currentEpisodes.Count} episodes from '{_currentFlode.displayName}' (database).",
                            "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Log($"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}' from MongoDB.");
                        return;
                    }

                    // Flödet finns, men saknar avsnitt i DB
                    Log($"Feed '{dbFlode.displayName}' found in MongoDB but has 0 episodes. Fetching episodes from Internet…");
                }
                catch (ValidationException vex) when (
                    vex.Message.StartsWith("Hittade inget poddflöde i databasen"))
                {
                    // Flödet finns inte alls i DB → vi går direkt till internet nedan
                    Log("No matching feed found in MongoDB, fetching from Internet…");
                }

                // ==========================
                // 2) INTERNET (NYTT ELLER “TOMT” FLÖDE)
                // ==========================
                Log($"Fetching feed from Internet: {url}");

                // PoddService.FetchPoddFeedAsync returnerar (Poddflöden poddflode, List<PoddAvsnitt> avsnitt)
                var (poddflode, avsnitt) = await _poddService.FetchPoddFeedAsync(url);
                var internetEpisodes = avsnitt ?? new List<PoddAvsnitt>();

                if (dbFlode != null)
                {
                    // Flödet fanns i DB men utan avsnitt:
                    // Behåll DB-flödets Id (så feedId matchar rätt dokument)
                    _currentFlode = dbFlode;
                    _currentEpisodes = internetEpisodes;

                    foreach (var ep in _currentEpisodes)
                    {
                        ep.feedId = _currentFlode.Id!;
                    }
                }
                else
                {
                    // Helt nytt flöde – ta allt från internet
                    _currentFlode = poddflode;
                    _currentEpisodes = internetEpisodes;
                }

                txtCustomName.Text = _currentFlode.displayName;
                FillEpisodesGrid(_currentEpisodes);

                // Nya/in-kompletta flöden är inte sparade → användaren kan spara
                btnSaveFeed.Enabled = !_currentFlode.IsSaved;

                MessageBox.Show(
                    $"Fetched {_currentEpisodes.Count} episodes from '{_currentFlode.displayName}' (Internet).",
                    "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log($"Loaded {_currentEpisodes.Count} episodes for '{_currentFlode.displayName}' from Internet.");
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
                MessageBox.Show("Error while fetching feed: " + ex.Message,
                    "Technical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error while fetching feed: " + ex);
            }
        }



        private async void btnSaveFeed_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentFlode == null)
                {
                    MessageBox.Show("No feed has been loaded yet.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_currentEpisodes == null || _currentEpisodes.Count == 0)
                {
                    MessageBox.Show("There are no episodes to save for this feed.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Uppdatera namn från textbox
                var customName = txtCustomName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(customName))
                {
                    _currentFlode.displayName = customName;
                }

                // Säkerställ att alla avsnitt pekar på rätt feedId
                foreach (var ep in _currentEpisodes)
                {
                    ep.feedId = _currentFlode.Id!;
                }

                // Kategori: om en kategori är vald i comboboxen så sätt den på flödet
                if (cmbCategoryFilter.SelectedItem is Kategori selectedCategory)
                {
                    _currentFlode.categoryId = selectedCategory.Id;
                }

                // Spara flöde + avsnitt I EN TRANSAKTION
                await _jamieService.SparaPoddflodeOchAvsnittAsync(_currentFlode, _currentEpisodes);

                _currentFlode.IsSaved = true;
                _currentFlode.SavedAt = DateTime.UtcNow;

                MessageBox.Show("Podcast feed and its episodes have been saved to your register.",
                    "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log($"Saved feed '{_currentFlode.displayName}' and {_currentEpisodes.Count} episodes.");

                // Uppdatera listan med sparade flöden
                LoadSavedFeeds();

                // eftersom den nu är sparad behöver man inte spara igen direkt
                btnSaveFeed.Enabled = false;
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation error while saving: " + vex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving feed: " + ex.Message,
                    "Technical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error while saving feed and episodes: " + ex);
            }
        }



        // ============================================================
        // LEFT: SAVED PODCASTS + CATEGORY FILTER
        // ============================================================

        private async Task LoadCategoriesAsync()
        {
            // IEnumerable<Kategori> -> List<Kategori>
            _allCategories = (await _categoryService
                .GetAllCategoriesAsync())
                .ToList();

            cmbCategoryFilter.Items.Clear();

            // Första valet = "All categories" (ingen filtrering)
            cmbCategoryFilter.Items.Add("All categories");

            foreach (var cat in _allCategories)
            {
                cmbCategoryFilter.Items.Add(cat);
            }

            // Viktigt: detta gör att combo visar Namn på Kategori-objekten
            cmbCategoryFilter.DisplayMember = "Namn";
            cmbCategoryFilter.SelectedIndex = 0;
        }


        private void LoadSavedFeeds()
        {
            _allSavedFeeds = _jamieService
                .HamtaAllaFloden()?
                .ToList()                      // <- konvertera till List
                ?? new List<Poddflöden>();

            ApplyCategoryFilter();
        }

        private void ApplyCategoryFilter()
        {
            lstPodcasts.DataSource = null;
            lstPodcasts.Items.Clear();

            IEnumerable<Poddflöden> filtered = _allSavedFeeds;

            // Om SelectedItem är en Kategori -> filtrera
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


        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCategoryFilter();
        }

        private async void lstPodcasts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected)
                return;

            _currentFlode = selected;
            txtCustomName.Text = selected.displayName;
            txtRssUrl.Text = selected.rssUrl;

            Log($"Selected saved podcast: {selected.displayName}");

            try
            {
                // Hämta avsnitt för valt flöde (via URL)
                var result = await _jamieService.HamtaPoddflodeFranUrlAsync(selected.rssUrl);
                _currentEpisodes = result.Avsnitt ?? new List<PoddAvsnitt>();

                FillEpisodesGrid(_currentEpisodes);
                Log($"Loaded {_currentEpisodes.Count} episodes for '{selected.displayName}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading episodes: " + ex.Message,
                    "Technical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error loading episodes for selected podcast: " + ex);
            }
        }

        // Ta bort sparat flöde (Jamies krav)
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected)
            {
                MessageBox.Show("Select a saved podcast to remove.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"This will remove '{selected.displayName}' and all its episodes from the database.\r\n\r\nAre you sure?",
                "Confirm removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                return;

            try
            {
                // HÅRD DELETE: flöde + avsnitt
                await _jamieService.TaBortPoddflodeOchAvsnittAsync(selected.rssUrl);
                Log($"Removed feed '{selected.displayName}' and its episodes from MongoDB.");

                // uppdatera state + UI
                _allSavedFeeds.Remove(selected);
                ApplyCategoryFilter();

                // töm avsnittslistan om vi just tog bort det som var aktivt
                if (_currentFlode != null && _currentFlode.rssUrl == selected.rssUrl)
                {
                    _currentFlode = null;
                    _currentEpisodes.Clear();
                    FillEpisodesGrid(_currentEpisodes);
                }
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Validation error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log("Validation error while deleting: " + vex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while removing feed: " + ex.Message,
                    "Technical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error while removing feed: " + ex);
            }
        }


        // Byt namn på valt flöde (Alex krav)
        private async void btnRename_Click(object sender, EventArgs e)
        {
            if (lstPodcasts.SelectedItem is not Poddflöden selected)
            {
                MessageBox.Show("Select a podcast first.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newName = txtCustomName.Text.Trim();
            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Please enter a new name in the 'Custom name' field.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await _alexService.AndraNamnPaPoddflodeAsync(selected.Id, newName);

                selected.displayName = newName;
                lstPodcasts.DisplayMember = "";         // force refresh
                lstPodcasts.DisplayMember = "displayName";

                MessageBox.Show("Podcast name updated.",
                    "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log($"Renamed podcast '{selected.Id}' to '{newName}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while renaming: " + ex.Message,
                    "Technical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error while renaming feed: " + ex);
            }
        }

        // ============================================================
        // RIGHT: EPISODE LIST + DETAILS
        // ============================================================

        private void FillEpisodesGrid(List<PoddAvsnitt> avsnitt)
        {
            dgvEpisodes.Rows.Clear();

            foreach (var ep in avsnitt)
            {
                dgvEpisodes.Rows.Add(
                    ep.title,
                    ep.publishDate,
                    string.Empty        
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

        private void dgvEpisodes_SelectionChanged(object sender, EventArgs e)
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

        // Öppna extern länk (t.ex. avsnittets webbsida/ljudfil)
        private void btnOpenExternalLink_Click(object sender, EventArgs e)
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
                MessageBox.Show("This episode does not have a link.",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Could not open link: " + ex.Message,
                    "Technical error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("Error opening external link: " + ex);
            }
        }

        // ============================================================
        // LOGG
        // ============================================================

        private void Log(string message)
        {
            var line = $"[{DateTime.Now:HH:mm}] {message}";
            txtLog.AppendText(line + Environment.NewLine);
        }
    }
}
