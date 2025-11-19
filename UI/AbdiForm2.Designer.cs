namespace UI
{
    partial class AbdiForm2
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
            label1 = new Label();
            tbUrl = new TextBox();
            btnhämta = new Button();
            listboxAvsnitt1 = new ListBox();
            visabeskrivning = new RichTextBox();
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
            // 
            // btnhämta
            //
            // 
            btnhämta.Location = new Point(23, 95);
            btnhämta.Name = "btnhämta";
            btnhämta.Size = new Size(194, 23);
            btnhämta.TabIndex = 2;
            btnhämta.Text = "Hämta och visa beskrivning";
            btnhämta.UseVisualStyleBackColor = true;
            btnhämta.Click += btnhämta_Click;
            // 
            // listboxAvsnitt1
            // 
            listboxAvsnitt1.FormattingEnabled = true;
            listboxAvsnitt1.Location = new Point(12, 151);
            listboxAvsnitt1.Name = "listboxAvsnitt1";
            listboxAvsnitt1.Size = new Size(223, 139);
            listboxAvsnitt1.TabIndex = 3;
            listboxAvsnitt1.SelectedIndexChanged += listboxAvsnitt1_SelectedIndexChanged;
            listboxAvsnitt1.DoubleClick += listboxAvsnitt1_DoubleClick;
            // 
            // visabeskrivning
            // 
            visabeskrivning.Location = new Point(296, 140);
            visabeskrivning.Name = "visabeskrivning";
            visabeskrivning.Size = new Size(211, 150);
            visabeskrivning.TabIndex = 4;
            visabeskrivning.Text = "";
            visabeskrivning.TextChanged += visabeskrivning_TextChanged;
            // 
            // AbdiForm2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(618, 311);
            Controls.Add(visabeskrivning);
            Controls.Add(listboxAvsnitt1);
            Controls.Add(btnhämta);
            Controls.Add(tbUrl);
            Controls.Add(label1);
            Name = "AbdiForm2";
            Text = "AbdiForm2";
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
    }
}