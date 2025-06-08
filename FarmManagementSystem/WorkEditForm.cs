using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services;

namespace FarmManagementSystem
{
    public class WorkEditForm : Form
    {
        public Work Work { get; private set; }
        private ComboBox cmbWorkType;
        private ComboBox cmbField;
        private ComboBox cmbTechnique;
        private ComboBox cmbEmployee;
        private DateTimePicker dtpDate;
        private NumericUpDown numDuration;
        private Button btnSave;
        private Button btnCancel;
        private List<WorkType> workTypes;
        private List<Field> fields;
        private List<Technique> techniques;
        private List<Employee> employees;
        private DatabaseService dbService;
        private string dbPath = "farm.db";
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public WorkEditForm(Work work = null)
        {
            isEditMode = work != null;
            Work = work != null ? new Work
            {
                Id = work.Id,
                Duration = work.Duration,
                Date = work.Date,
                WorkTypeId = work.WorkTypeId,
                FieldId = work.FieldId,
                TechniqueId = work.TechniqueId,
                EmployeeId = work.EmployeeId
            } : new Work { Date = DateTime.Today };
            dbService = new DatabaseService(dbPath);
            workTypes = dbService.GetAllWorkTypes();
            fields = dbService.GetAllFields();
            techniques = dbService.GetAllTechniques();
            employees = dbService.GetAllEmployees();
            InitializeComponent();
            if (isEditMode) LoadWork();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування роботи" : "Додавання роботи";
            this.Size = new Size(500, 550);
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
                Text = isEditMode ? "✏️ Редагування роботи" : "➕ Додавання роботи",
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

            // Тип роботи
            var lblWorkType = FormStyleHelper.CreateLabel("Тип роботи:");
            lblWorkType.Location = new Point(30, yPos);
            cmbWorkType = FormStyleHelper.CreateStyledComboBox();
            cmbWorkType.Location = new Point(labelWidth + 50, yPos - 3);
            cmbWorkType.Size = new Size(controlWidth, 25);
            cmbWorkType.DataSource = workTypes;
            cmbWorkType.DisplayMember = "Name";
            cmbWorkType.ValueMember = "Id";

            yPos += spacing;

            // Поле
            var lblField = FormStyleHelper.CreateLabel("Поле:");
            lblField.Location = new Point(30, yPos);
            cmbField = FormStyleHelper.CreateStyledComboBox();
            cmbField.Location = new Point(labelWidth + 50, yPos - 3);
            cmbField.Size = new Size(controlWidth, 25);
            cmbField.DataSource = fields;
            cmbField.DisplayMember = "Name";
            cmbField.ValueMember = "Id";

            yPos += spacing;

            // Техніка
            var lblTechnique = FormStyleHelper.CreateLabel("Техніка:");
            lblTechnique.Location = new Point(30, yPos);
            cmbTechnique = FormStyleHelper.CreateStyledComboBox();
            cmbTechnique.Location = new Point(labelWidth + 50, yPos - 3);
            cmbTechnique.Size = new Size(controlWidth, 25);
            cmbTechnique.DataSource = techniques;
            cmbTechnique.DisplayMember = "Name";
            cmbTechnique.ValueMember = "Id";

            yPos += spacing;

            // Працівник
            var lblEmployee = FormStyleHelper.CreateLabel("Працівник:");
            lblEmployee.Location = new Point(30, yPos);
            cmbEmployee = FormStyleHelper.CreateStyledComboBox();
            cmbEmployee.Location = new Point(labelWidth + 50, yPos - 3);
            cmbEmployee.Size = new Size(controlWidth, 25);
            cmbEmployee.DataSource = employees;
            cmbEmployee.DisplayMember = "FullName";
            cmbEmployee.ValueMember = "Id";

            yPos += spacing;

            // Дата
            var lblDate = FormStyleHelper.CreateLabel("Дата:");
            lblDate.Location = new Point(30, yPos);
            dtpDate = new DateTimePicker
            {
                Location = new Point(labelWidth + 50, yPos - 3),
                Size = new Size(controlWidth, 25),
                Format = DateTimePickerFormat.Short,
                Font = FormStyleHelper.RegularFont
            };

            yPos += spacing;

            // Тривалість
            var lblDuration = FormStyleHelper.CreateLabel("Тривалість (год):");
            lblDuration.Location = new Point(30, yPos);
            numDuration = new NumericUpDown
            {
                Location = new Point(labelWidth + 50, yPos - 3),
                Size = new Size(controlWidth, 25),
                Minimum = 1,
                Maximum = 1000,
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
                lblWorkType, cmbWorkType,
                lblField, cmbField,
                lblTechnique, cmbTechnique,
                lblEmployee, cmbEmployee,
                lblDate, dtpDate,
                lblDuration, numDuration,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadWork()
        {
            cmbWorkType.SelectedValue = Work.WorkTypeId;
            cmbField.SelectedValue = Work.FieldId;
            cmbTechnique.SelectedValue = Work.TechniqueId;
            cmbEmployee.SelectedValue = Work.EmployeeId;
            dtpDate.Value = Work.Date;
            numDuration.Value = Work.Duration;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbWorkType.SelectedItem == null)
            {
                MessageBox.Show("Оберіть тип роботи!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbField.SelectedItem == null)
            {
                MessageBox.Show("Оберіть поле!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbTechnique.SelectedItem == null)
            {
                MessageBox.Show("Оберіть техніку!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbEmployee.SelectedItem == null)
            {
                MessageBox.Show("Оберіть працівника!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numDuration.Value <= 0)
            {
                MessageBox.Show("Введіть тривалість роботи!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Work.WorkTypeId = (int)cmbWorkType.SelectedValue;
            Work.FieldId = (int)cmbField.SelectedValue;
            Work.TechniqueId = (int)cmbTechnique.SelectedValue;
            Work.EmployeeId = (int)cmbEmployee.SelectedValue;
            Work.Date = dtpDate.Value.Date;
            Work.Duration = (int)numDuration.Value;
            
            this.DialogResult = DialogResult.OK;
        }
    }
} 