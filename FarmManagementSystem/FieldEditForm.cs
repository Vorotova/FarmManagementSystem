using System;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;

namespace FarmManagementSystem
{
    public class FieldEditForm : Form
    {
        public Field Field { get; private set; }
        private TextBox txtName;
        private NumericUpDown numArea;
        private ComboBox cmbSoilType;
        private Button btnSave;
        private Button btnCancel;
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public FieldEditForm(Field field = null)
        {
            isEditMode = field != null;
            Field = field != null ? new Field
            {
                Id = field.Id,
                Name = field.Name,
                Area = field.Area,
                SoilType = field.SoilType
            } : new Field();
            InitializeComponent();
            if (isEditMode) LoadField();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування поля" : "Додавання поля";
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
                Text = isEditMode ? "🌾 Редагування поля" : "➕ Додавання поля",
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
            int labelWidth = 120;
            int controlWidth = 280;
            int spacing = 50;

            // Назва поля
            var lblName = FormStyleHelper.CreateLabel("Назва поля:");
            lblName.Location = new Point(30, yPos);
            txtName = FormStyleHelper.CreateStyledTextBox();
            txtName.Location = new Point(labelWidth + 50, yPos - 3);
            txtName.Size = new Size(controlWidth, 25);
            txtName.Font = FormStyleHelper.RegularFont;

            yPos += spacing;

            // Тип ґрунту
            var lblSoilType = FormStyleHelper.CreateLabel("Тип ґрунту:");
            lblSoilType.Location = new Point(30, yPos);
            cmbSoilType = FormStyleHelper.CreateStyledComboBox();
            cmbSoilType.Location = new Point(labelWidth + 50, yPos - 3);
            cmbSoilType.Size = new Size(controlWidth, 25);
            cmbSoilType.Items.AddRange(new object[] { "Чорнозем", "Пісок", "Глина", "Торф", "Суглинок", "Супісок" });

            yPos += spacing;

            // Площа
            var lblArea = FormStyleHelper.CreateLabel("Площа (га):");
            lblArea.Location = new Point(30, yPos);
            numArea = new NumericUpDown 
            { 
                Location = new Point(labelWidth + 50, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Minimum = 0, 
                Maximum = 100000, 
                DecimalPlaces = 1,
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
                lblSoilType, cmbSoilType,
                lblArea, numArea,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadField()
        {
            txtName.Text = Field.Name;
            cmbSoilType.SelectedItem = Field.SoilType;
            numArea.Value = Field.Area;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву поля!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbSoilType.SelectedItem == null)
            {
                MessageBox.Show("Оберіть тип ґрунту!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numArea.Value < 0)
            {
                MessageBox.Show("Площа не може бути від'ємною!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Field.Name = txtName.Text.Trim();
            Field.SoilType = cmbSoilType.SelectedItem.ToString();
            Field.Area = (int)numArea.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
} 