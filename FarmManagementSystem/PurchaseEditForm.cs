using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services;

namespace FarmManagementSystem
{
    public class PurchaseEditForm : Form
    {
        public Purchase Purchase { get; private set; }
        private ComboBox cmbMaterial;
        private ComboBox cmbSupplier;
        private DateTimePicker dtpDate;
        private NumericUpDown numQuantity;
        private NumericUpDown numUnitPrice;
        private CheckBox chkIsContract;
        private DateTimePicker dtpContractDate;
        private DateTimePicker dtpDeliveryDate;
        private ComboBox cmbStatus;
        private TextBox txtNotes;
        private Button btnSave;
        private Button btnCancel;
        private Label lblMaterial;
        private Label lblSupplier;
        private Label lblDate;
        private Label lblQuantity;
        private Label lblUnitPrice;
        private Label lblContractDate;
        private Label lblDeliveryDate;
        private Label lblStatus;
        private Label lblNotes;
        private List<MaterialType> materials;
        private List<Supplier> suppliers;
        private DatabaseService dbService;
        private string dbPath = "farm.db";
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public PurchaseEditForm(Purchase purchase = null)
        {
            isEditMode = purchase != null;
            Purchase = purchase != null ? new Purchase
            {
                Id = purchase.Id,
                MaterialId = purchase.MaterialId,
                SupplierId = purchase.SupplierId,
                Date = purchase.Date,
                Quantity = purchase.Quantity,
                UnitPrice = purchase.UnitPrice,
                ContractDate = purchase.ContractDate,
                DeliveryDate = purchase.DeliveryDate,
                Status = purchase.Status,
                Notes = purchase.Notes,
                Material = purchase.Material,
                Supplier = purchase.Supplier
            } : new Purchase { Date = DateTime.Today, Status = "Active" };
            
            dbService = new DatabaseService(dbPath);
            materials = dbService.GetAllMaterialTypes();
            suppliers = dbService.GetAllSuppliers();
            
            if (materials == null || materials.Count == 0)
            {
                MessageBox.Show("Увага: Не знайдено жодного типу матеріалів у базі даних!\nСпочатку додайте типи матеріалів.", 
                    "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                materials = new List<MaterialType>
                {
                    new MaterialType { Id = 0, Name = "Немає матеріалів", Type = "Тест", Unit = "шт" }
                };
            }
            
            InitializeComponent();
            
            if (isEditMode) 
            {
                LoadPurchase();
            }
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування закупки/контракту" : "Додавання закупки/контракту";
            this.Size = new Size(500, 700);
            FormStyleHelper.ApplyFormStyle(this);

            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = FormStyleHelper.PrimaryColor,
                Padding = new Padding(20, 15, 20, 15)
            };

            lblTitle = new Label
            {
                Text = isEditMode ? "✏️ Редагування закупки/контракту" : "➕ Додавання закупки/контракту",
                Font = FormStyleHelper.HeaderFont,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 18)
            };

            headerPanel.Controls.Add(lblTitle);
            this.Controls.Add(headerPanel);

            contentPanel = FormStyleHelper.CreateCardPanel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Padding = new Padding(30);

            int yPos = 80;
            int labelWidth = 120;
            int controlWidth = 280;
            int spacing = 45;

            lblMaterial = FormStyleHelper.CreateLabel("Матеріал:");
            lblMaterial.Location = new Point(20, yPos);
            cmbMaterial = FormStyleHelper.CreateStyledComboBox();
            cmbMaterial.Location = new Point(labelWidth + 40, yPos - 3);
            cmbMaterial.Size = new Size(controlWidth, 25);
            
            cmbMaterial.DataSource = materials;
            cmbMaterial.DisplayMember = "Name";
            cmbMaterial.ValueMember = "Id";
            
            if (cmbMaterial.Items.Count == 0 && materials != null && materials.Count > 0)
            {
                cmbMaterial.DataSource = null;
                cmbMaterial.Items.Clear();
                foreach (var material in materials)
                {
                    cmbMaterial.Items.Add(material);
                }
            }
            
            yPos += spacing;

            lblSupplier = FormStyleHelper.CreateLabel("Постачальник:");
            lblSupplier.Location = new Point(20, yPos);
            cmbSupplier = FormStyleHelper.CreateStyledComboBox();
            cmbSupplier.Location = new Point(labelWidth + 40, yPos - 3);
            cmbSupplier.Size = new Size(controlWidth, 25);
            cmbSupplier.DataSource = suppliers;
            cmbSupplier.DisplayMember = "Name";
            cmbSupplier.ValueMember = "Id";

            yPos += spacing;

            lblDate = FormStyleHelper.CreateLabel("Дата:");
            lblDate.Location = new Point(20, yPos);
            dtpDate = new DateTimePicker 
            { 
                Location = new Point(labelWidth + 40, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Format = DateTimePickerFormat.Short,
                Font = FormStyleHelper.RegularFont
            };

            yPos += spacing;

            lblQuantity = FormStyleHelper.CreateLabel("Кількість:");
            lblQuantity.Location = new Point(20, yPos);
            numQuantity = new NumericUpDown 
            { 
                Location = new Point(labelWidth + 40, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Minimum = 1, 
                Maximum = 1000000, 
                DecimalPlaces = 0,
                Font = FormStyleHelper.RegularFont
            };

            yPos += spacing;

            lblUnitPrice = FormStyleHelper.CreateLabel("Ціна за од. (грн):");
            lblUnitPrice.Location = new Point(20, yPos);
            numUnitPrice = new NumericUpDown 
            { 
                Location = new Point(labelWidth + 40, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Minimum = 1, 
                Maximum = 1000000, 
                DecimalPlaces = 0,
                Font = FormStyleHelper.RegularFont
            };

            yPos += spacing;

            chkIsContract = new CheckBox 
            { 
                Text = "Це контракт", 
                Location = new Point(20, yPos), 
                AutoSize = true,
                Font = FormStyleHelper.RegularFont,
                ForeColor = FormStyleHelper.TextColor
            };
            chkIsContract.CheckedChanged += ChkIsContract_CheckedChanged;

            yPos += spacing;

            lblContractDate = FormStyleHelper.CreateLabel("Дата контракту:");
            lblContractDate.Location = new Point(20, yPos);
            dtpContractDate = new DateTimePicker 
            { 
                Location = new Point(labelWidth + 40, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Format = DateTimePickerFormat.Short, 
                Enabled = false,
                Font = FormStyleHelper.RegularFont
            };

            yPos += spacing;

            lblDeliveryDate = FormStyleHelper.CreateLabel("Дата постачання:");
            lblDeliveryDate.Location = new Point(20, yPos);
            dtpDeliveryDate = new DateTimePicker 
            { 
                Location = new Point(labelWidth + 40, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Format = DateTimePickerFormat.Short, 
                Enabled = false,
                Font = FormStyleHelper.RegularFont
            };

            yPos += spacing;

            lblStatus = FormStyleHelper.CreateLabel("Статус:");
            lblStatus.Location = new Point(20, yPos);
            cmbStatus = FormStyleHelper.CreateStyledComboBox();
            cmbStatus.Location = new Point(labelWidth + 40, yPos - 3);
            cmbStatus.Size = new Size(controlWidth, 25);
            cmbStatus.Items.AddRange(new string[] { "Active", "Completed", "Cancelled" });
            cmbStatus.SelectedItem = "Active";

            yPos += spacing;

            lblNotes = FormStyleHelper.CreateLabel("Примітки:");
            lblNotes.Location = new Point(20, yPos);
            txtNotes = FormStyleHelper.CreateStyledTextBox();
            txtNotes.Location = new Point(labelWidth + 40, yPos - 3);
            txtNotes.Size = new Size(controlWidth, 60);
            txtNotes.Multiline = true;
            txtNotes.ScrollBars = ScrollBars.Vertical;

            yPos += 80;

            btnSave = FormStyleHelper.CreateStyledButton("💾 Зберегти", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnSave.Location = new Point(120, yPos);
            btnSave.Click += BtnSave_Click;

            btnCancel = FormStyleHelper.CreateStyledButton("❌ Скасувати", FormStyleHelper.DangerColor, new Size(120, 40));
            btnCancel.Location = new Point(260, yPos);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            contentPanel.Controls.AddRange(new Control[] {
                lblMaterial, cmbMaterial,
                lblSupplier, cmbSupplier,
                lblDate, dtpDate,
                lblQuantity, numQuantity,
                lblUnitPrice, numUnitPrice,
                chkIsContract,
                lblContractDate, dtpContractDate,
                lblDeliveryDate, dtpDeliveryDate,
                lblStatus, cmbStatus,
                lblNotes, txtNotes,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void ChkIsContract_CheckedChanged(object sender, EventArgs e)
        {
            bool isContract = chkIsContract.Checked;
            dtpContractDate.Enabled = isContract;
            dtpDeliveryDate.Enabled = isContract;
            
            if (isContract && !isEditMode)
            {
                dtpContractDate.Value = DateTime.Today;
                dtpDeliveryDate.Value = DateTime.Today.AddDays(30);
            }
        }

        private void LoadPurchase()
        {
            dtpDate.Value = Purchase.Date;
            numQuantity.Value = Purchase.Quantity;
            numUnitPrice.Value = Purchase.UnitPrice;
            
            for (int i = 0; i < cmbMaterial.Items.Count; i++)
            {
                MaterialType material = (MaterialType)cmbMaterial.Items[i];
                if (material.Id == Purchase.MaterialId)
                {
                    cmbMaterial.SelectedIndex = i;
                    break;
                }
            }
            
            for (int i = 0; i < cmbSupplier.Items.Count; i++)
            {
                Supplier supplier = (Supplier)cmbSupplier.Items[i];
                if (supplier.Id == Purchase.SupplierId)
                {
                    cmbSupplier.SelectedIndex = i;
                    break;
                }
            }
            
            chkIsContract.Checked = Purchase.IsContract();
            if (Purchase.ContractDate.HasValue)
            {
                dtpContractDate.Value = Purchase.ContractDate.Value;
            }
            if (Purchase.DeliveryDate.HasValue)
            {
                dtpDeliveryDate.Value = Purchase.DeliveryDate.Value;
            }
            cmbStatus.SelectedItem = Purchase.Status ?? "Active";
            txtNotes.Text = Purchase.Notes ?? "";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (cmbMaterial.SelectedItem == null)
            {
                MessageBox.Show("Оберіть матеріал!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbSupplier.SelectedItem == null)
            {
                MessageBox.Show("Оберіть постачальника!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("Введіть кількість!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numUnitPrice.Value <= 0)
            {
                MessageBox.Show("Введіть ціну за одиницю!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var selectedMaterial = (MaterialType)cmbMaterial.SelectedItem;
            var selectedSupplier = (Supplier)cmbSupplier.SelectedItem;
            
            Purchase.MaterialId = selectedMaterial.Id;
            Purchase.SupplierId = selectedSupplier.Id;
            Purchase.Date = dtpDate.Value.Date;
            Purchase.Quantity = (int)numQuantity.Value;
            Purchase.UnitPrice = (int)numUnitPrice.Value;
            Purchase.ContractDate = chkIsContract.Checked ? (DateTime?)dtpContractDate.Value.Date : null;
            Purchase.DeliveryDate = chkIsContract.Checked ? (DateTime?)dtpDeliveryDate.Value.Date : null;
            Purchase.Status = cmbStatus.SelectedItem.ToString();
            Purchase.Notes = txtNotes.Text;
            
            this.DialogResult = DialogResult.OK;
        }
    }
} 