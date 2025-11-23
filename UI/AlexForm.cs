using OruMongoDB.Core;
using OruMongoDB.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class AlexForm : Form
    {
        private readonly AlexKrav _service = new AlexKrav();
        private Poddflöden? _currentFlode;
        private List<PoddAvsnitt>? _currentAvsnitt;
        public AlexForm()
        {
            InitializeComponent();
        }

        private async void BtnAndraNamn_Click(object sender, EventArgs e)
        {
            {
                var id = txtPodId.Text;
                var nyttNamn = txtNyttNamn.Text;

                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(nyttNamn))
                {
                    MessageBox.Show("Fyll i både ID och nytt namn.");
                    return;
                }

                try
                {
                    await _service.AndraNamnPaPoddflodeAsync(id, nyttNamn);
                    MessageBox.Show("Namnet har uppdaterats!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fel: " + ex.Message);
                }
            }

        }

        private async void BtnSkapaNyttFlode_Click(object sender, EventArgs e)
        {
            var rssUrl = txtRssUrl.Text;
            var egetNamn = txtEgetNamn.Text;

            if (string.IsNullOrWhiteSpace(rssUrl) || string.IsNullOrWhiteSpace(egetNamn))
            {
                MessageBox.Show("Fyll i både RSS-URL och eget namn.");
                return;
            }

            try
            {
                var nyttFlode = await _service.SkapaNyttPoddflodeAsync(rssUrl, egetNamn);

                // Spara valt flöde i minnet (framtida funktionalitet)
                _currentFlode = nyttFlode;

                MessageBox.Show($"Poddflödet '{nyttFlode.displayName}' har lagts till!");

                // Om du vill visa det i en lista senare kan du lägga till:
                // listBoxFloden.Items.Add($"{nyttFlode.displayName} ({nyttFlode.rssUrl})");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel: " + ex.Message);
            }
        }

        private void AlexForm_Load(object sender, EventArgs e)
        {

        }

        private async void btnRaderaKategori_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKategoriId.Text))
            {
                MessageBox.Show("Du måste välja en kategori att radera.");
                return;
            }

            var resultat = MessageBox.Show(
                "Är du säker på att du vill radera denna kategori?",
                "Bekräfta radering",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (resultat == DialogResult.No)
                return;

            try
            {
                await _service.RaderaKategoriAsync(txtKategoriId.Text);
                MessageBox.Show("Kategorin har raderats.");
            }
            catch (Exception)
            {
                MessageBox.Show("Kategorin hittades inte.");
            }
        }


        private void txtRssUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void AlexForm_Load_1(object sender, EventArgs e)
        {

        }

        private void txtPodId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

