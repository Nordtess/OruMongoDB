namespace UI
{
    partial class AlexForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Metoden krävs för Windows Form Designer-support.
        /// </summary>
        private void InitializeComponent()
        {
            lblId = new Label();
            txtPodId = new TextBox();
            lblNyttNamn = new Label();
            txtNyttNamn = new TextBox();
            btnAndraNamn = new Button();
            lblRssUrl = new Label();
            txtRssUrl = new TextBox();
            lblEgetNamn = new Label();
            txtEgetNamn = new TextBox();
            btnSkapaNyttFlode = new Button();
            lblKategoriId = new Label();
            txtKategoriId = new TextBox();
            btnRaderaKategori = new Button();
            SuspendLayout();
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 10F);
            lblId.Location = new Point(40, 20);
            lblId.Name = "lblId";
            lblId.Size = new Size(91, 19);
            lblId.TabIndex = 0;
            lblId.Text = "Poddflöde ID:";
            // 
            // txtPodId
            // 
            txtPodId.Font = new Font("Segoe UI", 10F);
            txtPodId.Location = new Point(240, 20);
            txtPodId.Name = "txtPodId";
            txtPodId.Size = new Size(400, 43);
            txtPodId.TabIndex = 1;
            // 
            // lblNyttNamn
            // 
            lblNyttNamn.AutoSize = true;
            lblNyttNamn.Font = new Font("Segoe UI", 10F);
            lblNyttNamn.Location = new Point(40, 80);
            lblNyttNamn.Name = "lblNyttNamn";
            lblNyttNamn.Size = new Size(78, 19);
            lblNyttNamn.TabIndex = 2;
            lblNyttNamn.Text = "Nytt namn:";
            // 
            // txtNyttNamn
            // 
            txtNyttNamn.Font = new Font("Segoe UI", 10F);
            txtNyttNamn.Location = new Point(240, 80);
            txtNyttNamn.Name = "txtNyttNamn";
            txtNyttNamn.Size = new Size(400, 43);
            txtNyttNamn.TabIndex = 3;
            // 
            // btnAndraNamn
            // 
            btnAndraNamn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAndraNamn.Location = new Point(240, 110);
            btnAndraNamn.Name = "btnAndraNamn";
            btnAndraNamn.Size = new Size(230, 50);
            btnAndraNamn.TabIndex = 4;
            btnAndraNamn.Text = "Ändra namn";
            btnAndraNamn.Click += BtnAndraNamn_Click;
            // 
            // lblRssUrl
            // 
            lblRssUrl.AutoSize = true;
            lblRssUrl.Font = new Font("Segoe UI", 10F);
            lblRssUrl.Location = new Point(40, 200);
            lblRssUrl.Name = "lblRssUrl";
            lblRssUrl.Size = new Size(65, 19);
            lblRssUrl.TabIndex = 5;
            lblRssUrl.Text = "RSS-URL:";
            // 
            // txtRssUrl
            // 
            txtRssUrl.Font = new Font("Segoe UI", 10F);
            txtRssUrl.Location = new Point(240, 200);
            txtRssUrl.Name = "txtRssUrl";
            txtRssUrl.Size = new Size(400, 43);
            txtRssUrl.TabIndex = 6;
            // 
            // lblEgetNamn
            // 
            lblEgetNamn.AutoSize = true;
            lblEgetNamn.Font = new Font("Segoe UI", 10F);
            lblEgetNamn.Location = new Point(40, 260);
            lblEgetNamn.Name = "lblEgetNamn";
            lblEgetNamn.Size = new Size(78, 19);
            lblEgetNamn.TabIndex = 7;
            lblEgetNamn.Text = "Eget namn:";
            // 
            // txtEgetNamn
            // 
            txtEgetNamn.Font = new Font("Segoe UI", 10F);
            txtEgetNamn.Location = new Point(240, 260);
            txtEgetNamn.Name = "txtEgetNamn";
            txtEgetNamn.Size = new Size(400, 43);
            txtEgetNamn.TabIndex = 8;
            // 
            // btnSkapaNyttFlode
            // 
            btnSkapaNyttFlode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSkapaNyttFlode.Location = new Point(240, 290);
            btnSkapaNyttFlode.Name = "btnSkapaNyttFlode";
            btnSkapaNyttFlode.Size = new Size(300, 50);
            btnSkapaNyttFlode.TabIndex = 9;
            btnSkapaNyttFlode.Text = "Skapa och ge eget namn";
            btnSkapaNyttFlode.Click += BtnSkapaNyttFlode_Click;
            // 
            // lblKategoriId
            // 
            lblKategoriId.AutoSize = true;
            lblKategoriId.Font = new Font("Segoe UI", 10F);
            lblKategoriId.Location = new Point(40, 380);
            lblKategoriId.Name = "lblKategoriId";
            lblKategoriId.Size = new Size(157, 37);
            lblKategoriId.TabIndex = 10;
            lblKategoriId.Text = "Kategori ID:";
            // 
            // txtKategoriId
            // 
            txtKategoriId.Font = new Font("Segoe UI", 10F);
            txtKategoriId.Location = new Point(240, 380);
            txtKategoriId.Name = "txtKategoriId";
            txtKategoriId.Size = new Size(400, 43);
            txtKategoriId.TabIndex = 11;
            // 
            // btnRaderaKategori
            // 
            btnRaderaKategori.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRaderaKategori.Location = new Point(240, 410);
            btnRaderaKategori.Name = "btnRaderaKategori";
            btnRaderaKategori.Size = new Size(230, 50);
            btnRaderaKategori.TabIndex = 12;
            btnRaderaKategori.Text = "Radera kategori";
            btnRaderaKategori.Click += btnRaderaKategori_Click;
            // 
            // AlexForm
            // 
            ClientSize = new Size(700, 520);
            Controls.Add(lblId);
            Controls.Add(txtPodId);
            Controls.Add(lblNyttNamn);
            Controls.Add(txtNyttNamn);
            Controls.Add(btnAndraNamn);
            Controls.Add(lblRssUrl);
            Controls.Add(txtRssUrl);
            Controls.Add(lblEgetNamn);
            Controls.Add(txtEgetNamn);
            Controls.Add(btnSkapaNyttFlode);
            Controls.Add(lblKategoriId);
            Controls.Add(txtKategoriId);
            Controls.Add(btnRaderaKategori);
            Name = "AlexForm";
            StartPosition = FormStartPosition.CenterScreen;
            Load += AlexForm_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }






        #endregion

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtPodId;
        private System.Windows.Forms.Label lblNyttNamn;
        private System.Windows.Forms.TextBox txtNyttNamn;
        private System.Windows.Forms.Button btnAndraNamn;
        private System.Windows.Forms.Label lblRssUrl;
        private System.Windows.Forms.TextBox txtRssUrl;
        private System.Windows.Forms.Label lblEgetNamn;
        private System.Windows.Forms.TextBox txtEgetNamn;
        private System.Windows.Forms.Button btnSkapaNyttFlode;
        private System.Windows.Forms.TextBox txtKategoriId;
        private System.Windows.Forms.Button btnRaderaKategori;
        private System.Windows.Forms.Label lblKategoriId;



    }
}
