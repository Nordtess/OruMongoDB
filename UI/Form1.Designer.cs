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
            btnCategories = new Button();
            BtnAbdi = new Button();
            SuspendLayout();
            // 
            // btnTest
            // 
            btnTest.Location = new Point(300, 290);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(75, 23);
            btnTest.TabIndex = 1;
            btnTest.Text = "Diyar press here";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(84, 21);
            rtbLog.Name = "rtbLog";
            rtbLog.Size = new Size(607, 153);
            rtbLog.TabIndex = 2;
            rtbLog.Text = "";
            rtbLog.TextChanged += richTextBox1_TextChanged;
            // 
            // btnJamie
            // 
            btnJamie.Location = new Point(583, 288);
            btnJamie.Name = "btnJamie";
            btnJamie.Size = new Size(75, 23);
            btnJamie.TabIndex = 3;
            btnJamie.Text = "Jamie Form";
            btnJamie.UseVisualStyleBackColor = true;
            btnJamie.Click += button1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(138, 290);
            button1.Margin = new Padding(2, 1, 2, 1);
            button1.Name = "button1";
            button1.Size = new Size(81, 22);
            button1.TabIndex = 4;
            button1.Text = "Alex";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // btnCategories
            // 
            btnCategories.Location = new Point(27, 328);
            btnCategories.Margin = new Padding(2, 1, 2, 1);
            btnCategories.Name = "btnCategories";
            btnCategories.Size = new Size(108, 23);
            btnCategories.TabIndex = 0;
            btnCategories.Text = "Kategorier";
            btnCategories.UseVisualStyleBackColor = true;
            btnCategories.Click += btnCategories_Click;
            // 
            // BtnAbdi
            // 
            BtnAbdi.Location = new Point(437, 288);
            BtnAbdi.Name = "BtnAbdi";
            BtnAbdi.Size = new Size(75, 23);
            BtnAbdi.TabIndex = 5;
            BtnAbdi.Text = "Abdi";
            BtnAbdi.UseVisualStyleBackColor = true;
            BtnAbdi.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(738, 351);
            Controls.Add(BtnAbdi);
            Controls.Add(btnCategories);
            Controls.Add(button1);
            Controls.Add(btnJamie);
            Controls.Add(rtbLog);
            Controls.Add(btnTest);
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
        private Button BtnAbdi;
    }
}
