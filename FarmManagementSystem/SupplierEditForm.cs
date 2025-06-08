using System;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;

namespace FarmManagementSystem
{
    public class SupplierEditForm : Form
    {
        public Supplier Supplier { get; private set; }
        private TextBox txtName;
        private TextBox txtContactPerson;
        private TextBox txtPhone;
        private ComboBox cmbProductType;
        private Button btnSave;
        private Button btnCancel;
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public SupplierEditForm(Supplier supplier = null)
        {
            isEditMode = supplier != null;
            Supplier = supplier != null ? new Supplier
            {
                Id = supplier.Id,
                Name = supplier.Name,
                ContactPerson = supplier.ContactPerson,
                Phone = supplier.Phone,
                ProductType = supplier.ProductType
            } : new Supplier();
            InitializeComponent();
            if (isEditMode) LoadSupplier();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування постачальника" : "Додавання постачальника";
            this.Size = new Size(550, 500);
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
                Text = isEditMode ? "🏪 Редагування постачальника" : "➕ Додавання постачальника",
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

            // Назва компанії
            var lblName = FormStyleHelper.CreateLabel("Назва компанії:");
            lblName.Location = new Point(30, yPos);
            txtName = FormStyleHelper.CreateStyledTextBox();
            txtName.Location = new Point(labelWidth + 50, yPos - 3);
            txtName.Size = new Size(controlWidth, 25);
            txtName.Font = FormStyleHelper.RegularFont;

            yPos += spacing;

            // Контактна особа
            var lblContactPerson = FormStyleHelper.CreateLabel("Контактна особа:");
            lblContactPerson.Location = new Point(30, yPos);
            txtContactPerson = FormStyleHelper.CreateStyledTextBox();
            txtContactPerson.Location = new Point(labelWidth + 50, yPos - 3);
            txtContactPerson.Size = new Size(controlWidth, 25);
            txtContactPerson.Font = FormStyleHelper.RegularFont;

            yPos += spacing;

            // Телефон
            var lblPhone = FormStyleHelper.CreateLabel("Телефон:");
            lblPhone.Location = new Point(30, yPos);
            txtPhone = FormStyleHelper.CreateStyledTextBox();
            txtPhone.Location = new Point(labelWidth + 50, yPos - 3);
            txtPhone.Size = new Size(controlWidth, 25);
            txtPhone.Font = FormStyleHelper.RegularFont;

            yPos += spacing;

            // Тип продукції
            var lblProductType = FormStyleHelper.CreateLabel("Тип продукції:");
            lblProductType.Location = new Point(30, yPos);
            cmbProductType = FormStyleHelper.CreateStyledComboBox();
            cmbProductType.Location = new Point(labelWidth + 50, yPos - 3);
            cmbProductType.Size = new Size(controlWidth, 25);
            cmbProductType.Items.AddRange(new object[] {
                "Насіння",
                "Добрива",
                "Засоби захисту рослин",
                "Паливо",
                "Техніка",
                "Обладнання",
                "Інше"
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
                lblContactPerson, txtContactPerson,
                lblPhone, txtPhone,
                lblProductType, cmbProductType,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadSupplier()
        {
            txtName.Text = Supplier.Name;
            txtContactPerson.Text = Supplier.ContactPerson;
            txtPhone.Text = Supplier.Phone;
            cmbProductType.SelectedItem = Supplier.ProductType;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву компанії!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("Введіть контактну особу!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Введіть телефон!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbProductType.SelectedItem == null)
            {
                MessageBox.Show("Оберіть тип продукції!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            Supplier.Name = txtName.Text.Trim();
            Supplier.ContactPerson = txtContactPerson.Text.Trim();
            Supplier.Phone = txtPhone.Text.Trim();
            Supplier.ProductType = cmbProductType.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
} 