using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services;

namespace FarmManagementSystem
{
    public class ExpenseEditForm : Form
    {
        public Expense Expense { get; private set; }
        private TextBox txtExpenseType;
        private NumericUpDown numAmount;
        private DateTimePicker dtpDate;
        private ComboBox cmbWork;
        private Button btnSave;
        private Button btnCancel;
        private List<Work> works;
        private DatabaseService dbService;
        private string dbPath = "farm.db";
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public ExpenseEditForm(Expense expense = null)
        {
            isEditMode = expense != null;
            Expense = expense != null ? new Expense
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Date = expense.Date,
                ExpenseType = expense.ExpenseType,
                WorkId = expense.WorkId
            } : new Expense { Date = DateTime.Today };
            dbService = new DatabaseService(dbPath);
            works = dbService.GetAllWorks();
            InitializeComponent();
            if (isEditMode) LoadExpense();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування витрати" : "Додавання витрати";
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
                Text = isEditMode ? "✏️ Редагування витрати" : "➕ Додавання витрати",
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

            // Тип витрати
            var lblExpenseType = FormStyleHelper.CreateLabel("Тип витрати:");
            lblExpenseType.Location = new Point(30, yPos);
            txtExpenseType = FormStyleHelper.CreateStyledTextBox();
            txtExpenseType.Location = new Point(labelWidth + 50, yPos - 3);
            txtExpenseType.Size = new Size(controlWidth, 25);

            yPos += spacing;

            // Сума
            var lblAmount = FormStyleHelper.CreateLabel("Сума (грн):");
            lblAmount.Location = new Point(30, yPos);
            numAmount = new NumericUpDown
            {
                Location = new Point(labelWidth + 50, yPos - 3),
                Size = new Size(controlWidth, 25),
                Minimum = 1,
                Maximum = 1000000,
                DecimalPlaces = 0,
                Font = FormStyleHelper.RegularFont,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

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

            // Робота
            var lblWork = FormStyleHelper.CreateLabel("Робота:");
            lblWork.Location = new Point(30, yPos);
            cmbWork = FormStyleHelper.CreateStyledComboBox();
            cmbWork.Location = new Point(labelWidth + 50, yPos - 3);
            cmbWork.Size = new Size(controlWidth, 25);
            cmbWork.DataSource = works;
            cmbWork.DisplayMember = "ToString";
            cmbWork.ValueMember = "Id";

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
                lblExpenseType, txtExpenseType,
                lblAmount, numAmount,
                lblDate, dtpDate,
                lblWork, cmbWork,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadExpense()
        {
            txtExpenseType.Text = Expense.ExpenseType;
            numAmount.Value = Expense.Amount;
            dtpDate.Value = Expense.Date;
            cmbWork.SelectedValue = Expense.WorkId;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExpenseType.Text))
            {
                MessageBox.Show("Введіть тип витрати!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numAmount.Value <= 0)
            {
                MessageBox.Show("Введіть суму!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbWork.SelectedItem == null)
            {
                MessageBox.Show("Оберіть роботу!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Expense.ExpenseType = txtExpenseType.Text.Trim();
            Expense.Amount = (int)numAmount.Value;
            Expense.Date = dtpDate.Value.Date;
            Expense.WorkId = (int)cmbWork.SelectedValue;
            
            this.DialogResult = DialogResult.OK;
        }
    }
} 