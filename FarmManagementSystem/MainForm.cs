using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;
using FarmManagementSystem.Services;

namespace FarmManagementSystem
{
    public partial class MainForm : Form
    {
        private Button btnOpenCultures;
        private Button btnOpenFields;
        private Button btnOpenEmployees;
        private Button btnOpenTechniques;
        private Button btnOpenMaterialTypes;
        private Button btnOpenSuppliers;
        private Button btnOpenPlantings;
        private Button btnOpenHarvests;
        private Button btnOpenWorks;
        private Button btnOpenPurchases;
        private Button btnOpenMaterialUsages;
        private Button btnOpenExpenses;
        private Button btnOpenClients;
        private Panel leftPanel;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;
        private List<Button> allButtons;
        private Button activeButton;
        private DatabaseService dbService;

        private readonly Color DefaultButtonColor = Color.FromArgb(70, 130, 180); 
        private readonly Color ActiveButtonColor = Color.FromArgb(50, 100, 150);  
        private readonly Color HoverButtonColor = Color.FromArgb(80, 140, 190);   

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Система управління фермерським господарством";
            this.WindowState = FormWindowState.Maximized;
            FormStyleHelper.ApplyFormStyle(this);
            allButtons = new List<Button>();
            dbService = new DatabaseService("farm.db");
            InitializeHeaderPanel();
            InitializeLeftPanel();
            InitializeContentPanel();
            CreateAllButtons();
            ShowWelcomeContent();
        }

        private void InitializeHeaderPanel()
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = FormStyleHelper.PrimaryColor,
                Padding = new Padding(20)
            };

            lblTitle = new Label
            {
                Text = "🌾 Система управління фермерським господарством",
                Font = new Font("Arial", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 25)
            };

            headerPanel.Controls.Add(lblTitle);
            this.Controls.Add(headerPanel);
        }

        private void InitializeLeftPanel()
        {
            leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 280,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(15, 30, 15, 20)
            };

            leftPanel.Paint += (s, e) =>
            {
                using (var brush = new SolidBrush(Color.FromArgb(20, Color.Black)))
                {
                    e.Graphics.FillRectangle(brush, leftPanel.Width - 2, 0, 2, leftPanel.Height);
                }
            };

            this.Controls.Add(leftPanel);
        }

        private void InitializeContentPanel()
        {
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = FormStyleHelper.BackgroundColor,
                Padding = new Padding(20)
            };

            this.Controls.Add(contentPanel);
        }

        private void ShowWelcomeContent()
        {
            ClearContentPanel();
            
            var welcomeLabel = new Label
            {
                Text = "Вітаємо в системі управління фермерським господарством!\n\nОберіть розділ для роботи.",
                Font = new Font("Arial", 16F, FontStyle.Regular),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(320, 100),
                TextAlign = ContentAlignment.TopLeft
            };

            contentPanel.Controls.Add(welcomeLabel);
        }

        private void ClearContentPanel()
        {
            contentPanel.Controls.Clear();
        }

        private Button CreateMenuButton(string text, string icon, EventHandler clickHandler)
        {
            var button = new Button
            {
                Text = $"{icon}  {text}",
                Size = new Size(250, 45),
                Font = new Font("Arial", 11F, FontStyle.Regular),
                BackColor = DefaultButtonColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Margin = new Padding(0, 0, 0, 5)
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = HoverButtonColor;

            button.Click += (s, e) =>
            {
                SetActiveButton(button);
                clickHandler?.Invoke(s, e);
            };

            button.MouseEnter += (s, e) =>
            {
                if (button != activeButton)
                {
                    button.BackColor = HoverButtonColor;
                }
            };

            button.MouseLeave += (s, e) =>
            {
                if (button != activeButton)
                {
                    button.BackColor = DefaultButtonColor;
                }
            };

            return button;
        }

        private void SetActiveButton(Button button)
        {
            if (activeButton != null)
            {
                activeButton.BackColor = DefaultButtonColor;
            }

            activeButton = button;
            activeButton.BackColor = ActiveButtonColor;
        }

        private void CreateAllButtons()
        {
            int yPos = 20;
            int spacing = 50; 

            btnOpenCultures = CreateMenuButton("Культури", "🌱", BtnOpenCultures_Click);
            btnOpenCultures.Location = new Point(15, yPos);
            allButtons.Add(btnOpenCultures);
            yPos += spacing;

            btnOpenFields = CreateMenuButton("Поля", "🏞️", BtnOpenFields_Click);
            btnOpenFields.Location = new Point(15, yPos);
            allButtons.Add(btnOpenFields);
            yPos += spacing;

            btnOpenEmployees = CreateMenuButton("Працівники", "👥", BtnOpenEmployees_Click);
            btnOpenEmployees.Location = new Point(15, yPos);
            allButtons.Add(btnOpenEmployees);
            yPos += spacing;

            btnOpenTechniques = CreateMenuButton("Техніка", "🚜", BtnOpenTechniques_Click);
            btnOpenTechniques.Location = new Point(15, yPos);
            allButtons.Add(btnOpenTechniques);
            yPos += spacing;

            btnOpenMaterialTypes = CreateMenuButton("Типи матеріалів", "📦", BtnOpenMaterialTypes_Click);
            btnOpenMaterialTypes.Location = new Point(15, yPos);
            allButtons.Add(btnOpenMaterialTypes);
            yPos += spacing;

            btnOpenSuppliers = CreateMenuButton("Постачальники", "🏪", BtnOpenSuppliers_Click);
            btnOpenSuppliers.Location = new Point(15, yPos);
            allButtons.Add(btnOpenSuppliers);
            yPos += spacing;

            btnOpenPlantings = CreateMenuButton("Посадки", "🌿", BtnOpenPlantings_Click);
            btnOpenPlantings.Location = new Point(15, yPos);
            allButtons.Add(btnOpenPlantings);
            yPos += spacing;

            btnOpenHarvests = CreateMenuButton("Урожай", "🌾", BtnOpenHarvests_Click);
            btnOpenHarvests.Location = new Point(15, yPos);
            allButtons.Add(btnOpenHarvests);
            yPos += spacing;

            btnOpenWorks = CreateMenuButton("Роботи", "⚒️", BtnOpenWorks_Click);
            btnOpenWorks.Location = new Point(15, yPos);
            allButtons.Add(btnOpenWorks);
            yPos += spacing;

            btnOpenPurchases = CreateMenuButton("Закупки та Контракти", "💰", BtnOpenPurchases_Click);
            btnOpenPurchases.Location = new Point(15, yPos);
            allButtons.Add(btnOpenPurchases);
            yPos += spacing;

            btnOpenMaterialUsages = CreateMenuButton("Використання матеріалу", "📊", BtnOpenMaterialUsages_Click);
            btnOpenMaterialUsages.Location = new Point(15, yPos);
            allButtons.Add(btnOpenMaterialUsages);
            yPos += spacing;

            btnOpenExpenses = CreateMenuButton("Витрати", "💸", BtnOpenExpenses_Click);
            btnOpenExpenses.Location = new Point(15, yPos);
            allButtons.Add(btnOpenExpenses);
            yPos += spacing;

            btnOpenClients = CreateMenuButton("Клієнти та Продажі", "🤝", BtnOpenClients_Click);
            btnOpenClients.Location = new Point(15, yPos);
            allButtons.Add(btnOpenClients);
            yPos += spacing;

            foreach (var button in allButtons)
            {
                leftPanel.Controls.Add(button);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }

        
        private void ShowCulturesContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "🌱 Культури",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var filterPanel = new Panel
            {
                Location = new Point(300, 120),
                Size = new Size(contentPanel.Width - 330, 40),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            var lblSearch = new Label 
            { 
                Text = "Пошук:", 
                Location = new Point(10, 12), 
                AutoSize = true,
                Font = FormStyleHelper.RegularFont
            };
            
            var txtSearch = new TextBox 
            { 
                Location = new Point(60, 10), 
                Width = 180,
                Font = FormStyleHelper.RegularFont
            };
            
            var lblSeasonality = new Label 
            { 
                Text = "Сезонність:", 
                Location = new Point(260, 12), 
                AutoSize = true,
                Font = FormStyleHelper.RegularFont
            };
            
            var cmbSeasonality = FormStyleHelper.CreateStyledComboBox();
            cmbSeasonality.Location = new Point(340, 10);
            cmbSeasonality.Width = 120;
            cmbSeasonality.Items.AddRange(new object[] { "Всі", "Весна", "Літо", "Осінь", "Зима", "Цілий рік" });
            cmbSeasonality.SelectedIndex = 0;

            filterPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblSeasonality, cmbSeasonality });

            var dgvCultures = FormStyleHelper.CreateStyledDataGridView();
            dgvCultures.Location = new Point(300, 180);
            dgvCultures.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 275);
            dgvCultures.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvCultures.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvCultures.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Назва", DataPropertyName = "Name", Width = 200 });
            dgvCultures.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Сезонність", DataPropertyName = "Seasonality", Width = 150 });
            dgvCultures.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Середня врожайність", DataPropertyName = "AverageYield", Width = 150 });

            var allCultures = dbService.GetAllCultures();
            
            Action applyFilter = () =>
            {
                string search = txtSearch.Text.Trim().ToLower();
                string season = cmbSeasonality.SelectedItem?.ToString();
                var filtered = allCultures;
                
                if (!string.IsNullOrWhiteSpace(search))
                    filtered = filtered.FindAll(c => c.Name != null && c.Name.ToLower().Contains(search));
                    
                if (!string.IsNullOrWhiteSpace(season) && season != "Всі")
                    filtered = filtered.FindAll(c => c.Seasonality != null && c.Seasonality.Equals(season, StringComparison.OrdinalIgnoreCase));
                    
                dgvCultures.DataSource = null;
                dgvCultures.DataSource = filtered;
            };

            txtSearch.TextChanged += (s, e) => applyFilter();
            cmbSeasonality.SelectedIndexChanged += (s, e) => applyFilter();

            applyFilter();

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvCultures.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new CultureEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddCulture(editForm.Culture);
                    ShowCulturesContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvCultures.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть культуру для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvCultures.SelectedRows[0].DataBoundItem as Culture;
                var editForm = new CultureEditForm(selected);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateCulture(editForm.Culture);
                    ShowCulturesContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvCultures.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть культуру для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvCultures.SelectedRows[0].DataBoundItem as Culture;
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити культуру '{selected.Name}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteCulture(selected.Id);
                    ShowCulturesContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(filterPanel);
            contentPanel.Controls.Add(dgvCultures);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowFieldsContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "🏞️ Поля",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var filterPanel = new Panel
            {
                Location = new Point(300, 120),
                Size = new Size(contentPanel.Width - 330, 40),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            var lblSearch = new Label 
            { 
                Text = "Пошук:", 
                Location = new Point(10, 12), 
                AutoSize = true,
                Font = FormStyleHelper.RegularFont
            };
            
            var txtSearch = new TextBox 
            { 
                Location = new Point(60, 10), 
                Width = 180,
                Font = FormStyleHelper.RegularFont
            };
            
            var lblSoilType = new Label 
            { 
                Text = "Тип ґрунту:", 
                Location = new Point(260, 12), 
                AutoSize = true,
                Font = FormStyleHelper.RegularFont
            };
            
            var cmbSoilType = FormStyleHelper.CreateStyledComboBox();
            cmbSoilType.Location = new Point(340, 10);
            cmbSoilType.Width = 120;
            cmbSoilType.Items.AddRange(new object[] { "Всі", "Чорнозем", "Суглинок", "Супісок", "Глина", "Пісок" });
            cmbSoilType.SelectedIndex = 0;

            filterPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblSoilType, cmbSoilType });

            var dgvFields = FormStyleHelper.CreateStyledDataGridView();
            dgvFields.Location = new Point(300, 180);
            dgvFields.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 275);
            dgvFields.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvFields.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvFields.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Назва", DataPropertyName = "Name", Width = 200 });
            dgvFields.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Площа (га)", DataPropertyName = "Area", Width = 100 });
            dgvFields.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тип ґрунту", DataPropertyName = "SoilType", Width = 150 });

            var allFields = dbService.GetAllFields();
            
            Action applyFilter = () =>
            {
                string search = txtSearch.Text.Trim().ToLower();
                string soilType = cmbSoilType.SelectedItem?.ToString();
                var filtered = allFields;
                
                if (!string.IsNullOrWhiteSpace(search))
                    filtered = filtered.FindAll(f => f.Name != null && f.Name.ToLower().Contains(search));
                    
                if (!string.IsNullOrWhiteSpace(soilType) && soilType != "Всі")
                    filtered = filtered.FindAll(f => f.SoilType != null && f.SoilType.Equals(soilType, StringComparison.OrdinalIgnoreCase));
                    
                dgvFields.DataSource = null;
                dgvFields.DataSource = filtered;
            };

            txtSearch.TextChanged += (s, e) => applyFilter();
            cmbSoilType.SelectedIndexChanged += (s, e) => applyFilter();

            applyFilter();

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvFields.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new FieldEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddField(editForm.Field);
                    ShowFieldsContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvFields.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть поле для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvFields.SelectedRows[0].DataBoundItem as Field;
                var editForm = new FieldEditForm(selected);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateField(editForm.Field);
                    ShowFieldsContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvFields.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть поле для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvFields.SelectedRows[0].DataBoundItem as Field;
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити поле '{selected.Name}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteField(selected.Id);
                    ShowFieldsContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(filterPanel);
            contentPanel.Controls.Add(dgvFields);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowEmployeesContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "👥 Працівники",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var dgvEmployees = FormStyleHelper.CreateStyledDataGridView();
            dgvEmployees.Location = new Point(300, 130);
            dgvEmployees.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 225);
            dgvEmployees.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ПІБ", DataPropertyName = "FullName", Width = 200 });
            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Посада", DataPropertyName = "Position", Width = 150 });
            dgvEmployees.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Телефон", DataPropertyName = "Phone", Width = 150 });

            var allEmployees = dbService.GetAllEmployees();
            dgvEmployees.DataSource = allEmployees;

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvEmployees.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new EmployeeEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddEmployee(editForm.Employee);
                    ShowEmployeesContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvEmployees.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть працівника для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;
                var editForm = new EmployeeEditForm(selected);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateEmployee(editForm.Employee);
                    ShowEmployeesContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvEmployees.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть працівника для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити працівника '{selected.FullName}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteEmployee(selected.Id);
                    ShowEmployeesContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(dgvEmployees);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowTechniquesContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "🚜 Техніка",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var filterPanel = new Panel
            {
                Location = new Point(300, 120),
                Size = new Size(contentPanel.Width - 330, 40),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            var lblSearch = new Label 
            { 
                Text = "Пошук:", 
                Location = new Point(10, 12), 
                AutoSize = true,
                Font = FormStyleHelper.RegularFont
            };
            
            var txtSearch = new TextBox 
            { 
                Location = new Point(60, 10), 
                Width = 180,
                Font = FormStyleHelper.RegularFont
            };
            
            var lblType = new Label 
            { 
                Text = "Тип техніки:", 
                Location = new Point(260, 12), 
                AutoSize = true,
                Font = FormStyleHelper.RegularFont
            };
            
            var cmbType = FormStyleHelper.CreateStyledComboBox();
            cmbType.Location = new Point(340, 10);
            cmbType.Width = 200;
            cmbType.Items.AddRange(new object[] { 
                "Всі", 
                "Трактори та міні-трактори",
                "Ґрунтообробні механізми",
                "Посівне обладнання",
                "Механізми для догляду за посівами",
                "Збиральна техніка",
                "Додаткове обладнання"
            });
            cmbType.SelectedIndex = 0;

            filterPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblType, cmbType });

            var dgvTechniques = FormStyleHelper.CreateStyledDataGridView();
            dgvTechniques.Location = new Point(300, 180);
            dgvTechniques.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 275);
            dgvTechniques.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvTechniques.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvTechniques.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Назва", DataPropertyName = "Name", Width = 250 });
            dgvTechniques.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тип", DataPropertyName = "Type", Width = 200 });
            dgvTechniques.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Вартість використання", DataPropertyName = "UsageCost", Width = 150 });
            dgvTechniques.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Стан", DataPropertyName = "Condition", Width = 100 });

            var allTechniques = dbService.GetAllTechniques();
            
            Action applyFilter = () =>
            {
                string search = txtSearch.Text.Trim().ToLower();
                string type = cmbType.SelectedItem?.ToString();
                var filtered = allTechniques;
                
                if (!string.IsNullOrWhiteSpace(search))
                    filtered = filtered.FindAll(t => t.Name != null && t.Name.ToLower().Contains(search));
                    
                if (!string.IsNullOrWhiteSpace(type) && type != "Всі")
                    filtered = filtered.FindAll(t => t.Type != null && t.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
                    
                dgvTechniques.DataSource = null;
                dgvTechniques.DataSource = filtered;
            };

            txtSearch.TextChanged += (s, e) => applyFilter();
            cmbType.SelectedIndexChanged += (s, e) => applyFilter();

            applyFilter();

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvTechniques.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new TechniqueEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddTechnique(editForm.Technique);
                    ShowTechniquesContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvTechniques.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть техніку для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvTechniques.SelectedRows[0].DataBoundItem as Technique;
                var editForm = new TechniqueEditForm(selected);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateTechnique(editForm.Technique);
                    ShowTechniquesContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvTechniques.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть техніку для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvTechniques.SelectedRows[0].DataBoundItem as Technique;
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити техніку '{selected.Name}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteTechnique(selected.Id);
                    ShowTechniquesContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(filterPanel);
            contentPanel.Controls.Add(dgvTechniques);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowPlantingsContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "🌿 Посадки",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var dgvPlantings = FormStyleHelper.CreateStyledDataGridView();
            dgvPlantings.Location = new Point(300, 130);
            dgvPlantings.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 225);
            dgvPlantings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvPlantings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvPlantings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Поле", DataPropertyName = "FieldName", Width = 200 });
            dgvPlantings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Культура", DataPropertyName = "CultureName", Width = 200 });
            dgvPlantings.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата посіву", DataPropertyName = "SowingDate", Width = 150 });

            var allPlantings = dbService.GetAllPlantings();
            
            var displayData = allPlantings.ConvertAll(p => new
            {
                p.Id,
                FieldName = p.Field?.Name ?? "Невідоме поле",
                CultureName = p.Culture?.Name ?? "Невідома культура",
                SowingDate = p.SowingDate.ToShortDateString()
            });
            
            dgvPlantings.DataSource = displayData;

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvPlantings.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new PlantingEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddPlanting(editForm.Planting);
                    ShowPlantingsContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvPlantings.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть посадку для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvPlantings.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var planting = allPlantings.Find(p => p.Id == selectedId);
                var editForm = new PlantingEditForm(planting);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdatePlanting(editForm.Planting);
                    ShowPlantingsContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvPlantings.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть посадку для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvPlantings.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var planting = allPlantings.Find(p => p.Id == selectedId);
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити посадку '{planting.Culture?.Name}' на полі '{planting.Field?.Name}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeletePlanting(planting.Id);
                    ShowPlantingsContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(dgvPlantings);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowHarvestsContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "🌾 Урожай",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var dgvHarvests = FormStyleHelper.CreateStyledDataGridView();
            dgvHarvests.Location = new Point(300, 130);
            dgvHarvests.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 225);
            dgvHarvests.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvHarvests.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvHarvests.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Поле", DataPropertyName = "FieldName", Width = 150 });
            dgvHarvests.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Культура", DataPropertyName = "CultureName", Width = 150 });
            dgvHarvests.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата збору", DataPropertyName = "HarvestDate", Width = 120 });
            dgvHarvests.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Об'єм (кг)", DataPropertyName = "Volume", Width = 100 });
            dgvHarvests.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ціна за кг (грн)", DataPropertyName = "PricePerKg", Width = 120 });
            dgvHarvests.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Загальна вартість (грн)", DataPropertyName = "TotalValue", Width = 150 });

            var allHarvests = dbService.GetAllHarvests();
            
            var displayData = allHarvests.ConvertAll(h => new
            {
                h.Id,
                FieldName = h.Field?.Name ?? "Невідоме поле",
                CultureName = h.Culture?.Name ?? "Невідома культура",
                HarvestDate = h.HarvestDate.ToShortDateString(),
                h.Volume,
                h.PricePerKg,
                TotalValue = h.CalculateTotalValue()
            });
            
            dgvHarvests.DataSource = displayData;

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvHarvests.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new HarvestEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddHarvest(editForm.Harvest);
                    ShowHarvestsContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvHarvests.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть урожай для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvHarvests.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var harvest = allHarvests.Find(h => h.Id == selectedId);
                var editForm = new HarvestEditForm(harvest);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateHarvest(editForm.Harvest);
                    ShowHarvestsContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvHarvests.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть урожай для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvHarvests.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var harvest = allHarvests.Find(h => h.Id == selectedId);
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити урожай '{harvest.Culture?.Name}' з поля '{harvest.Field?.Name}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteHarvest(harvest.Id);
                    ShowHarvestsContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(dgvHarvests);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowMaterialTypesContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "📦 Типи матеріалів",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var filterPanel = new Panel
            {
                Location = new Point(300, 120),
                Size = new Size(contentPanel.Width - 330, 40),
                BackColor = Color.FromArgb(250, 250, 250)
            };

            var lblSearch = new Label 
            { 
                Text = "Пошук:", 
                Location = new Point(10, 12), 
                AutoSize = true,
                Font = FormStyleHelper.RegularFont
            };
            
            var txtSearch = new TextBox 
            { 
                Location = new Point(60, 10), 
                Width = 180,
                Font = FormStyleHelper.RegularFont
            };
            
            var lblType = new Label 
            { 
                Text = "Тип матеріалу:", 
                Location = new Point(260, 12), 
                AutoSize = true,
                Font = FormStyleHelper.RegularFont
            };
            
            var cmbType = FormStyleHelper.CreateStyledComboBox();
            cmbType.Location = new Point(360, 10);
            cmbType.Width = 180;
            cmbType.Items.AddRange(new object[] { 
                "Всі", 
                "Насіння", 
                "Добрива", 
                "Засоби захисту рослин", 
                "Паливо", 
                "Інше"
            });
            cmbType.SelectedIndex = 0;

            filterPanel.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblType, cmbType });

            var dgvMaterialTypes = FormStyleHelper.CreateStyledDataGridView();
            dgvMaterialTypes.Location = new Point(300, 180);
            dgvMaterialTypes.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 275);
            dgvMaterialTypes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvMaterialTypes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvMaterialTypes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Назва матеріалу", DataPropertyName = "Name", Width = 250 });
            dgvMaterialTypes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тип", DataPropertyName = "Type", Width = 200 });
            dgvMaterialTypes.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Одиниця виміру", DataPropertyName = "Unit", Width = 150 });

            var allMaterialTypes = dbService.GetAllMaterialTypes();
            
            Action applyFilter = () =>
            {
                string search = txtSearch.Text.Trim().ToLower();
                string type = cmbType.SelectedItem?.ToString();
                var filtered = allMaterialTypes;
                
                if (!string.IsNullOrWhiteSpace(search))
                    filtered = filtered.FindAll(m => m.Name != null && m.Name.ToLower().Contains(search));
                    
                if (!string.IsNullOrWhiteSpace(type) && type != "Всі")
                    filtered = filtered.FindAll(m => m.Type != null && m.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
                    
                dgvMaterialTypes.DataSource = null;
                dgvMaterialTypes.DataSource = filtered;
            };

            txtSearch.TextChanged += (s, e) => applyFilter();
            cmbType.SelectedIndexChanged += (s, e) => applyFilter();

            applyFilter();

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvMaterialTypes.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new MaterialTypeEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddMaterialType(editForm.MaterialType);
                    ShowMaterialTypesContent();
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvMaterialTypes.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть тип матеріалу для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvMaterialTypes.SelectedRows[0].DataBoundItem as MaterialType;
                var editForm = new MaterialTypeEditForm(selected);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateMaterialType(editForm.MaterialType);
                    ShowMaterialTypesContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvMaterialTypes.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть тип матеріалу для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvMaterialTypes.SelectedRows[0].DataBoundItem as MaterialType;
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити тип матеріалу '{selected.Name}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteMaterialType(selected.Id);
                    ShowMaterialTypesContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(filterPanel);
            contentPanel.Controls.Add(dgvMaterialTypes);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowPurchasesContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "🛒 Закупки та Контракти",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var dgvPurchases = FormStyleHelper.CreateStyledDataGridView();
            dgvPurchases.Location = new Point(300, 130);
            dgvPurchases.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 225);
            dgvPurchases.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Матеріал", DataPropertyName = "MaterialName", Width = 150 });
            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Постачальник", DataPropertyName = "SupplierName", Width = 120 });
            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата", DataPropertyName = "Date", Width = 100 });
            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кількість", DataPropertyName = "Quantity", Width = 80 });
            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ціна за од.", DataPropertyName = "UnitPrice", Width = 80 });
            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Загальна вартість", DataPropertyName = "TotalCost", Width = 100 });
            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата контракту", DataPropertyName = "ContractDate", Width = 100 });
            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата постачання", DataPropertyName = "DeliveryDate", Width = 100 });
            dgvPurchases.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Статус", DataPropertyName = "Status", Width = 80 });

            var allPurchases = dbService.GetAllPurchases();
            
            var displayData = allPurchases.ConvertAll(p => new
            {
                p.Id,
                MaterialName = p.Material?.Name ?? "Невідомий матеріал",
                SupplierName = p.Supplier?.Name ?? "Невідомий постачальник",
                Date = p.Date.ToShortDateString(),
                p.Quantity,
                p.UnitPrice,
                TotalCost = p.Quantity * p.UnitPrice,
                ContractDate = p.ContractDate?.ToShortDateString() ?? "",
                DeliveryDate = p.DeliveryDate?.ToShortDateString() ?? "",
                p.Status
            });
            
            dgvPurchases.DataSource = displayData;

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvPurchases.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new PurchaseEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddPurchase(editForm.Purchase);
                    ShowPurchasesContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvPurchases.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть закупку для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvPurchases.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var selected = allPurchases.Find(p => p.Id == selectedId);
                var editForm = new PurchaseEditForm(selected);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdatePurchase(editForm.Purchase);
                    ShowPurchasesContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvPurchases.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть закупку для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvPurchases.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var selected = allPurchases.Find(p => p.Id == selectedId);
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити закупку ID {selected.Id}?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeletePurchase(selected.Id);
                    ShowPurchasesContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(dgvPurchases);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowClientsContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "🤝 Клієнти",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var dgvClients = FormStyleHelper.CreateStyledDataGridView();
            dgvClients.Location = new Point(300, 130);
            dgvClients.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 225);
            dgvClients.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 60 });
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Назва компанії", DataPropertyName = "CompanyName", Width = 250 });
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Контактна особа", DataPropertyName = "ContactPerson", Width = 180 });
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Телефон", DataPropertyName = "Phone", Width = 130 });
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", Width = 200 });
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кількість контрактів", Width = 140 });
            
            var btnColumn = new DataGridViewButtonColumn
            {
                HeaderText = "Контракти",
                Text = "Переглянути",
                UseColumnTextForButtonValue = true,
                Width = 120
            };
            dgvClients.Columns.Add(btnColumn);

            var allClients = dbService.GetAllClients();
            
            foreach (var client in allClients)
            {
                int contractsCount = dbService.GetClientContractsCount(client.Id);
                var row = new object[]
                {
                    client.Id,
                    client.CompanyName,
                    client.ContactPerson,
                    client.Phone,
                    client.Email ?? "",
                    contractsCount
                };
                dgvClients.Rows.Add(row);
            }

            dgvClients.CellClick += (s, e) =>
            {
                if (e.ColumnIndex == dgvClients.Columns.Count - 1 && e.RowIndex >= 0) 
                {
                    var selectedClient = allClients[e.RowIndex];
                    ShowClientContractsForm(selectedClient);
                }
            };

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvClients.Bottom + 5),
                Size = new Size(500, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати клієнта", FormStyleHelper.SuccessColor, new Size(140, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new ClientEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        dbService.AddClient(editForm.Client);
                        ShowClientsContent(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при додаванні клієнта: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати клієнта", FormStyleHelper.AccentColor, new Size(140, 40));
            btnEdit.Location = new Point(150, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvClients.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть клієнта для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedIndex = dgvClients.SelectedRows[0].Index;
                var selectedClient = allClients[selectedIndex];
                var editForm = new ClientEditForm(selectedClient);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        dbService.UpdateClient(editForm.Client);
                        ShowClientsContent(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при оновленні клієнта: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити клієнта", FormStyleHelper.DangerColor, new Size(140, 40));
            btnDelete.Location = new Point(300, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvClients.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть клієнта для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedIndex = dgvClients.SelectedRows[0].Index;
                var selectedClient = allClients[selectedIndex];
                
                int contractsCount = dbService.GetClientContractsCount(selectedClient.Id);
                string message = contractsCount > 0 
                    ? $"Ви дійсно бажаєте видалити клієнта '{selectedClient.CompanyName}'?\nЦе також видалить всі {contractsCount} контракт(и/ів) цього клієнта!"
                    : $"Ви дійсно бажаєте видалити клієнта '{selectedClient.CompanyName}'?";
                    
                var result = MessageBox.Show(message, "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteClient(selectedClient.Id);
                    ShowClientsContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(dgvClients);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowClientContractsForm(Client client)
        {
            var contractsForm = new ClientContractsForm(client, dbService);
            contractsForm.ShowDialog();
            ShowClientsContent(); 
        }

        private void ShowSuppliersContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "🏪 Постачальники",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var dgvSuppliers = FormStyleHelper.CreateStyledDataGridView();
            dgvSuppliers.Location = new Point(300, 130);
            dgvSuppliers.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 225);
            dgvSuppliers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Назва компанії", DataPropertyName = "Name", Width = 250 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Контактна особа", DataPropertyName = "ContactPerson", Width = 180 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Телефон", DataPropertyName = "Phone", Width = 150 });
            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тип продукції", DataPropertyName = "ProductType", Width = 180 });

            var allSuppliers = dbService.GetAllSuppliers();
            dgvSuppliers.DataSource = allSuppliers;

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvSuppliers.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new SupplierEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddSupplier(editForm.Supplier);
                    ShowSuppliersContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvSuppliers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть постачальника для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvSuppliers.SelectedRows[0].DataBoundItem as Supplier;
                var editForm = new SupplierEditForm(selected);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateSupplier(editForm.Supplier);
                    ShowSuppliersContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvSuppliers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть постачальника для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selected = dgvSuppliers.SelectedRows[0].DataBoundItem as Supplier;
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити постачальника '{selected.Name}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteSupplier(selected.Id);
                    ShowSuppliersContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(dgvSuppliers);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowWorksContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "⚒️ Роботи",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var dgvWorks = FormStyleHelper.CreateStyledDataGridView();
            dgvWorks.Location = new Point(300, 130);
            dgvWorks.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 225);
            dgvWorks.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvWorks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvWorks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тип роботи", DataPropertyName = "WorkTypeName", Width = 150 });
            dgvWorks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Поле", DataPropertyName = "FieldName", Width = 120 });
            dgvWorks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Техніка", DataPropertyName = "TechniqueName", Width = 150 });
            dgvWorks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Працівник", DataPropertyName = "EmployeeName", Width = 150 });
            dgvWorks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата", DataPropertyName = "Date", Width = 100 });
            dgvWorks.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тривалість (год)", DataPropertyName = "Duration", Width = 120 });

            var allWorks = dbService.GetAllWorks();
            
            var displayData = allWorks.ConvertAll(w => new
            {
                w.Id,
                WorkTypeName = w.WorkType?.Name ?? "Невідомий тип",
                FieldName = w.Field?.Name ?? "Невідоме поле",
                TechniqueName = w.Technique?.Name ?? "Не призначено",
                EmployeeName = w.Employee?.FullName ?? "Не призначено",
                Date = w.Date.ToShortDateString(),
                w.Duration
            });
            
            dgvWorks.DataSource = displayData;

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvWorks.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new WorkEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddWork(editForm.Work);
                    ShowWorksContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvWorks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть роботу для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvWorks.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var work = allWorks.Find(w => w.Id == selectedId);
                var editForm = new WorkEditForm(work);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateWork(editForm.Work);
                    ShowWorksContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvWorks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть роботу для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvWorks.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var work = allWorks.Find(w => w.Id == selectedId);
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити роботу '{work.WorkType?.Name}' на полі '{work.Field?.Name}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteWork(work.Id);
                    ShowWorksContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(dgvWorks);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowMaterialUsagesContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "📊 Використання матеріалу",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var dgvMaterialUsages = FormStyleHelper.CreateStyledDataGridView();
            dgvMaterialUsages.Location = new Point(300, 130);
            dgvMaterialUsages.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 225);
            dgvMaterialUsages.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvMaterialUsages.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvMaterialUsages.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Матеріал", DataPropertyName = "MaterialName", Width = 200 });
            dgvMaterialUsages.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Одиниця", DataPropertyName = "Unit", Width = 100 });
            dgvMaterialUsages.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кількість", DataPropertyName = "Quantity", Width = 100 });
            dgvMaterialUsages.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Робота", DataPropertyName = "WorkDetails", Width = 250 });

            var allMaterialUsages = dbService.GetAllMaterialUsages();
            
            var displayData = allMaterialUsages.ConvertAll(u => new
            {
                u.Id,
                MaterialName = u.MaterialType?.Name ?? "Невідомий матеріал",
                Unit = u.MaterialType?.Unit ?? "од.",
                u.Quantity,
                WorkDetails = $"{u.Work?.WorkType?.Name ?? "Невідома робота"} на полі {u.Work?.Field?.Name ?? "Невідоме поле"}"
            });
            
            dgvMaterialUsages.DataSource = displayData;

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvMaterialUsages.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new MaterialUsageEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddMaterialUsage(editForm.MaterialUsage);
                    ShowMaterialUsagesContent();
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvMaterialUsages.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть використання матеріалу для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvMaterialUsages.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var usage = allMaterialUsages.Find(u => u.Id == selectedId);
                var editForm = new MaterialUsageEditForm(usage);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateMaterialUsage(editForm.MaterialUsage);
                    ShowMaterialUsagesContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvMaterialUsages.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть використання матеріалу для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvMaterialUsages.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var usage = allMaterialUsages.Find(u => u.Id == selectedId);
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити використання матеріалу '{usage.MaterialType?.Name}'?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteMaterialUsage(usage.Id);
                    ShowMaterialUsagesContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(dgvMaterialUsages);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void ShowExpensesContent()
        {
            ClearContentPanel();
            
            var titleLabel = new Label
            {
                Text = "💸 Витрати",
                Font = new Font("Arial", 16F, FontStyle.Bold),
                ForeColor = FormStyleHelper.TextColor,
                AutoSize = true,
                Location = new Point(300, 80)
            };

            var dgvExpenses = FormStyleHelper.CreateStyledDataGridView();
            dgvExpenses.Location = new Point(300, 130);
            dgvExpenses.Size = new Size(contentPanel.Width - 350, contentPanel.Height - 225);
            dgvExpenses.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Тип витрати", DataPropertyName = "ExpenseType", Width = 150 });
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Сума (грн)", DataPropertyName = "Amount", Width = 120 });
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Дата", DataPropertyName = "Date", Width = 120 });
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Робота", DataPropertyName = "WorkDetails", Width = 250 });

            var allExpenses = dbService.GetAllExpenses();
            
            var displayData = allExpenses.ConvertAll(exp => new
            {
                exp.Id,
                exp.ExpenseType,
                exp.Amount,
                Date = exp.Date.ToShortDateString(),
                WorkDetails = $"{exp.Work?.WorkType?.Name ?? "Невідома робота"} на полі {exp.Work?.Field?.Name ?? "Невідоме поле"}"
            });
            
            dgvExpenses.DataSource = displayData;

            var buttonPanel = new Panel
            {
                Location = new Point(300, dgvExpenses.Bottom + 5),
                Size = new Size(400, 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            var btnAdd = FormStyleHelper.CreateStyledButton("➕ Додати", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnAdd.Location = new Point(0, 5);
            btnAdd.Click += (s, e) =>
            {
                var editForm = new ExpenseEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.AddExpense(editForm.Expense);
                    ShowExpensesContent(); 
                }
            };

            var btnEdit = FormStyleHelper.CreateStyledButton("✏️ Редагувати", FormStyleHelper.AccentColor, new Size(120, 40));
            btnEdit.Location = new Point(130, 5);
            btnEdit.Click += (s, e) =>
            {
                if (dgvExpenses.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть витрату для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvExpenses.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var expense = allExpenses.Find(ex => ex.Id == selectedId);
                var editForm = new ExpenseEditForm(expense);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    dbService.UpdateExpense(editForm.Expense);
                    ShowExpensesContent(); 
                }
            };

            var btnDelete = FormStyleHelper.CreateStyledButton("🗑️ Видалити", FormStyleHelper.DangerColor, new Size(120, 40));
            btnDelete.Location = new Point(260, 5);
            btnDelete.Click += (s, e) =>
            {
                if (dgvExpenses.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Оберіть витрату для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvExpenses.SelectedRows[0];
                dynamic rowData = selectedRow.DataBoundItem;
                var selectedId = (int)rowData.Id;
                var expense = allExpenses.Find(ex => ex.Id == selectedId);
                var result = MessageBox.Show($"Ви дійсно бажаєте видалити витрату '{expense.ExpenseType}' на суму {expense.Amount} грн?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dbService.DeleteExpense(expense.Id);
                    ShowExpensesContent(); 
                }
            };

            buttonPanel.Controls.Add(btnAdd);
            buttonPanel.Controls.Add(btnEdit);
            buttonPanel.Controls.Add(btnDelete);

            contentPanel.Controls.Add(titleLabel);
            contentPanel.Controls.Add(dgvExpenses);
            contentPanel.Controls.Add(buttonPanel);
        }

        private void BtnOpenCultures_Click(object sender, EventArgs e)
        {
            ShowCulturesContent();
        }

        private void BtnOpenFields_Click(object sender, EventArgs e)
        {
            ShowFieldsContent();
        }

        private void BtnOpenEmployees_Click(object sender, EventArgs e)
        {
            ShowEmployeesContent();
        }

        private void BtnOpenTechniques_Click(object sender, EventArgs e)
        {
            ShowTechniquesContent();
        }

        private void BtnOpenMaterialTypes_Click(object sender, EventArgs e)
        {
            ShowMaterialTypesContent();
        }

        private void BtnOpenSuppliers_Click(object sender, EventArgs e)
        {
            ShowSuppliersContent();
        }

        private void BtnOpenPlantings_Click(object sender, EventArgs e)
        {
            ShowPlantingsContent();
        }

        private void BtnOpenHarvests_Click(object sender, EventArgs e)
        {
            ShowHarvestsContent();
        }

        private void BtnOpenWorks_Click(object sender, EventArgs e)
        {
            ShowWorksContent();
        }

        private void BtnOpenPurchases_Click(object sender, EventArgs e)
        {
            ShowPurchasesContent();
        }

        private void BtnOpenMaterialUsages_Click(object sender, EventArgs e)
        {
            ShowMaterialUsagesContent();
        }

        private void BtnOpenExpenses_Click(object sender, EventArgs e)
        {
            ShowExpensesContent();
        }

        private void BtnOpenClients_Click(object sender, EventArgs e)
        {
            ShowClientsContent();
        }
    }
} 