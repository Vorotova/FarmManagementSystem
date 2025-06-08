using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmManagementSystem
{
    public static class FormStyleHelper
    {
        // Кольорова палітра
        public static readonly Color PrimaryColor = Color.FromArgb(34, 139, 34);
        public static readonly Color SecondaryColor = Color.FromArgb(76, 175, 80);
        public static readonly Color BackgroundColor = Color.FromArgb(240, 244, 248);
        public static readonly Color CardBackgroundColor = Color.White;
        public static readonly Color TextColor = Color.FromArgb(64, 64, 64);
        public static readonly Color AccentColor = Color.FromArgb(33, 150, 243);
        public static readonly Color SuccessColor = Color.FromArgb(76, 175, 80);
        public static readonly Color WarningColor = Color.FromArgb(255, 152, 0);
        public static readonly Color DangerColor = Color.FromArgb(244, 67, 54);

        // Шрифти
        public static readonly Font HeaderFont = new Font("Arial", 14F, FontStyle.Bold);
        public static readonly Font SubHeaderFont = new Font("Arial", 12F, FontStyle.Bold);
        public static readonly Font RegularFont = new Font("Arial", 10F, FontStyle.Regular);
        public static readonly Font SmallFont = new Font("Arial", 9F, FontStyle.Regular);

        public static void ApplyFormStyle(Form form)
        {
            form.BackColor = BackgroundColor;
            form.Font = RegularFont;
            form.StartPosition = FormStartPosition.CenterParent;
        }

        public static Button CreateStyledButton(string text, Color backColor, Size? size = null)
        {
            var button = new Button
            {
                Text = text,
                Size = size ?? new Size(120, 35),
                Font = RegularFont,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                Math.Min(255, backColor.R + 20),
                Math.Min(255, backColor.G + 20),
                Math.Min(255, backColor.B + 20)
            );

            return button;
        }

        public static DataGridView CreateStyledDataGridView()
        {
            var dgv = new DataGridView
            {
                BackgroundColor = CardBackgroundColor,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                GridColor = Color.FromArgb(230, 230, 230),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                Font = RegularFont
            };

            // Стиль заголовків
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = PrimaryColor;
            dgv.ColumnHeadersHeight = 40;

            // Стиль рядків
            dgv.DefaultCellStyle.BackColor = CardBackgroundColor;
            dgv.DefaultCellStyle.ForeColor = TextColor;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
            dgv.DefaultCellStyle.SelectionForeColor = TextColor;
            dgv.RowTemplate.Height = 35;

            // Альтернативні рядки
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            return dgv;
        }

        public static Panel CreateCardPanel()
        {
            var panel = new Panel
            {
                BackColor = CardBackgroundColor,
                Padding = new Padding(20)
            };

            // Додаємо тінь
            panel.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, panel.Width, panel.Height);
                using (var brush = new SolidBrush(Color.FromArgb(20, Color.Black)))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height));
                }
                using (var brush = new SolidBrush(CardBackgroundColor))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            };

            return panel;
        }

        public static Label CreateHeaderLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = HeaderFont,
                ForeColor = TextColor,
                AutoSize = true
            };
        }

        public static Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = RegularFont,
                ForeColor = TextColor,
                AutoSize = true
            };
        }

        public static TextBox CreateStyledTextBox()
        {
            var textBox = new TextBox
            {
                Font = RegularFont,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = CardBackgroundColor,
                ForeColor = TextColor
            };

            return textBox;
        }

        public static ComboBox CreateStyledComboBox()
        {
            var comboBox = new ComboBox
            {
                Font = RegularFont,
                BackColor = CardBackgroundColor,
                ForeColor = TextColor,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };

            return comboBox;
        }
    }
} 