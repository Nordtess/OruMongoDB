using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OruMongoDB.BusinessLayer;
using OruMongoDB.BusinessLayer.Exceptions;
using OruMongoDB.BusinessLayer.Rss;
using OruMongoDB.Core;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;

namespace UI
{
    public partial class AbdiForm2 : Form
    {
        // Internet / RSS-service
        private readonly PoddService _poddService;

        // Jamie-service för att läsa sparade flöden/avsnitt från MongoDB
        private readonly JamiesPoddService _jamieService;

        // Avsnitt som just nu visas i listan
        private List<PoddAvsnitt> _allaAvsnitt = new();

        public AbdiForm2()
        {
            InitializeComponent();

            // Bygg upp PoddService (Internet + spara) via MongoConnector
            var connector = MongoConnector.Instance;
            var db = connector.GetDatabase();

            var poddRepo = new PoddflodeRepository(db);
            var avsnittRepo = new PoddAvsnittRepository(db);
            var rssParser = new RssParser();

            _poddService = new PoddService(poddRepo, avsnittRepo, rssParser, connector);

            // Jamie-service för att läsa direkt från MongoDB
            _jamieService = new JamiesPoddService();

            // Fyll comboboxen med källor
            cmbKalla.Items.Add("Internet (RSS-URL)");
            cmbKalla.Items.Add("MongoDB (sparade flöden)");

            cmbKalla.SelectedIndex = 0; // default = Internet
        }

        private void cmbKalla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKalla.SelectedIndex == 0)
            {
                // Internet-läge
                tbUrl.Enabled = true;
                linkLabel1.Visible = true;
                tbUrl.Text = "https://anchor.fm/s/d49ab0d0/podcast/rss";

                listboxFlodenDb.Enabled = false;
                listboxFlodenDb.DataSource = null;
                listboxFlodenDb.Items.Clear();
            }
            else if (cmbKalla.SelectedIndex == 1)
            {
                // MongoDB-läge
                tbUrl.Enabled = false;
                linkLabel1.Visible = false;
                listboxFlodenDb.Enabled = true;
                LaddaSparadeFlodenFranMongo();
            }
        }

        private void LaddaSparadeFlodenFranMongo()
        {
            try
            {
                var floden = _jamieService.HamtaAllaFloden(); // läser från Mongo via JamiesPoddService

                listboxFlodenDb.DataSource = null;
                listboxFlodenDb.Items.Clear();

                if (floden.Count == 0)
                {
                    listboxFlodenDb.Items.Add("Inga sparade poddar.");
                    listboxFlodenDb.Enabled = false;
                    return;
                }

                listboxFlodenDb.Enabled = true;
                listboxFlodenDb.DataSource = floden;
                listboxFlodenDb.DisplayMember = "displayName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Fel vid läsning av sparade poddar: " + ex.Message,
                    "Tekniskt fel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Klick på "Hämta avsnitt"
        private async void btnhämta_Click(object sender, EventArgs e)
        {
            try
            {
                listboxAvsnitt1.Items.Clear();
                visabeskrivning.Clear();
                _allaAvsnitt = new List<PoddAvsnitt>();

                if (cmbKalla.SelectedIndex == 0)
                {
                    var rssUrl = tbUrl.Text.Trim();
                    if (string.IsNullOrWhiteSpace(rssUrl))
                    {
                        MessageBox.Show("Skriv in en RSS-URL först.");
                        return;
                    }

                    var result = await _poddService.FetchPoddFeedAsync(rssUrl);
                    _allaAvsnitt = result.avsnitt ?? new List<PoddAvsnitt>();
                }
                else
                {
                    if (listboxFlodenDb.SelectedItem is not Poddflöden valtFlode)
                    {
                        MessageBox.Show("Välj först ett sparat poddflöde i listan till höger.");
                        return;
                    }

                    // Använd JamiesPoddService för att läsa avsnitt från MongoDB via rssUrl
                    var result = await _jamieService.HamtaPoddflodeFranUrlAsync(valtFlode.rssUrl);
                    _allaAvsnitt = result.Avsnitt ?? new List<PoddAvsnitt>();
                }

                foreach (var avsnitt in _allaAvsnitt)
                {
                    listboxAvsnitt1.Items.Add(avsnitt);
                }

                listboxAvsnitt1.DisplayMember = "title";

                if (_allaAvsnitt.Count == 0)
                {
                    MessageBox.Show("Inga avsnitt hittades för detta flöde.");
                }
            }
            catch (ServiceException ex)
            {
                MessageBox.Show("Kunde inte hämta poddflödet: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett oväntat fel inträffade: " + ex.Message);
            }
        }

        // När användaren väljer ett avsnitt → visa titel, datum och beskrivning
        private void listboxAvsnitt1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listboxAvsnitt1.SelectedItem is not PoddAvsnitt avsnitt)
                return;

            visabeskrivning.Text =
                $"Titel: {avsnitt.title}\r\n" +
                $"Publicerad: {avsnitt.publishDate}\r\n\r\n" +
                $"{avsnitt.description}";
        }

        private void listboxAvsnitt1_DoubleClick(object sender, EventArgs e)
        {
            if (listboxAvsnitt1.SelectedItem is not PoddAvsnitt avsnitt)
                return;

            var text = $"{avsnitt.description}";
            MessageBox.Show(text, "Avsnittsdetaljer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void visabeskrivning_TextChanged(object sender, EventArgs e)
        {
            // inget behöver göras här – eventet finns bara kopplat
        }

        private void tbUrl_TextChanged(object sender, EventArgs e)
        {
            // optional: validate URL/local input
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbUrl.Text = "https://anchor.fm/s/d49ab0d0/podcast/rss";
        }

        private void AbdiForm2_Load(object sender, EventArgs e)
        {
            // form load actions (if any)
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = "https://anchor.fm/s/d49ab0d0/podcast/rss";
            tbUrl.Text = url;
            tbUrl.Focus();
            tbUrl.SelectAll();
        }
    }
}






