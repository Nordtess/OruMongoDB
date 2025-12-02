using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

/*
 Summary:
 UI helpers for consistent dark theme styling of WinForms controls and safe link opening.
 Centralizes colors/visuals to keep the UI coherent, while business/data layers handle
 MongoDB Atlas persistence with async operations and transactions. Keeps the UI stable by
 avoiding unhandled exceptions from bubbling up.
*/

namespace UI
{
    internal static class UiHelpers
    {
        public static void ApplyTheme(
        Form form,
        GroupBox[] groups,
        Label[] labels,
        TextBox[] textBoxes,
        ListBox[] listBoxes,
        ComboBox[] comboBoxes,
        Button[] buttons,
        DataGridView grid,
        PictureBox? pictureBox = null)
        {
            // Palette
            Color bg = Color.FromArgb(22, 22, 22);
            Color panelBg = Color.FromArgb(30, 30, 30);
            Color borderGray = Color.FromArgb(55, 55, 55);
            Color btnGray = Color.FromArgb(45, 45, 45);
            Color btnHover = Color.FromArgb(60, 60, 60);
            Color btnDown = Color.FromArgb(35, 35, 35);
            Color textWhite = Color.White;

            // Form
            form.BackColor = bg;
            form.ForeColor = textWhite;
            form.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Group headers and borders
            foreach (var g in groups)
            {
                g.BackColor = panelBg;
                g.ForeColor = textWhite;
            }

            // Labels
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
                btn.FlatAppearance.BorderSize = 1;
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
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.DefaultCellStyle.BackColor = bg;
            grid.DefaultCellStyle.ForeColor = textWhite;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 50, 50);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.GridColor = Color.FromArgb(60, 60, 60);

            if (pictureBox != null)
            {
                pictureBox.BackColor = bg;
            }
        }

        // Fire-and-forget safe link opener for the UI (errors are intentionally swallowed)
        public static void OpenLink(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return; // no-op for invalid input
            try
            {
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
            catch
            {
                // Intentionally ignored: callers handle user messaging to keep UI resilient
            }
        }
    }
}
