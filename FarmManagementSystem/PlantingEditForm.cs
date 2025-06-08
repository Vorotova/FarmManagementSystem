using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services;

namespace FarmManagementSystem
{
    public class PlantingEditForm : Form
    {
        public Planting Planting { get; private set; }
        private ComboBox cmbField;
        private ComboBox cmbCulture;
        private DateTimePicker dtpSowingDate;
        private Button btnSave;
        private Button btnCancel;
        private List<Field> fields;
        private List<Culture> cultures;
        private DatabaseService dbService;
        private string dbPath = "farm.db";
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public PlantingEditForm(Planting planting = null)
        {
            isEditMode = planting != null;
            Planting = planting != null ? new Planting
            {
                Id = planting.Id,
                FieldId = planting.FieldId,
                CultureId = planting.CultureId,
                SowingDate = planting.SowingDate
            } : new Planting { SowingDate = DateTime.Today };
            dbService = new DatabaseService(dbPath);
            fields = dbService.GetAllFields();
            cultures = dbService.GetAllCultures();
            InitializeComponent();
            if (isEditMode) LoadPlanting();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування посадки" : "Додавання посадки";
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
                Text = isEditMode ? "🌿 Редагування посадки" : "➕ Додавання посадки",
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

            // Культура
            var lblCulture = FormStyleHelper.CreateLabel("Культура:");
            lblCulture.Location = new Point(30, yPos);
            cmbCulture = FormStyleHelper.CreateStyledComboBox();
            cmbCulture.Location = new Point(labelWidth + 50, yPos - 3);
            cmbCulture.Size = new Size(controlWidth, 25);
            cmbCulture.DataSource = cultures;
            cmbCulture.DisplayMember = "Name";
            cmbCulture.ValueMember = "Id";

            yPos += spacing;

            // Дата посіву
            var lblDate = FormStyleHelper.CreateLabel("Дата посіву:");
            lblDate.Location = new Point(30, yPos);
            dtpSowingDate = new DateTimePicker
            {
                Location = new Point(labelWidth + 50, yPos - 3),
                Size = new Size(controlWidth, 25),
                Format = DateTimePickerFormat.Short,
                Font = FormStyleHelper.RegularFont,
                Value = DateTime.Today
            };

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
                lblField, cmbField,
                lblCulture, cmbCulture,
                lblDate, dtpSowingDate,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadPlanting()
        {
            cmbField.SelectedValue = Planting.FieldId;
            cmbCulture.SelectedValue = Planting.CultureId;
            dtpSowingDate.Value = Planting.SowingDate;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbField.SelectedItem == null)
            {
                MessageBox.Show("Оберіть поле!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbCulture.SelectedItem == null)
            {
                MessageBox.Show("Оберіть культуру!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            Planting.FieldId = (int)cmbField.SelectedValue;
            Planting.CultureId = (int)cmbCulture.SelectedValue;
            Planting.SowingDate = dtpSowingDate.Value.Date;
            this.DialogResult = DialogResult.OK;
        }
    }
} 