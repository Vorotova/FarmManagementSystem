using System;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;

namespace FarmManagementSystem
{
    public class EmployeeEditForm : Form
    {
        public Employee Employee { get; private set; }
        private TextBox txtFullName;
        private TextBox txtPhone;
        private TextBox txtPosition;
        private Button btnSave;
        private Button btnCancel;
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public EmployeeEditForm(Employee employee = null)
        {
            isEditMode = employee != null;
            Employee = employee != null ? new Employee
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Phone = employee.Phone,
                Position = employee.Position
            } : new Employee();
            InitializeComponent();
            if (isEditMode) LoadEmployee();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування працівника" : "Додавання працівника";
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
                Text = isEditMode ? "👤 Редагування працівника" : "➕ Додавання працівника",
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

            // ПІБ
            var lblFullName = FormStyleHelper.CreateLabel("ПІБ:");
            lblFullName.Location = new Point(30, yPos);
            txtFullName = FormStyleHelper.CreateStyledTextBox();
            txtFullName.Location = new Point(labelWidth + 50, yPos - 3);
            txtFullName.Size = new Size(controlWidth, 25);
            txtFullName.Font = FormStyleHelper.RegularFont;

            yPos += spacing;

            // Телефон
            var lblPhone = FormStyleHelper.CreateLabel("Телефон:");
            lblPhone.Location = new Point(30, yPos);
            txtPhone = FormStyleHelper.CreateStyledTextBox();
            txtPhone.Location = new Point(labelWidth + 50, yPos - 3);
            txtPhone.Size = new Size(controlWidth, 25);
            txtPhone.Font = FormStyleHelper.RegularFont;

            yPos += spacing;

            // Посада
            var lblPosition = FormStyleHelper.CreateLabel("Посада:");
            lblPosition.Location = new Point(30, yPos);
            txtPosition = FormStyleHelper.CreateStyledTextBox();
            txtPosition.Location = new Point(labelWidth + 50, yPos - 3);
            txtPosition.Size = new Size(controlWidth, 25);
            txtPosition.Font = FormStyleHelper.RegularFont;

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
                lblFullName, txtFullName,
                lblPhone, txtPhone,
                lblPosition, txtPosition,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadEmployee()
        {
            txtFullName.Text = Employee.FullName;
            txtPhone.Text = Employee.Phone;
            txtPosition.Text = Employee.Position;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Введіть ПІБ працівника!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Додаткова валідація телефону (простий приклад)
            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && txtPhone.Text.Length < 7)
            {
                MessageBox.Show("Введіть коректний номер телефону!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Employee.FullName = txtFullName.Text.Trim();
            Employee.Phone = txtPhone.Text.Trim();
            Employee.Position = txtPosition.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }
    }
} 