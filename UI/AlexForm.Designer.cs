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
            SuspendLayout();
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 10F);
            lblId.Location = new Point(30, 30);
            lblId.Name = "lblId";
            lblId.Size = new Size(91, 19);
            lblId.TabIndex = 0;
            lblId.Text = "Poddflöde ID:";
            // 
            // txtPodId
            // 
            txtPodId.Font = new Font("Segoe UI", 10F);
            txtPodId.Location = new Point(150, 27);
            txtPodId.Name = "txtPodId";
            txtPodId.Size = new Size(280, 25);
            txtPodId.TabIndex = 1;
            // 
            // lblNyttNamn
            // 
            lblNyttNamn.AutoSize = true;
            lblNyttNamn.Font = new Font("Segoe UI", 10F);
            lblNyttNamn.Location = new Point(30, 80);
            lblNyttNamn.Name = "lblNyttNamn";
            lblNyttNamn.Size = new Size(78, 19);
            lblNyttNamn.TabIndex = 2;
            lblNyttNamn.Text = "Nytt namn:";
            // 
            // txtNyttNamn
            // 
            txtNyttNamn.Font = new Font("Segoe UI", 10F);
            txtNyttNamn.Location = new Point(150, 77);
            txtNyttNamn.Name = "txtNyttNamn";
            txtNyttNamn.Size = new Size(280, 25);
            txtNyttNamn.TabIndex = 3;
            // 
            // btnAndraNamn
            // 
            btnAndraNamn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAndraNamn.Location = new Point(150, 112);
            btnAndraNamn.Margin = new Padding(2, 1, 2, 1);
            btnAndraNamn.Name = "btnAndraNamn";
            btnAndraNamn.Size = new Size(140, 35);
            btnAndraNamn.TabIndex = 4;
            btnAndraNamn.Text = "Ändra namn";
            btnAndraNamn.UseVisualStyleBackColor = true;
            btnAndraNamn.Click += BtnAndraNamn_Click;
            // 
            // lblRssUrl
            // 
            lblRssUrl.AutoSize = true;
            lblRssUrl.Font = new Font("Segoe UI", 10F);
            lblRssUrl.Location = new Point(30, 164);
            lblRssUrl.Margin = new Padding(2, 0, 2, 0);
            lblRssUrl.Name = "lblRssUrl";
            lblRssUrl.Size = new Size(65, 19);
            lblRssUrl.TabIndex = 5;
            lblRssUrl.Text = "RSS-URL:";
            // 
            // txtRssUrl
            // 
            txtRssUrl.Font = new Font("Segoe UI", 10F);
            txtRssUrl.Location = new Point(150, 161);
            txtRssUrl.Margin = new Padding(2, 1, 2, 1);
            txtRssUrl.Name = "txtRssUrl";
            txtRssUrl.Size = new Size(280, 25);
            txtRssUrl.TabIndex = 6;
            // 
            // lblEgetNamn
            // 
            lblEgetNamn.AutoSize = true;
            lblEgetNamn.Font = new Font("Segoe UI", 10F);
            lblEgetNamn.Location = new Point(30, 211);
            lblEgetNamn.Margin = new Padding(2, 0, 2, 0);
            lblEgetNamn.Name = "lblEgetNamn";
            lblEgetNamn.Size = new Size(78, 19);
            lblEgetNamn.TabIndex = 7;
            lblEgetNamn.Text = "Eget namn:";
            // 
            // txtEgetNamn
            // 
            txtEgetNamn.Font = new Font("Segoe UI", 10F);
            txtEgetNamn.Location = new Point(150, 208);
            txtEgetNamn.Margin = new Padding(2, 1, 2, 1);
            txtEgetNamn.Name = "txtEgetNamn";
            txtEgetNamn.Size = new Size(280, 25);
            txtEgetNamn.TabIndex = 8;
            // 
            // btnSkapaNyttFlode
            // 
            btnSkapaNyttFlode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSkapaNyttFlode.Location = new Point(150, 255);
            btnSkapaNyttFlode.Margin = new Padding(2, 1, 2, 1);
            btnSkapaNyttFlode.Name = "btnSkapaNyttFlode";
            btnSkapaNyttFlode.Size = new Size(198, 35);
            btnSkapaNyttFlode.TabIndex = 9;
            btnSkapaNyttFlode.Text = "Skapa och ge eget namn";
            btnSkapaNyttFlode.Click += BtnSkapaNyttFlode_Click;
            // 
            // AlexForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 312);
            Controls.Add(btnAndraNamn);
            Controls.Add(lblRssUrl);
            Controls.Add(txtRssUrl);
            Controls.Add(lblEgetNamn);
            Controls.Add(txtEgetNamn);
            Controls.Add(btnSkapaNyttFlode);
            Controls.Add(txtNyttNamn);
            Controls.Add(lblNyttNamn);
            Controls.Add(txtPodId);
            Controls.Add(lblId);
            Name = "AlexForm";
            Text = "Ändra namn på poddflöde";
            Load += AlexForm_Load;
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

    }
}
