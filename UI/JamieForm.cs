using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OruMongoDB.Core;
using OruMongoDB.Infrastructure;
using OruMongoDB.Domain;

namespace UI
{
    public partial class JamieForm : Form
    {
        private readonly JamiesPoddService _service = new JamiesPoddService();
        private Poddflöden? _currentFlode;
        private List<PoddAvsnitt>? _currentAvsnitt;

        public JamieForm()
        {
            InitializeComponent();
        }

        private async void buttonHamta_Click(object sender, EventArgs e)
        {
            try
            {
                string url = textBoxUrl.Text.Trim();
                if (string.IsNullOrEmpty(url))
                {
                    MessageBox.Show("Skriv in en RSS-url först!");
                    return;
                }

                var result = await _service.HamtaPoddflodeFranUrlAsync(url);

                _currentFlode = result.Flode;
                _currentAvsnitt = result.Avsnitt;

                listBoxAvsnitt.Items.Clear();
                foreach (var av in _currentAvsnitt)
                {
                    listBoxAvsnitt.Items.Add(av.title);
                }

                MessageBox.Show($"Hämtade {_currentAvsnitt.Count} avsnitt från {result.Flode.displayName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid hämtning: " + ex.Message);
            }
        }

        private void listBoxAvsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAvsnitt.SelectedIndex < 0 || _currentAvsnitt == null)
                return;

            var av = _currentAvsnitt[listBoxAvsnitt.SelectedIndex];

            textBoxDetails.Text =
                $"Titel: {av.title}\r\n" +
                $"Publicerad: {av.publishDate}\r\n\r\n" +
                $"{av.description}";
        }

        private async void buttonSpara_Click(object sender, EventArgs e)
        {
            if (_currentFlode == null)
            {
                MessageBox.Show("Inget flöde hämtat ännu!");
                return;
            }

            // Validation is the pod already saved?
            if (_currentFlode.IsSaved)
            {
                MessageBox.Show("Den här podden är redan sparad.");
                return;
            }

            await _service.SparaPoddflodeAsync(_currentFlode);

            MessageBox.Show("Podden sparades!");
        }

        private void buttonVisaSparade_Click(object sender, EventArgs e)
        {
            var floden = _service.HamtaAllaFloden();

            listBoxSparade.Items.Clear();

            if (floden.Count == 0)
            {
                listBoxSparade.Items.Add("Inga sparade poddar.");
                return;
            }

            foreach (var f in floden)
            {
                listBoxSparade.Items.Add($"{f.displayName} ({f.rssUrl})");
            }
        }

        private void JamieForm_Load(object sender, EventArgs e)
        {

        }

        private void textBoxUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxDetails_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnTaBort_Click(object sender, EventArgs e)
        {
            if (listBoxSparade.SelectedIndex < 0)
            {
                MessageBox.Show("Välj ett sparat poddflöde att ta bort.");
                return;
            }


            string selected = listBoxSparade.SelectedItem.ToString();


            string rssUrl = selected.Substring(selected.IndexOf("(") + 1).TrimEnd(')');


            await _service.TaBortSparatFlodeAsync(rssUrl);

            MessageBox.Show("Poddflödet har tagits bort från dina sparade.");


            buttonVisaSparade_Click(null, null);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(linkLabel1.Text);
            MessageBox.Show("Kopierat till urklipp!");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(linkLabel2.Text);
            MessageBox.Show("Kopierat till urklipp!");
        }
    }
}
