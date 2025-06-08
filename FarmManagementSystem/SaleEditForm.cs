using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services;

namespace FarmManagementSystem
{
    public class SaleEditForm : Form
    {
        public Sale Sale { get; private set; }
        
        private TextBox txtClient;
        private ComboBox cmbHarvest;
        private NumericUpDown numQuantity;
        private TextBox txtUnitPrice;
        private Label lblTotalAmount;
        private Label lblAvailableQuantity;
        
        private CheckBox chkIsContract;
        private DateTimePicker dtpContractDate;
        private DateTimePicker dtpDeliveryDate;
        private ComboBox cmbStatus;
        private TextBox txtNotes;
        
        private Button btnSave;
        private Button btnCancel;
        
        private List<Client> allClients;
        private List<Harvest> availableHarvests;
        private DatabaseService dbService;
        private string dbPath = "farm.db";
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public SaleEditForm(Sale sale = null)
        {
            isEditMode = sale != null;
            Sale = sale != null ? new Sale
            {
                Id = sale.Id,
                ClientId = sale.ClientId,
                HarvestId = sale.HarvestId,
                Quantity = sale.Quantity,
                UnitPrice = sale.UnitPrice,
                ContractDate = sale.ContractDate,
                DeliveryDate = sale.DeliveryDate,
                Status = sale.Status,
                Notes = sale.Notes,
                CreatedDate = sale.CreatedDate
            } : new Sale { Status = "Active", CreatedDate = DateTime.Today };
            
            dbService = new DatabaseService(dbPath);
            allClients = dbService.GetAllClients();
            availableHarvests = dbService.GetAvailableHarvests();
            
            InitializeComponent();
            
            if (isEditMode) 
            {
                LoadSale();
            }
        }

        public void SetClientAndDisable(Client client)
        {
            if (txtClient != null)
            {
                txtClient.Text = client.CompanyName;
                txtClient.ReadOnly = true;
                Sale.ClientId = client.Id;
                
                if (lblTitle != null)
                {
                    lblTitle.Text = $"📄 Додавання контракту для {client.CompanyName}";
                }
            }
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування контракту" : "Додавання контракту";
            this.Size = new Size(550, 850);
            this.StartPosition = FormStartPosition.CenterParent;
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
                Text = isEditMode ? "✏️ Редагування контракту" : "📄 Додавання контракту",
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
            int labelWidth = 140;
            int controlWidth = 320;
            int spacing = 50;

            var lblClientSection = new Label
            {
                Text = "👤 КЛІЄНТ",
                Font = new Font("Arial", 12F, FontStyle.Bold),
                ForeColor = FormStyleHelper.PrimaryColor,
                Location = new Point(20, yPos),
                AutoSize = true
            };
            yPos += 35;

            var lblClient = FormStyleHelper.CreateLabel("Клієнт:");
            lblClient.Location = new Point(20, yPos);
            txtClient = FormStyleHelper.CreateStyledTextBox();
            txtClient.Location = new Point(labelWidth + 20, yPos - 3);
            txtClient.Size = new Size(controlWidth, 25);
            txtClient.ReadOnly = true;
            txtClient.BackColor = Color.FromArgb(240, 240, 240);
            txtClient.Text = "Оберіть клієнта...";
            yPos += spacing;

            var lblSaleSection = new Label
            {
                Text = "📦 ДАНІ ПРОДАЖУ",
                Font = new Font("Arial", 12F, FontStyle.Bold),
                ForeColor = FormStyleHelper.PrimaryColor,
                Location = new Point(20, yPos),
                AutoSize = true
            };
            yPos += 35;

            var lblHarvest = FormStyleHelper.CreateLabel("Урожай:");
            lblHarvest.Location = new Point(20, yPos);
            cmbHarvest = FormStyleHelper.CreateStyledComboBox();
            cmbHarvest.Location = new Point(labelWidth + 20, yPos - 3);
            cmbHarvest.Size = new Size(controlWidth, 25);
            cmbHarvest.DataSource = availableHarvests;
            cmbHarvest.DisplayMember = "GetAvailableHarvestSummary";
            cmbHarvest.ValueMember = "Id";
            cmbHarvest.SelectedIndexChanged += CmbHarvest_SelectedIndexChanged;
            yPos += spacing;

            var lblAvailableLabel = FormStyleHelper.CreateLabel("Доступно:");
            lblAvailableLabel.Location = new Point(20, yPos);
            lblAvailableQuantity = new Label
            {
                Text = "Оберіть урожай",
                Location = new Point(labelWidth + 20, yPos),
                Size = new Size(controlWidth, 20),
                Font = FormStyleHelper.RegularFont,
                ForeColor = FormStyleHelper.AccentColor
            };
            yPos += spacing;

            var lblQuantity = FormStyleHelper.CreateLabel("Кількість (кг):");
            lblQuantity.Location = new Point(20, yPos);
            numQuantity = new NumericUpDown 
            { 
                Location = new Point(labelWidth + 20, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Minimum = 1, 
                Maximum = 999999,
                DecimalPlaces = 0,
                Font = FormStyleHelper.RegularFont,
                Enabled = false
            };
            numQuantity.ValueChanged += NumQuantity_ValueChanged;
            yPos += spacing;

            var lblUnitPrice = FormStyleHelper.CreateLabel("Ціна за кг (грн):");
            lblUnitPrice.Location = new Point(20, yPos);
            txtUnitPrice = FormStyleHelper.CreateStyledTextBox();
            txtUnitPrice.Location = new Point(labelWidth + 20, yPos - 3);
            txtUnitPrice.Size = new Size(controlWidth, 25);
            txtUnitPrice.ReadOnly = true;
            txtUnitPrice.BackColor = Color.FromArgb(240, 240, 240);
            yPos += spacing;

            var lblTotalLabel = FormStyleHelper.CreateLabel("Загальна сума:");
            lblTotalLabel.Location = new Point(20, yPos);
            lblTotalAmount = new Label
            {
                Text = "0 грн",
                Location = new Point(labelWidth + 20, yPos),
                Size = new Size(controlWidth, 20),
                Font = new Font("Arial", 10F, FontStyle.Bold),
                ForeColor = FormStyleHelper.SuccessColor
            };
            yPos += spacing;

            chkIsContract = new CheckBox 
            { 
                Text = "Це контракт (з датами)", 
                Location = new Point(20, yPos), 
                AutoSize = true,
                Font = new Font("Arial", 10F, FontStyle.Bold),
                ForeColor = FormStyleHelper.AccentColor
            };
            chkIsContract.CheckedChanged += ChkIsContract_CheckedChanged;
            yPos += spacing;

            var lblContractSection = new Label
            {
                Text = "📅 ДАНІ КОНТРАКТУ",
                Font = new Font("Arial", 12F, FontStyle.Bold),
                ForeColor = FormStyleHelper.PrimaryColor,
                Location = new Point(20, yPos),
                AutoSize = true,
                Visible = false
            };
            yPos += 35;

            var lblContractDate = FormStyleHelper.CreateLabel("Дата контракту:");
            lblContractDate.Location = new Point(20, yPos);
            lblContractDate.Visible = false;
            dtpContractDate = new DateTimePicker 
            { 
                Location = new Point(labelWidth + 20, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Format = DateTimePickerFormat.Short, 
                Visible = false,
                Font = FormStyleHelper.RegularFont
            };
            yPos += spacing;

            var lblDeliveryDate = FormStyleHelper.CreateLabel("Дата постачання:");
            lblDeliveryDate.Location = new Point(20, yPos);
            lblDeliveryDate.Visible = false;
            dtpDeliveryDate = new DateTimePicker 
            { 
                Location = new Point(labelWidth + 20, yPos - 3), 
                Size = new Size(controlWidth, 25), 
                Format = DateTimePickerFormat.Short, 
                Visible = false,
                Font = FormStyleHelper.RegularFont
            };
            yPos += spacing;

            var lblStatus = FormStyleHelper.CreateLabel("Статус:");
            lblStatus.Location = new Point(20, yPos);
            cmbStatus = FormStyleHelper.CreateStyledComboBox();
            cmbStatus.Location = new Point(labelWidth + 20, yPos - 3);
            cmbStatus.Size = new Size(controlWidth, 25);
            cmbStatus.Items.AddRange(new string[] { "Active", "Completed", "Cancelled" });
            cmbStatus.SelectedItem = "Active";
            yPos += spacing;

            var lblNotes = FormStyleHelper.CreateLabel("Примітки:");
            lblNotes.Location = new Point(20, yPos);
            txtNotes = FormStyleHelper.CreateStyledTextBox();
            txtNotes.Location = new Point(labelWidth + 20, yPos - 3);
            txtNotes.Size = new Size(controlWidth, 60);
            txtNotes.Multiline = true;
            txtNotes.ScrollBars = ScrollBars.Vertical;
            yPos += 80;

            btnSave = FormStyleHelper.CreateStyledButton("💾 Зберегти", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnSave.Location = new Point(150, yPos);
            btnSave.Click += BtnSave_Click;

            btnCancel = FormStyleHelper.CreateStyledButton("❌ Скасувати", FormStyleHelper.DangerColor, new Size(120, 40));
            btnCancel.Location = new Point(290, yPos);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            var contractElements = new Control[] { lblContractSection, lblContractDate, dtpContractDate, lblDeliveryDate, dtpDeliveryDate };
            chkIsContract.Tag = contractElements;

            contentPanel.Controls.AddRange(new Control[] {
                lblClientSection,
                lblClient, txtClient,
                lblSaleSection,
                lblHarvest, cmbHarvest,
                lblAvailableLabel, lblAvailableQuantity,
                lblQuantity, numQuantity,
                lblUnitPrice, txtUnitPrice,
                lblTotalLabel, lblTotalAmount,
                chkIsContract,
                lblContractSection,
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
            var contractElements = (Control[])chkIsContract.Tag;
            
            foreach (var element in contractElements)
            {
                element.Visible = isContract;
            }
            
            if (isContract && !isEditMode)
            {
                dtpContractDate.Value = DateTime.Today;
                dtpDeliveryDate.Value = DateTime.Today.AddDays(30);
            }
        }

        private void CmbHarvest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHarvest.SelectedItem is Harvest selectedHarvest)
            {
                lblAvailableQuantity.Text = $"{selectedHarvest.AvailableQuantity} кг";
                lblAvailableQuantity.ForeColor = FormStyleHelper.AccentColor;
                
                numQuantity.Enabled = selectedHarvest.AvailableQuantity > 0 || isEditMode;
                
                if (selectedHarvest.AvailableQuantity > 0 && !isEditMode)
                {
                    numQuantity.Value = 1;
                }
                
                txtUnitPrice.Text = selectedHarvest.PricePerKg.ToString();
                
                UpdateTotalAmount();
            }
            else
            {
                lblAvailableQuantity.Text = "Оберіть урожай";
                numQuantity.Enabled = false;
                txtUnitPrice.Text = "";
                lblTotalAmount.Text = "0 грн";
            }
        }

        private void NumQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotalAmount();
            
            if (cmbHarvest.SelectedItem is Harvest selectedHarvest)
            {
                int availableQuantityForDisplay = selectedHarvest.AvailableQuantity;
                
                if (numQuantity.Value > availableQuantityForDisplay)
                {
                    lblAvailableQuantity.ForeColor = FormStyleHelper.DangerColor;
                    lblAvailableQuantity.Text = $"{availableQuantityForDisplay} кг (ПЕРЕВИЩЕНО!)";
                }
                else
                {
                    lblAvailableQuantity.ForeColor = FormStyleHelper.AccentColor;
                    lblAvailableQuantity.Text = $"{availableQuantityForDisplay} кг";
                }
            }
        }

        private void UpdateTotalAmount()
        {
            if (int.TryParse(txtUnitPrice.Text, out int unitPrice))
            {
                int totalAmount = (int)numQuantity.Value * unitPrice;
                lblTotalAmount.Text = $"{totalAmount} грн";
            }
            else
            {
                lblTotalAmount.Text = "0 грн";
            }
        }

        private void LoadSale()
        {
            var client = allClients.FirstOrDefault(c => c.Id == Sale.ClientId);
            if (client != null)
            {
                txtClient.Text = client.CompanyName;
            }
            
            var allHarvests = dbService.GetAllHarvests();
            var saleHarvest = allHarvests.FirstOrDefault(h => h.Id == Sale.HarvestId);
            if (saleHarvest != null)
            {
                if (!availableHarvests.Any(h => h.Id == saleHarvest.Id))
                {
                    saleHarvest.AvailableQuantity = dbService.GetAvailableQuantity(saleHarvest.Id) + Sale.Quantity;
                    availableHarvests.Insert(0, saleHarvest);
                    cmbHarvest.DataSource = null;
                    cmbHarvest.DataSource = availableHarvests;
                    cmbHarvest.DisplayMember = "GetAvailableHarvestSummary";
                    cmbHarvest.ValueMember = "Id";
                }
                else
                {
                    var existingHarvest = availableHarvests.FirstOrDefault(h => h.Id == saleHarvest.Id);
                    if (existingHarvest != null)
                    {
                        existingHarvest.AvailableQuantity = dbService.GetAvailableQuantity(saleHarvest.Id) + Sale.Quantity;
                        cmbHarvest.DataSource = null;
                        cmbHarvest.DataSource = availableHarvests;
                        cmbHarvest.DisplayMember = "GetAvailableHarvestSummary";
                        cmbHarvest.ValueMember = "Id";
                    }
                }
                
                cmbHarvest.SelectedValue = Sale.HarvestId;
                
                var selectedHarvest = availableHarvests.FirstOrDefault(h => h.Id == Sale.HarvestId);
                if (selectedHarvest != null)
                {
                    numQuantity.Enabled = true;
                    numQuantity.Value = Sale.Quantity;
                    
                    lblAvailableQuantity.Text = $"{selectedHarvest.AvailableQuantity} кг";
                    lblAvailableQuantity.ForeColor = FormStyleHelper.AccentColor;
                    
                    txtUnitPrice.Text = Sale.UnitPrice.ToString();
                    
                    UpdateTotalAmount();
                }
            }
            
            chkIsContract.Checked = Sale.IsContract();
            if (Sale.ContractDate.HasValue)
            {
                dtpContractDate.Value = Sale.ContractDate.Value;
            }
            if (Sale.DeliveryDate.HasValue)
            {
                dtpDeliveryDate.Value = Sale.DeliveryDate.Value;
            }
            
            cmbStatus.SelectedItem = Sale.Status ?? "Active";
            txtNotes.Text = Sale.Notes ?? "";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtClient.Text == "Оберіть клієнта..." || Sale.ClientId == 0)
            {
                MessageBox.Show("Оберіть клієнта!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbHarvest.SelectedItem == null)
            {
                MessageBox.Show("Оберіть урожай!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("Введіть кількість!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var selectedHarvest = (Harvest)cmbHarvest.SelectedItem;
            int availableQuantityForValidation = selectedHarvest.AvailableQuantity;
            
            if (numQuantity.Value > availableQuantityForValidation)
            {
                MessageBox.Show($"Введена кількість ({numQuantity.Value} кг) перевищує доступну кількість ({availableQuantityForValidation} кг)!\n\nБудь ласка, введіть кількість не більше {availableQuantityForValidation} кг.", 
                    "Недостатньо товару", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numQuantity.Focus();
                return;
            }
            
            Sale.HarvestId = (int)cmbHarvest.SelectedValue;
            Sale.Quantity = (int)numQuantity.Value;
            Sale.UnitPrice = int.Parse(txtUnitPrice.Text);
            Sale.ContractDate = chkIsContract.Checked ? (DateTime?)dtpContractDate.Value.Date : null;
            Sale.DeliveryDate = chkIsContract.Checked ? (DateTime?)dtpDeliveryDate.Value.Date : null;
            Sale.Status = cmbStatus.SelectedItem.ToString();
            Sale.Notes = txtNotes.Text.Trim();
            
            this.DialogResult = DialogResult.OK;
        }
    }
} 