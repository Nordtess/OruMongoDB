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
            textBoxDetails = new TextBox();
            buttonSpara = new Button();
            buttonVisaSparade = new Button();
            listBoxSparade = new ListBox();
            SuspendLayout();
            
            textBoxUrl.Location = new Point(161, 97);
            textBoxUrl.Name = "textBoxUrl";
            textBoxUrl.Size = new Size(292, 23);
            textBoxUrl.TabIndex = 0;
            
            listBoxAvsnitt.FormattingEnabled = true;
            listBoxAvsnitt.Location = new Point(162, 157);
            listBoxAvsnitt.Name = "listBoxAvsnitt";
            listBoxAvsnitt.Size = new Size(120, 94);
            listBoxAvsnitt.TabIndex = 1;
            listBoxAvsnitt.SelectedIndexChanged += listBoxAvsnitt_SelectedIndexChanged;
            
            buttonHamta.Location = new Point(493, 100);
            buttonHamta.Name = "buttonHamta";
            buttonHamta.Size = new Size(75, 23);
            buttonHamta.TabIndex = 2;
            buttonHamta.Text = "Hämta";
            buttonHamta.UseVisualStyleBackColor = true;
            buttonHamta.Click += buttonHamta_Click;
            
            textBoxDetails.Location = new Point(319, 157);
            textBoxDetails.Multiline = true;
            textBoxDetails.Name = "textBoxDetails";
            textBoxDetails.ScrollBars = ScrollBars.Vertical;
            textBoxDetails.Size = new Size(261, 135);
            textBoxDetails.TabIndex = 3;
            
            buttonSpara.Location = new Point(170, 339);
            buttonSpara.Name = "buttonSpara";
            buttonSpara.Size = new Size(75, 23);
            buttonSpara.TabIndex = 4;
            buttonSpara.Text = "Spara flöde";
            buttonSpara.UseVisualStyleBackColor = true;
            buttonSpara.Click += buttonSpara_Click;
            
            buttonVisaSparade.Location = new Point(274, 339);
            buttonVisaSparade.Name = "buttonVisaSparade";
            buttonVisaSparade.Size = new Size(155, 23);
            buttonVisaSparade.TabIndex = 5;
            buttonVisaSparade.Text = "Visa sparade flöden";
            buttonVisaSparade.UseVisualStyleBackColor = true;
            buttonVisaSparade.Click += buttonVisaSparade_Click;
            
            listBoxSparade.FormattingEnabled = true;
            listBoxSparade.Location = new Point(493, 339);
            listBoxSparade.Name = "listBoxSparade";
            listBoxSparade.Size = new Size(120, 94);
            listBoxSparade.TabIndex = 6;
            
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBoxSparade);
            Controls.Add(buttonVisaSparade);
            Controls.Add(buttonSpara);
            Controls.Add(textBoxDetails);
            Controls.Add(buttonHamta);
            Controls.Add(listBoxAvsnitt);
            Controls.Add(textBoxUrl);
            Name = "JamieForm";
            Text = "JamieForm";
            
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private TextBox textBoxUrl;
        private ListBox listBoxAvsnitt;
        private Button buttonHamta;
        private TextBox textBoxDetails;
        private Button buttonSpara;
        private Button buttonVisaSparade;
        private ListBox listBoxSparade;
    }
}