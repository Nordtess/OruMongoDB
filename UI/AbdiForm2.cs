using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OruMongoDB.BusinessLayer;
using OruMongoDB.BusinessLayer.Rss;
using OruMongoDB.BusinessLayer.Exceptions;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;

namespace UI
{
    public partial class AbdiForm2 : Form
    {
        // Service-lagret vi använder
        private readonly IPoddService _poddService;

        // Alla avsnitt vi hämtar från RSS
        private List<PoddAvsnitt> _allaAvsnitt = new();

        public AbdiForm2()
        {
            InitializeComponent();

            // Bygg upp PoddService med dina riktiga klasser
            var connector = MongoConnector.Instance;      // din singleton
            var db = connector.GetDatabase();

            var poddRepo = new PoddflodeRepository(db);
            var avsnittRepo = new PoddAvsnittRepository(db);
            var rssParser = new RssParser();

            _poddService = new PoddService(poddRepo, avsnittRepo, rssParser, connector);
        }

        // Klick på "Hämta och visa beskrivning"
        private async void btnhämta_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Läs URL från textbox
                var rssUrl = tbUrl.Text.Trim();
                if (string.IsNullOrWhiteSpace(rssUrl))
                {
                    MessageBox.Show("Skriv in en RSS-URL först.");
                    return;
                }

                // 2. Hämta poddflöde + avsnitt via PoddService (RSS → PoddAvsnitt)
                var result = await _poddService.FetchPoddFeedAsync(rssUrl);

                _allaAvsnitt = result.avsnitt ?? new List<PoddAvsnitt>();

                // 3. Fyll listboxen med avsnitt
                listboxAvsnitt1.Items.Clear();
                visabeskrivning.Clear();

                foreach (var avsnitt in _allaAvsnitt)
                {
                    // lägg in själva objektet (så vi kommer åt description sen)
                    listboxAvsnitt1.Items.Add(avsnitt);
                }

                // visa titel i listan
                listboxAvsnitt1.DisplayMember = "title";

                if (_allaAvsnitt.Count == 0)
                {
                    MessageBox.Show("Inga avsnitt hittades i detta flöde.");
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

        // När användaren väljer ett avsnitt i listan
        private void listboxAvsnitt1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listboxAvsnitt1.SelectedItem is not PoddAvsnitt avsnitt)
                return;

            visabeskrivning.Text =
                $"Titel: {avsnitt.title}\r\n" +
                $"Publicerad: {avsnitt.publishDate}\r\n\r\n" +
                $"{avsnitt.description}";
        }

        private void visabeskrivning_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
