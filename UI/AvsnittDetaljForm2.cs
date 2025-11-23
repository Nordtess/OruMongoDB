using System;
using System.Windows.Forms;
using OruMongoDB.Domain;

namespace UI
{
    public partial class AvsnittDetaljForm : Form
    {
        private readonly PoddAvsnitt _avsnitt;

        public AvsnittDetaljForm(PoddAvsnitt avsnitt)
        {
            InitializeComponent();
            _avsnitt = avsnitt;
        }

        private void AvsnittDetaljForm_Load(object sender, EventArgs e)
        {
            lblTitel.Text = _avsnitt.title ?? "Ingen titel";
            lblDatum.Text = _avsnitt.publishDate ?? "Okänt datum";
            rtbBeskrivning.Text = _avsnitt.description ?? "Ingen beskrivning";
        }

        private void rtbBeskrivning_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
