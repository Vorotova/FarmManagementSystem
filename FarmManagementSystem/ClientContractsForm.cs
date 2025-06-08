using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services;

namespace FarmManagementSystem
{
    public class ClientContractsForm : Form
    {
        private Client client;
        private DatabaseService dbService;
        private List<Sale> clientSales;
        
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblClientInfo;
        private DataGridView dgvContracts;
        private Panel buttonPanel;
        private Button btnAddContract;
        private Button btnEditContract;
        private Button btnDeleteContract;

        public ClientContractsForm(Client client, DatabaseService dbService)
        {
            this.client = client;
            this.dbService = dbService;
            this.clientSales = dbService.GetSalesByClientId(client.Id);
            
            InitializeComponent();
            LoadContracts();
        }

        private void InitializeComponent()
        {
            this.Text = $"Контракти клієнта: {client.CompanyName}";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            FormStyleHelper.ApplyFormStyle(this);

            // Створюємо заголовок
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = FormStyleHelper.PrimaryColor,
                Padding = new Padding(20, 15, 20, 15)
            };

            lblTitle = new Label
            {
                Text = "📄 Контракти та продажі",
                Font = FormStyleHelper.HeaderFont,
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 15)
            };

            lblClientInfo = new Label
            {
                Text = $"Клієнт: {client.CompanyName} ({client.ContactPerson})\n" +
                       $"Телефон: {client.Phone} | Email: {client.Email ?? "Не вказано"}",
                Font = new Font("Arial", 10F, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 50)
            };

            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblClientInfo);

            // Створюємо панель для кнопок
            buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20, 15, 20, 15)
            };

            // Створюємо кнопки
            btnAddContract = FormStyleHelper.CreateStyledButton("➕ Додати контракт", FormStyleHelper.SuccessColor, new Size(140, 40));
            btnAddContract.Location = new Point(20, 15);
            btnAddContract.Click += BtnAddContract_Click;

            btnEditContract = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEditContract.Location = new Point(170, 15);
            btnEditContract.Click += BtnEditContract_Click;

            btnDeleteContract = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDeleteContract.Location = new Point(300, 15);
            btnDeleteContract.Click += BtnDeleteContract_Click;

            buttonPanel.Controls.Add(btnAddContract);
            buttonPanel.Controls.Add(btnEditContract);
            buttonPanel.Controls.Add(btnDeleteContract);

            // Створюємо DataGridView для контрактів
            dgvContracts = FormStyleHelper.CreateStyledDataGridView();
            dgvContracts.Location = new Point(20, 120); // Під заголовком
            dgvContracts.Size = new Size(960, 410); // Розмір з урахуванням панелі кнопок
            dgvContracts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dgvContracts.AllowUserToAddRows = false;
            dgvContracts.AllowUserToDeleteRows = false;
            dgvContracts.ReadOnly = true;
            dgvContracts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContracts.MultiSelect = false;
            dgvContracts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvContracts.RowHeadersVisible = false;

            // Додаємо колонки
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тип", Width = 80 });
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Продукт", Width = 120 });
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кількість (кг)", DataPropertyName = "Quantity", Width = 100 });
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ціна за кг", DataPropertyName = "UnitPrice", Width = 100 });
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Загальна сума", Width = 120 });
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата контракту", Width = 120 });
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата постачання", Width = 120 });
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Статус", DataPropertyName = "Status", Width = 100 });
            dgvContracts.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата створення", Width = 120 });

            // Додаємо контроли в правильному порядку
            this.Controls.Add(headerPanel);    // Зверху
            this.Controls.Add(buttonPanel);    // Знизу
            this.Controls.Add(dgvContracts);   // Заповнює середину
        }

        private void LoadContracts()
        {
            // Очищуємо таблицю
            dgvContracts.DataSource = null;
            dgvContracts.Rows.Clear();
            
            // Оновлюємо дані з бази
            clientSales = dbService.GetSalesByClientId(client.Id);
            
            // Додаємо дані до таблиці
            foreach (var sale in clientSales)
            {
                try
                {
                    string saleType = sale.GetSaleType();
                    string productType = sale.GetProductType();
                    
                    var row = new object[]
                    {
                        sale.Id,
                        saleType,
                        productType,
                        sale.Quantity,
                        sale.UnitPrice,
                        sale.CalculateTotalAmount(),
                        sale.ContractDate?.ToString("dd.MM.yyyy") ?? "",
                        sale.DeliveryDate?.ToString("dd.MM.yyyy") ?? "",
                        sale.Status,
                        sale.CreatedDate.ToString("dd.MM.yyyy")
                    };
                    dgvContracts.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при додаванні рядка: {ex.Message}", "Помилка");
                }
            }

            // Оновлюємо інформацію про клієнта в заголовку
            int totalContracts = clientSales.Count;
            int totalAmount = clientSales.Sum(s => s.CalculateTotalAmount());
            
            lblClientInfo.Text = $"Клієнт: {client.CompanyName} ({client.ContactPerson})\n" +
                                $"Телефон: {client.Phone} | Email: {client.Email ?? "Не вказано"}\n" +
                                $"Всього контрактів: {totalContracts} | Загальна сума: {totalAmount} грн";
                                
            // Переконуємося, що таблиця видима та оновлена
            dgvContracts.Visible = true;
            dgvContracts.AutoResizeColumns();
            dgvContracts.Refresh();
            dgvContracts.Invalidate();
        }

        private void BtnAddContract_Click(object sender, EventArgs e)
        {
            var saleForm = new SaleEditForm();
            
            // Встановлюємо поточного клієнта та робимо поле неактивним
            saleForm.SetClientAndDisable(client);
            
            if (saleForm.ShowDialog() == DialogResult.OK)
            {
                // Переконуємося, що контракт створюється для поточного клієнта
                saleForm.Sale.ClientId = client.Id;
                dbService.AddSale(saleForm.Sale);
                LoadContracts(); // Оновлюємо відображення
            }
        }

        private void BtnEditContract_Click(object sender, EventArgs e)
        {
            if (dgvContracts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Оберіть контракт для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIndex = dgvContracts.SelectedRows[0].Index;
            var selectedSale = clientSales[selectedIndex];
            
            var saleForm = new SaleEditForm(selectedSale);
            if (saleForm.ShowDialog() == DialogResult.OK)
            {
                dbService.UpdateSale(saleForm.Sale);
                LoadContracts(); // Оновлюємо відображення
            }
        }

        private void BtnDeleteContract_Click(object sender, EventArgs e)
        {
            if (dgvContracts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Оберіть контракт для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedIndex = dgvContracts.SelectedRows[0].Index;
            var selectedSale = clientSales[selectedIndex];
            
            var result = MessageBox.Show(
                $"Ви дійсно бажаєте видалити {selectedSale.GetSaleType().ToLower()} на {selectedSale.GetProductType()}?\n" +
                $"Кількість: {selectedSale.Quantity} кг, Сума: {selectedSale.CalculateTotalAmount()} грн",
                "Підтвердження", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);
                
            if (result == DialogResult.Yes)
            {
                dbService.DeleteSale(selectedSale.Id);
                LoadContracts(); // Оновлюємо відображення
            }
        }
    }
} 