using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services;

namespace FarmManagementSystem
{
    public class MaterialUsageEditForm : Form
    {
        public MaterialUsage MaterialUsage { get; private set; }
        private ComboBox cmbMaterialType;
        private ComboBox cmbWork;
        private NumericUpDown numQuantity;
        private Button btnSave;
        private Button btnCancel;
        private List<MaterialType> materialTypes;
        private List<Work> works;
        private DatabaseService dbService;
        private string dbPath = "farm.db";
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public MaterialUsageEditForm(MaterialUsage usage = null)
        {
            isEditMode = usage != null;
            MaterialUsage = usage != null ? new MaterialUsage
            {
                Id = usage.Id,
                MaterialTypeId = usage.MaterialTypeId,
                WorkId = usage.WorkId,
                Quantity = usage.Quantity
            } : new MaterialUsage();
            dbService = new DatabaseService(dbPath);
            materialTypes = dbService.GetAllMaterialTypes();
            works = dbService.GetAllWorks();
            InitializeComponent();
            if (isEditMode) LoadUsage();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування використання матеріалу" : "Додавання використання матеріалу";
            this.Size = new Size(500, 400);
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
                Text = isEditMode ? "✏️ Редагування використання матеріалу" : "➕ Додавання використання матеріалу",
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

            // Матеріал
            var lblMaterialType = FormStyleHelper.CreateLabel("Матеріал:");
            lblMaterialType.Location = new Point(30, yPos);
            cmbMaterialType = FormStyleHelper.CreateStyledComboBox();
            cmbMaterialType.Location = new Point(labelWidth + 50, yPos - 3);
            cmbMaterialType.Size = new Size(controlWidth, 25);
            cmbMaterialType.DataSource = materialTypes;
            cmbMaterialType.DisplayMember = "Name";
            cmbMaterialType.ValueMember = "Id";

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

            yPos += spacing;

            // Кількість
            var lblQuantity = FormStyleHelper.CreateLabel("Кількість:");
            lblQuantity.Location = new Point(30, yPos);
            numQuantity = new NumericUpDown
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
                lblMaterialType, cmbMaterialType,
                lblWork, cmbWork,
                lblQuantity, numQuantity,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadUsage()
        {
            cmbMaterialType.SelectedValue = MaterialUsage.MaterialTypeId;
            cmbWork.SelectedValue = MaterialUsage.WorkId;
            numQuantity.Value = MaterialUsage.Quantity;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbMaterialType.SelectedItem == null)
            {
                MessageBox.Show("Оберіть матеріал!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbWork.SelectedItem == null)
            {
                MessageBox.Show("Оберіть роботу!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("Введіть кількість!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MaterialUsage.MaterialTypeId = (int)cmbMaterialType.SelectedValue;
            MaterialUsage.WorkId = (int)cmbWork.SelectedValue;
            MaterialUsage.Quantity = (int)numQuantity.Value;
            
            this.DialogResult = DialogResult.OK;
        }
    }
} 