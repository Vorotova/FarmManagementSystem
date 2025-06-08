using System;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;

namespace FarmManagementSystem
{
    public class MaterialTypeEditForm : Form
    {
        public MaterialType MaterialType { get; private set; }
        private TextBox txtName;
        private ComboBox cmbType;
        private ComboBox cmbUnit;
        private Button btnSave;
        private Button btnCancel;
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public MaterialTypeEditForm(MaterialType materialType = null)
        {
            isEditMode = materialType != null;
            MaterialType = materialType != null ? new MaterialType
            {
                Id = materialType.Id,
                Name = materialType.Name,
                Type = materialType.Type,
                Unit = materialType.Unit
            } : new MaterialType();
            InitializeComponent();
            if (isEditMode) LoadMaterialType();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування типу матеріалу" : "Додавання типу матеріалу";
            this.Size = new Size(550, 450);
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
                Text = isEditMode ? "📦 Редагування типу матеріалу" : "➕ Додавання типу матеріалу",
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

            // Назва матеріалу
            var lblName = FormStyleHelper.CreateLabel("Назва матеріалу:");
            lblName.Location = new Point(30, yPos);
            txtName = FormStyleHelper.CreateStyledTextBox();
            txtName.Location = new Point(labelWidth + 50, yPos - 3);
            txtName.Size = new Size(controlWidth, 25);
            txtName.Font = FormStyleHelper.RegularFont;

            yPos += spacing;

            // Тип матеріалу
            var lblType = FormStyleHelper.CreateLabel("Тип матеріалу:");
            lblType.Location = new Point(30, yPos);
            cmbType = FormStyleHelper.CreateStyledComboBox();
            cmbType.Location = new Point(labelWidth + 50, yPos - 3);
            cmbType.Size = new Size(controlWidth, 25);
            cmbType.Items.AddRange(new object[] {
                "Насіння",
                "Добрива",
                "Засоби захисту рослин",
                "Паливо",
                "Інше"
            });

            yPos += spacing;

            // Одиниця виміру
            var lblUnit = FormStyleHelper.CreateLabel("Одиниця виміру:");
            lblUnit.Location = new Point(30, yPos);
            cmbUnit = FormStyleHelper.CreateStyledComboBox();
            cmbUnit.Location = new Point(labelWidth + 50, yPos - 3);
            cmbUnit.Size = new Size(controlWidth, 25);
            cmbUnit.Items.AddRange(new object[] {
                "кг",
                "л",
                "т",
                "шт",
                "упак",
                "м³",
                "м²",
                "г"
            });

            yPos += 80;

            // Кнопки
            btnSave = FormStyleHelper.CreateStyledButton("💾 Зберегти", FormStyleHelper.SuccessColor, new Size(140, 45));
            btnSave.Location = new Point(140, yPos);
            btnSave.Click += BtnSave_Click;

            btnCancel = FormStyleHelper.CreateStyledButton("❌ Скасувати", FormStyleHelper.DangerColor, new Size(140, 45));
            btnCancel.Location = new Point(300, yPos);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Додаємо всі елементи до панелі
            contentPanel.Controls.AddRange(new Control[] {
                lblName, txtName,
                lblType, cmbType,
                lblUnit, cmbUnit,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadMaterialType()
        {
            txtName.Text = MaterialType.Name;
            cmbType.SelectedItem = MaterialType.Type;
            cmbUnit.SelectedItem = MaterialType.Unit;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву матеріалу!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbType.SelectedItem == null)
            {
                MessageBox.Show("Оберіть тип матеріалу!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbUnit.SelectedItem == null)
            {
                MessageBox.Show("Оберіть одиницю виміру!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            MaterialType.Name = txtName.Text.Trim();
            MaterialType.Type = cmbType.SelectedItem.ToString();
            MaterialType.Unit = cmbUnit.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
} 