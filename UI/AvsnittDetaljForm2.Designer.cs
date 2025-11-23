namespace UI
{
    partial class AvsnittDetaljForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitel = new Label();
            lblDatum = new Label();
            rtbBeskrivning = new RichTextBox();
            SuspendLayout();
            // 
            // lblTitel
            // 
            lblTitel.Location = new Point(0, 0);
            lblTitel.Name = "lblTitel";
            lblTitel.Size = new Size(100, 23);
            lblTitel.TabIndex = 2;
            // 
            // lblDatum
            // 
            lblDatum.Location = new Point(0, 0);
            lblDatum.Name = "lblDatum";
            lblDatum.Size = new Size(100, 23);
            lblDatum.TabIndex = 1;
            // 
            // rtbBeskrivning
            // 
            rtbBeskrivning.Location = new Point(0, 0);
            rtbBeskrivning.Name = "rtbBeskrivning";
            rtbBeskrivning.Size = new Size(287, 260);
            rtbBeskrivning.TabIndex = 0;
            rtbBeskrivning.Text = "";
            // 
            // AvsnittDetaljForm
            // 
            ClientSize = new Size(284, 261);
            Controls.Add(rtbBeskrivning);
            Controls.Add(lblDatum);
            Controls.Add(lblTitel);
            Name = "AvsnittDetaljForm";
            Text = "Avsnittsdetaljer";
            Load += AvsnittDetaljForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitel;
        private Label lblDatum;
        private RichTextBox rtbBeskrivning;
    }
}
