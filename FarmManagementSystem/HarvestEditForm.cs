using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services;

namespace FarmManagementSystem
{
    public class HarvestEditForm : Form
    {
        public Harvest Harvest { get; private set; }
        private ComboBox cmbField;
        private ComboBox cmbCulture;
        private DateTimePicker dtpHarvestDate;
        private NumericUpDown numVolume;
        private NumericUpDown numPricePerKg;
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

        public HarvestEditForm(Harvest harvest = null)
        {
            isEditMode = harvest != null;
            Harvest = harvest != null ? new Harvest
            {
                Id = harvest.Id,
                FieldId = harvest.FieldId,
                CultureId = harvest.CultureId,
                HarvestDate = harvest.HarvestDate,
                Volume = harvest.Volume,
                PricePerKg = harvest.PricePerKg
            } : new Harvest { HarvestDate = DateTime.Today };
            dbService = new DatabaseService(dbPath);
            fields = dbService.GetAllFields();
            cultures = dbService.GetAllCultures();
            InitializeComponent();
            if (isEditMode) LoadHarvest();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування урожаю" : "Додавання урожаю";
            this.Size = new Size(550, 550);
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
                Text = isEditMode ? "🌾 Редагування урожаю" : "➕ Додавання урожаю",
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

            // Дата збору
            var lblDate = FormStyleHelper.CreateLabel("Дата збору:");
            lblDate.Location = new Point(30, yPos);
            dtpHarvestDate = new DateTimePicker 
            { 
                Location = new Point(labelWidth + 50, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Format = DateTimePickerFormat.Short,
                Font = FormStyleHelper.RegularFont
            };

            yPos += spacing;

            // Обсяг
            var lblVolume = FormStyleHelper.CreateLabel("Обсяг (кг):");
            lblVolume.Location = new Point(30, yPos);
            numVolume = new NumericUpDown 
            { 
                Location = new Point(labelWidth + 50, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Minimum = 0, 
                Maximum = 1000000, 
                DecimalPlaces = 0,
                Font = FormStyleHelper.RegularFont,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            yPos += spacing;

            // Ціна за кг
            var lblPrice = FormStyleHelper.CreateLabel("Ціна за кг (грн):");
            lblPrice.Location = new Point(30, yPos);
            numPricePerKg = new NumericUpDown 
            { 
                Location = new Point(labelWidth + 50, yPos - 3), 
                Size = new Size(controlWidth, 25), 
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
            btnSave.Location = new Point(140, yPos);
            btnSave.Click += BtnSave_Click;

            btnCancel = FormStyleHelper.CreateStyledButton("❌ Скасувати", FormStyleHelper.DangerColor, new Size(140, 45));
            btnCancel.Location = new Point(300, yPos);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Додаємо всі елементи до панелі
            contentPanel.Controls.AddRange(new Control[] {
                lblField, cmbField,
                lblCulture, cmbCulture,
                lblDate, dtpHarvestDate,
                lblVolume, numVolume,
                lblPrice, numPricePerKg,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadHarvest()
        {
            cmbField.SelectedValue = Harvest.FieldId;
            cmbCulture.SelectedValue = Harvest.CultureId;
            dtpHarvestDate.Value = Harvest.HarvestDate;
            numVolume.Value = Harvest.Volume;
            numPricePerKg.Value = Harvest.PricePerKg;
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
            if (numVolume.Value <= 0)
            {
                MessageBox.Show("Введіть обсяг урожаю!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numPricePerKg.Value <= 0)
            {
                MessageBox.Show("Введіть ціну за кг!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Harvest.FieldId = (int)cmbField.SelectedValue;
            Harvest.CultureId = (int)cmbCulture.SelectedValue;
            Harvest.HarvestDate = dtpHarvestDate.Value.Date;
            Harvest.Volume = (int)numVolume.Value;
            Harvest.PricePerKg = (int)numPricePerKg.Value;
            this.DialogResult = DialogResult.OK;
        }
    }
} 