using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;
using OruMongoDB.Core;
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


                if (string.IsNullOrWhiteSpace(url))
                {
                    MessageBox.Show("Skriv in en RSS-URL först!",
                        "Validering", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                MessageBox.Show(
                    $"Hämtade {_currentAvsnitt.Count} avsnitt från {result.Flode.displayName}",
                    "Klart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Valideringsfel",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid hämtning: " + ex.Message,
                    "Tekniskt fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                if (_currentFlode == null)
                {
                    MessageBox.Show("Inget flöde hämtat ännu!",
                        "Validering", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_currentFlode.IsSaved)
                {
                    MessageBox.Show("Den här podden är redan sparad i ditt register.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                await _service.SparaPoddflodeAsync(_currentFlode);


                _currentFlode.IsSaved = true;
                _currentFlode.SavedAt = DateTime.UtcNow;

                MessageBox.Show("Poddflödet har sparats i ditt register!",
                    "Klart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Valideringsfel",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid sparande: " + ex.Message,
                    "Tekniskt fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonVisaSparade_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid läsning av sparade poddar: " + ex.Message,
                    "Tekniskt fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnTaBort_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxSparade.SelectedIndex < 0)
                {
                    MessageBox.Show("Välj ett sparat poddflöde att ta bort.",
                        "Validering", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selected = listBoxSparade.SelectedItem.ToString() ?? string.Empty;


                if (!selected.Contains("(") || !selected.Contains(")"))
                {
                    MessageBox.Show("Det finns inget riktigt poddflöde att ta bort.",
                        "Validering", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                string rssUrl = selected.Substring(selected.IndexOf("(") + 1).TrimEnd(')');


                await _service.TaBortSparatFlodeAsync(rssUrl);


                if (_currentFlode != null && _currentFlode.rssUrl == rssUrl)
                {
                    _currentFlode.IsSaved = false;
                    _currentFlode.SavedAt = null;
                }

                MessageBox.Show("Poddflödet har tagits bort från ditt register.",
                    "Klart", MessageBoxButtons.OK, MessageBoxIcon.Information);


                buttonVisaSparade_Click(null, null);
            }
            catch (ValidationException vex)
            {
                MessageBox.Show(vex.Message, "Valideringsfel",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid borttagning: " + ex.Message,
                    "Tekniskt fel", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void listBoxSparade_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
