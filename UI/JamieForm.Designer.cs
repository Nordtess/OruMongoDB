namespace UI
{
    partial class JamieForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxUrl = new TextBox();
            listBoxAvsnitt = new ListBox();
            buttonHamta = new Button();
            buttonSpara = new Button();
            buttonVisaSparade = new Button();
            listBoxSparade = new ListBox();
            label1 = new Label();
            label2 = new Label();
            btnTaBort = new Button();
            textBoxDetails = new TextBox();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            linkLabel2 = new LinkLabel();
            SuspendLayout();
            // 
            // textBoxUrl
            // 
            textBoxUrl.Location = new Point(211, 49);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(292, 23);
            textBoxUrl.TabIndex = 0;
            textBoxUrl.Text = "Enter URL";
            textBoxUrl.TextChanged += textBoxUrl_TextChanged;
            // 
            // listBoxAvsnitt
            // 
            listBoxAvsnitt.FormattingEnabled = true;
            listBoxAvsnitt.Location = new Point(162, 157);
            listBoxAvsnitt.Name = "listBoxAvsnitt";
            listBoxAvsnitt.Size = new Size(120, 94);
            listBoxAvsnitt.TabIndex = 1;
            listBoxAvsnitt.SelectedIndexChanged += listBoxAvsnitt_SelectedIndexChanged;
            // 
            // buttonHamta
            // 
            buttonHamta.Location = new Point(509, 49);
            buttonHamta.Name = "buttonHamta";
            buttonHamta.Size = new Size(75, 23);
            buttonHamta.TabIndex = 2;
            buttonHamta.Text = "Hämta";
            buttonHamta.UseVisualStyleBackColor = true;
            buttonHamta.Click += buttonHamta_Click;
            // 
            // buttonSpara
            // 
            buttonSpara.Location = new Point(162, 257);
            buttonSpara.Name = "buttonSpara";
            buttonSpara.Size = new Size(75, 23);
            buttonSpara.TabIndex = 4;
            buttonSpara.Text = "Spara flöde";
            buttonSpara.UseVisualStyleBackColor = true;
            buttonSpara.Click += buttonSpara_Click;
            // 
            // buttonVisaSparade
            // 
            buttonVisaSparade.Location = new Point(383, 257);
            buttonVisaSparade.Name = "buttonVisaSparade";
            buttonVisaSparade.Size = new Size(155, 23);
            buttonVisaSparade.TabIndex = 5;
            buttonVisaSparade.Text = "Visa sparade flöden";
            buttonVisaSparade.UseVisualStyleBackColor = true;
            buttonVisaSparade.Click += buttonVisaSparade_Click;
            // 
            // listBoxSparade
            // 
            listBoxSparade.FormattingEnabled = true;
            listBoxSparade.Location = new Point(383, 157);
            listBoxSparade.Name = "listBoxSparade";
            listBoxSparade.Size = new Size(252, 94);
            listBoxSparade.TabIndex = 6;
            listBoxSparade.SelectedIndexChanged += listBoxSparade_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(162, 139);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 7;
            label1.Text = "Alla avsnitt";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(383, 139);
            label2.Name = "label2";
            label2.Size = new Size(119, 15);
            label2.TabIndex = 8;
            label2.Text = "Mina sparade poddar";
            // 
            // btnTaBort
            // 
            btnTaBort.Location = new Point(544, 257);
            btnTaBort.Name = "btnTaBort";
            btnTaBort.Size = new Size(91, 23);
            btnTaBort.TabIndex = 9;
            btnTaBort.Text = "Ta bort flöde";
            btnTaBort.UseVisualStyleBackColor = true;
            btnTaBort.Click += btnTaBort_Click;
            // 
            // textBoxDetails
            // 
            textBoxDetails.Location = new Point(632, 371);
            textBoxDetails.Multiline = true;
            textBoxDetails.Name = "textBoxDetails";
            textBoxDetails.ScrollBars = ScrollBars.Vertical;
            textBoxDetails.Size = new Size(121, 57);
            textBoxDetails.TabIndex = 3;
            textBoxDetails.TextChanged += textBoxDetails_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 75);
            label3.Name = "label3";
            label3.Size = new Size(84, 15);
            label3.TabIndex = 10;
            label3.Text = "Example URL'S";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(15, 90);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(186, 15);
            linkLabel1.TabIndex = 11;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "https://example.com/techpod/rss";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(15, 105);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(169, 15);
            linkLabel2.TabIndex = 12;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "https://example.com/news/rss";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // JamieForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(label3);
            Controls.Add(btnTaBort);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listBoxSparade);
            Controls.Add(buttonVisaSparade);
            Controls.Add(buttonSpara);
            Controls.Add(textBoxDetails);
            Controls.Add(buttonHamta);
            Controls.Add(listBoxAvsnitt);
            Controls.Add(textBoxUrl);
            Name = "JamieForm";
            Text = "JamieForm";
            Load += JamieForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private TextBox textBoxUrl;
        private ListBox listBoxAvsnitt;
        private Button buttonHamta;
        private Button buttonSpara;
        private Button buttonVisaSparade;
        private ListBox listBoxSparade;
        private Label label1;
        private Label label2;
        private Button btnTaBort;
        private TextBox textBoxDetails;
        private Label label3;
        private LinkLabel linkLabel1;
        private LinkLabel linkLabel2;
    }
}