namespace UI
{
    partial class AbdiForm2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            tbUrl = new TextBox();
            btnhämta = new Button();
            listboxAvsnitt1 = new ListBox();
            visabeskrivning = new RichTextBox();
            lblKalla = new Label();
            cmbKalla = new ComboBox();
            listboxFlodenDb = new ListBox();
            lblSparade = new Label();
            linkLabel1 = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 26);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "Ange URL";
            // 
            // tbUrl
            // 
            tbUrl.Location = new Point(23, 66);
            tbUrl.Name = "tbUrl";
            tbUrl.Size = new Size(194, 23);
            tbUrl.TabIndex = 1;
            tbUrl.TextChanged += tbUrl_TextChanged;
            // 
            // btnhämta
            // 
            btnhämta.Location = new Point(23, 95);
            btnhämta.Name = "btnhämta";
            btnhämta.Size = new Size(194, 23);
            btnhämta.TabIndex = 2;
            btnhämta.Text = "Hämta avsnitt";
            btnhämta.UseVisualStyleBackColor = true;
            btnhämta.Click += btnhämta_Click;
            // 
            // listboxAvsnitt1
            // 
            listboxAvsnitt1.FormattingEnabled = true;
            listboxAvsnitt1.Location = new Point(23, 180);
            listboxAvsnitt1.Name = "listboxAvsnitt1";
            listboxAvsnitt1.Size = new Size(250, 199);
            listboxAvsnitt1.TabIndex = 3;
            listboxAvsnitt1.SelectedIndexChanged += listboxAvsnitt1_SelectedIndexChanged;
            listboxAvsnitt1.DoubleClick += listboxAvsnitt1_DoubleClick;
            // 
            // visabeskrivning
            // 
            visabeskrivning.Location = new Point(296, 180);
            visabeskrivning.Name = "visabeskrivning";
            visabeskrivning.Size = new Size(300, 199);
            visabeskrivning.TabIndex = 4;
            visabeskrivning.Text = "";
            visabeskrivning.TextChanged += visabeskrivning_TextChanged;
            // 
            // lblKalla
            // 
            lblKalla.AutoSize = true;
            lblKalla.Location = new Point(296, 26);
            lblKalla.Name = "lblKalla";
            lblKalla.Size = new Size(32, 15);
            lblKalla.TabIndex = 5;
            lblKalla.Text = "Källa";
            // 
            // cmbKalla
            // 
            cmbKalla.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKalla.Location = new Point(296, 46);
            cmbKalla.Name = "cmbKalla";
            cmbKalla.Size = new Size(210, 23);
            cmbKalla.TabIndex = 6;
            cmbKalla.SelectedIndexChanged += cmbKalla_SelectedIndexChanged;
            // 
            // listboxFlodenDb
            // 
            listboxFlodenDb.FormattingEnabled = true;
            listboxFlodenDb.Location = new Point(620, 66);
            listboxFlodenDb.Name = "listboxFlodenDb";
            listboxFlodenDb.Size = new Size(250, 139);
            listboxFlodenDb.TabIndex = 7;
            // 
            // lblSparade
            // 
            lblSparade.AutoSize = true;
            lblSparade.Location = new Point(620, 26);
            lblSparade.Name = "lblSparade";
            lblSparade.Size = new Size(155, 15);
            lblSparade.TabIndex = 8;
            lblSparade.Text = "Sparade poddar (MongoDB)";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(625, 226);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(234, 15);
            linkLabel1.TabIndex = 9;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://anchor.fm/s/d49ab0d0/podcast/rss";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked_1;
            // 
            // AbdiForm2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 420);
            Controls.Add(lblSparade);
            Controls.Add(listboxFlodenDb);
            Controls.Add(cmbKalla);
            Controls.Add(lblKalla);
            Controls.Add(visabeskrivning);
            Controls.Add(listboxAvsnitt1);
            Controls.Add(btnhämta);
            Controls.Add(tbUrl);
            Controls.Add(label1);
            Controls.Add(linkLabel1);
            Name = "AbdiForm2";
            Text = "Abdi – Avsnittsläsare";
            Load += AbdiForm2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbUrl;
        private Button btnhämta;
        private ListBox listboxAvsnitt1;
        private RichTextBox visabeskrivning;

        private Label lblKalla;
        private ComboBox cmbKalla;

        private ListBox listboxFlodenDb;
        private Label lblSparade;
        private LinkLabel linkLabel1;
    }
}
