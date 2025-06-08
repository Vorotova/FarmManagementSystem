using System;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;

namespace FarmManagementSystem
{
    public class ClientEditForm : Form
    {
        public Client Client { get; private set; }
        
        private TextBox txtCompanyName;
        private TextBox txtContactPerson;
        private TextBox txtPhone;
        private TextBox txtEmail;
        
        private Button btnSave;
        private Button btnCancel;
        
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public ClientEditForm(Client client = null)
        {
            isEditMode = client != null;
            Client = client != null ? new Client
            {
                Id = client.Id,
                CompanyName = client.CompanyName,
                ContactPerson = client.ContactPerson,
                Phone = client.Phone,
                Email = client.Email
            } : new Client();
            
            InitializeComponent();
            
            if (isEditMode) 
            {
                LoadClient();
            }
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування клієнта" : "Додавання клієнта";
            this.Size = new Size(530, 500);
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
                Text = isEditMode ? "✏️ Редагування клієнта" : "➕ Додавання клієнта",
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
                Text = "📋 ДАНІ КЛІЄНТА",
                Font = new Font("Arial", 12F, FontStyle.Bold),
                ForeColor = FormStyleHelper.PrimaryColor,
                Location = new Point(20, yPos),
                AutoSize = true
            };
            yPos += 35;

            var lblCompanyName = FormStyleHelper.CreateLabel("Назва компанії:");
            lblCompanyName.Location = new Point(20, yPos);
            txtCompanyName = FormStyleHelper.CreateStyledTextBox();
            txtCompanyName.Location = new Point(labelWidth + 20, yPos - 3);
            txtCompanyName.Size = new Size(controlWidth, 25);
            yPos += spacing;

            var lblContactPerson = FormStyleHelper.CreateLabel("Контактна особа:");
            lblContactPerson.Location = new Point(20, yPos);
            txtContactPerson = FormStyleHelper.CreateStyledTextBox();
            txtContactPerson.Location = new Point(labelWidth + 20, yPos - 3);
            txtContactPerson.Size = new Size(controlWidth, 25);
            yPos += spacing;

            var lblPhone = FormStyleHelper.CreateLabel("Телефон:");
            lblPhone.Location = new Point(20, yPos);
            txtPhone = FormStyleHelper.CreateStyledTextBox();
            txtPhone.Location = new Point(labelWidth + 20, yPos - 3);
            txtPhone.Size = new Size(controlWidth, 25);
            yPos += spacing;

            var lblEmail = FormStyleHelper.CreateLabel("Email:");
            lblEmail.Location = new Point(20, yPos);
            txtEmail = FormStyleHelper.CreateStyledTextBox();
            txtEmail.Location = new Point(labelWidth + 20, yPos - 3);
            txtEmail.Size = new Size(controlWidth, 25);
            yPos += spacing + 20;

            btnSave = FormStyleHelper.CreateStyledButton("💾 Зберегти", FormStyleHelper.SuccessColor, new Size(120, 40));
            btnSave.Location = new Point(150, yPos);
            btnSave.Click += BtnSave_Click;

            btnCancel = FormStyleHelper.CreateStyledButton("❌ Скасувати", FormStyleHelper.DangerColor, new Size(120, 40));
            btnCancel.Location = new Point(290, yPos);
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            contentPanel.Controls.AddRange(new Control[] {
                lblClientSection,
                lblCompanyName, txtCompanyName,
                lblContactPerson, txtContactPerson,
                lblPhone, txtPhone,
                lblEmail, txtEmail,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadClient()
        {
            txtCompanyName.Text = Client.CompanyName;
            txtContactPerson.Text = Client.ContactPerson;
            txtPhone.Text = Client.Phone;
            txtEmail.Text = Client.Email ?? "";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Введіть назву компанії!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("Введіть контактну особу!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Введіть телефон!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!int.TryParse(txtPhone.Text.Trim(), out _))
            {
                MessageBox.Show("Телефон має містити тільки цифри!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            Client.CompanyName = txtCompanyName.Text.Trim();
            Client.ContactPerson = txtContactPerson.Text.Trim();
            Client.Phone = txtPhone.Text.Trim();
            Client.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
            
            this.DialogResult = DialogResult.OK;
        }
    }
} 