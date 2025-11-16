namespace UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTest = new Button();
            rtbLog = new RichTextBox();
            btnJamie = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnTest
            // 
            btnTest.Location = new Point(613, 614);
            btnTest.Margin = new Padding(6);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(139, 49);
            btnTest.TabIndex = 1;
            btnTest.Text = "Diyar press here";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(156, 45);
            rtbLog.Margin = new Padding(6);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(1124, 322);
            rtbLog.TabIndex = 2;
            rtbLog.Text = "";
            rtbLog.TextChanged += richTextBox1_TextChanged;
            // 
            // btnJamie
            // 
            btnJamie.Location = new Point(1016, 608);
            btnJamie.Margin = new Padding(6);
            btnJamie.Name = "btnJamie";
            btnJamie.Size = new Size(139, 49);
            btnJamie.TabIndex = 3;
            btnJamie.Text = "Jamie Form";
            btnJamie.UseVisualStyleBackColor = true;
            btnJamie.Click += button1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(257, 618);
            button1.Name = "button1";
            button1.Size = new Size(150, 46);
            button1.TabIndex = 4;
            button1.Text = "Alex";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;

            btnCategories = new Button();
            btnCategories.Location = new Point(50, 700);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(200, 50);
            btnCategories.Text = "Kategorier";
            btnCategories.UseVisualStyleBackColor = true;
            btnCategories.Click += btnCategories_Click;
            this.Controls.Add(btnCategories);
            // 
            // Form1
            // 
            // Form1
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1486, 960);

            // Lägg till ALLA knappar
            this.Controls.Add(btnCategories);
            this.Controls.Add(button1);
            this.Controls.Add(btnJamie);
            this.Controls.Add(rtbLog);
            this.Controls.Add(btnTest);

            Margin = new Padding(6);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);

        }

        #endregion
        private Button btnTest;
        private RichTextBox rtbLog;
        private Button btnJamie;
        private Button button1;
        private Button btnCategories;
    }
}
