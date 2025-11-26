using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace UI
{
 internal static class UiHelpers
 {
 public static void ApplyTheme(Form form,
 GroupBox[] groups,
 Label[] labels,
 TextBox[] textBoxes,
 ListBox[] listBoxes,
 ComboBox[] comboBoxes,
 Button[] buttons,
 DataGridView grid,
 PictureBox? pictureBox = null)
 {
 // Base colors
 Color bg = Color.FromArgb(22,22,22);
 Color panelBg = Color.FromArgb(30,30,30);
 Color borderGray = Color.FromArgb(55,55,55);
 Color btnGray = Color.FromArgb(45,45,45);
 Color btnHover = Color.FromArgb(60,60,60);
 Color btnDown = Color.FromArgb(35,35,35);

 // Make all text bright white as requested
 Color textWhite = Color.White;
 Color textGray = Color.FromArgb(200,200,200); // keep if needed elsewhere

 form.BackColor = bg;
 form.ForeColor = textWhite;
 form.Font = new Font("Segoe UI",9F, FontStyle.Regular);

 // Group headers and borders
 foreach (var g in groups)
 {
 g.BackColor = panelBg;
 g.ForeColor = textWhite; // bright white titles (My podcasts, Episode list, Categories)
 }

 // Labels (e.g., RSS URL and other captions) -> bright white
 foreach (var lbl in labels)
 {
 lbl.ForeColor = textWhite;
 }

 // Inputs and lists
 foreach (var tb in textBoxes)
 {
 tb.BackColor = panelBg;
 tb.ForeColor = textWhite;
 tb.BorderStyle = BorderStyle.FixedSingle;
 }
 foreach (var lb in listBoxes)
 {
 lb.BackColor = bg;
 lb.ForeColor = textWhite;
 lb.BorderStyle = BorderStyle.FixedSingle;
 }
 foreach (var cb in comboBoxes)
 {
 cb.BackColor = panelBg;
 cb.ForeColor = textWhite;
 cb.FlatStyle = FlatStyle.Flat;
 }

 // Buttons
 foreach (var btn in buttons)
 {
 btn.FlatStyle = FlatStyle.Flat;
 btn.FlatAppearance.BorderSize =1;
 btn.FlatAppearance.BorderColor = borderGray;
 btn.BackColor = btnGray;
 btn.ForeColor = textWhite;
 btn.FlatAppearance.MouseOverBackColor = btnHover;
 btn.FlatAppearance.MouseDownBackColor = btnDown;
 btn.Cursor = Cursors.Hand;
 }

 // Data grid styling
 grid.BackgroundColor = bg;
 grid.BorderStyle = BorderStyle.None;
 grid.EnableHeadersVisualStyles = false;
 grid.ColumnHeadersDefaultCellStyle.BackColor = panelBg;
 grid.ColumnHeadersDefaultCellStyle.ForeColor = textWhite;
 grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI",9F, FontStyle.Bold);
 grid.DefaultCellStyle.BackColor = bg;
 grid.DefaultCellStyle.ForeColor = textWhite;
 grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50,50,50);
 grid.DefaultCellStyle.SelectionForeColor = Color.White;
 grid.GridColor = Color.FromArgb(60,60,60);

 if (pictureBox != null)
 pictureBox.BackColor = bg;
 }

 public static void OpenLink(string url)
 {
 try
 {
 Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
 }
 catch
 {
 // swallow; UI can display message separately
 }
 }
 }
}
