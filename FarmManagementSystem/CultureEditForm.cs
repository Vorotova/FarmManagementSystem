using System;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;

namespace FarmManagementSystem
{
    public class CultureEditForm : Form
    {
        public Culture Culture { get; private set; }
        private TextBox txtName;
        private ComboBox cmbSeasonality;
        private NumericUpDown numAverageYield;
        private Button btnSave;
        private Button btnCancel;
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public CultureEditForm(Culture culture = null)
        {
            isEditMode = culture != null;
            Culture = culture != null ? new Culture
            {
                Id = culture.Id,
                Name = culture.Name,
                Seasonality = culture.Seasonality,
                AverageYield = culture.AverageYield
            } : new Culture();
            InitializeComponent();
            if (isEditMode) LoadCulture();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування культури" : "Додавання культури";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            FormStyleHelper.ApplyFormStyle(this);

            // Створюємо заголовок
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = FormStyleHelper.PrimaryColor,
                Padding = new Padding(20, 15, 20, 15)
            };

            lblTitle = new Label
            {
                Text = isEditMode ? "✏️ Редагування культури" : "➕ Додавання культури",
                Font = FormStyleHelper.HeaderFont,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 18)
            };

            headerPanel.Controls.Add(lblTitle);
            this.Controls.Add(headerPanel);

            // Створюємо основну панель контенту
            contentPanel = FormStyleHelper.CreateCardPanel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Padding = new Padding(40);

            int yPos = 80;
            int labelWidth = 150;
            int controlWidth = 280;
            int spacing = 50;

            // Назва культури
            var lblName = FormStyleHelper.CreateLabel("Назва культури:");
            lblName.Location = new Point(30, yPos);
            txtName = FormStyleHelper.CreateStyledTextBox();
            txtName.Location = new Point(labelWidth + 50, yPos - 3);
            txtName.Size = new Size(controlWidth, 25);
            txtName.Font = FormStyleHelper.RegularFont;

            yPos += spacing;

            // Сезонність
            var lblSeasonality = FormStyleHelper.CreateLabel("Сезонність:");
            lblSeasonality.Location = new Point(30, yPos);
            cmbSeasonality = FormStyleHelper.CreateStyledComboBox();
            cmbSeasonality.Location = new Point(labelWidth + 50, yPos - 3);
            cmbSeasonality.Size = new Size(controlWidth, 25);
            cmbSeasonality.Items.AddRange(new object[] { "Весна", "Літо", "Осінь", "Зима", "Цілий рік" });

            yPos += spacing;

            // Середня врожайність
            var lblAverageYield = FormStyleHelper.CreateLabel("Середня врожайність\n(кг/га):");
            lblAverageYield.Location = new Point(30, yPos);
            numAverageYield = new NumericUpDown 
            { 
                Location = new Point(labelWidth + 70, yPos - 3),
                Size = new Size(controlWidth - 20, 25), 
                Minimum = 0, 
                Maximum = 100000, 
                DecimalPlaces = 0,
                Font = FormStyleHelper.RegularFont,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            yPos += 80;

            // Кнопки
            btnSave = FormStyleHelper.CreateStyledButton("💾 Зберегти", FormStyleHelper.SuccessColor, new Size(140, 45));
            btnSave.Location = new Point(120, yPos);
            btnSave.Click += BtnSave_Click;

            btnCancel = FormStyleHelper.CreateStyledButton("❌ Скасувати", FormStyleHelper.DangerColor, new Size(140, 45));
            btnCancel.Location = new Point(280, yPos);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Додаємо всі елементи до панелі
            contentPanel.Controls.AddRange(new Control[] {
                lblName, txtName,
                lblSeasonality, cmbSeasonality,
                lblAverageYield, numAverageYield,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadCulture()
        {
            txtName.Text = Culture.Name;
            cmbSeasonality.SelectedItem = Culture.Seasonality;
            numAverageYield.Value = Culture.AverageYield;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву культури!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbSeasonality.SelectedItem == null)
            {
                MessageBox.Show("Оберіть сезонність!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numAverageYield.Value < 0)
            {
                MessageBox.Show("Середня врожайність не може бути від'ємною!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Culture.Name = txtName.Text.Trim();
            Culture.Seasonality = cmbSeasonality.SelectedItem.ToString();
            Culture.AverageYield = (int)numAverageYield.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
} 