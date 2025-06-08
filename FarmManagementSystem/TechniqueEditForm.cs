using System;
using System.Drawing;
using System.Windows.Forms;
using FarmManagementSystem.Models;

namespace FarmManagementSystem
{
    public class TechniqueEditForm : Form
    {
        public Technique Technique { get; private set; }
        private TextBox txtName;
        private ComboBox cmbType;
        private NumericUpDown numUsageCost;
        private ComboBox cmbCondition;
        private Button btnSave;
        private Button btnCancel;
        private bool isEditMode;
        private Panel headerPanel;
        private Label lblTitle;
        private Panel contentPanel;

        public TechniqueEditForm(Technique technique = null)
        {
            isEditMode = technique != null;
            Technique = technique != null ? new Technique
            {
                Id = technique.Id,
                Name = technique.Name,
                Type = technique.Type,
                UsageCost = technique.UsageCost,
                Condition = technique.Condition
            } : new Technique();
            InitializeComponent();
            if (isEditMode) LoadTechnique();
        }

        private void InitializeComponent()
        {
            this.Text = isEditMode ? "Редагування техніки" : "Додавання техніки";
            this.Size = new Size(550, 500);
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
                Text = isEditMode ? "🚜 Редагування техніки" : "➕ Додавання техніки",
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

            // Назва техніки
            var lblName = FormStyleHelper.CreateLabel("Назва техніки:");
            lblName.Location = new Point(30, yPos);
            txtName = FormStyleHelper.CreateStyledTextBox();
            txtName.Location = new Point(labelWidth + 50, yPos - 3);
            txtName.Size = new Size(controlWidth, 25);
            txtName.Font = FormStyleHelper.RegularFont;

            yPos += spacing;

            // Тип техніки
            var lblType = FormStyleHelper.CreateLabel("Тип техніки:");
            lblType.Location = new Point(30, yPos);
            cmbType = FormStyleHelper.CreateStyledComboBox();
            cmbType.Location = new Point(labelWidth + 50, yPos - 3);
            cmbType.Size = new Size(controlWidth, 25);
            cmbType.Items.AddRange(new object[] {
                "Трактори та міні-трактори",
                "Ґрунтообробні механізми",
                "Посівне обладнання",
                "Механізми для догляду за посівами",
                "Збиральна техніка",
                "Додаткове обладнання"
            });

            yPos += spacing;

            // Вартість використання
            var lblUsageCost = FormStyleHelper.CreateLabel("Вартість використання\n(грн/год):");
            lblUsageCost.Location = new Point(30, yPos);
            numUsageCost = new NumericUpDown 
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

            yPos += spacing;

            // Стан техніки
            var lblCondition = FormStyleHelper.CreateLabel("Стан техніки:");
            lblCondition.Location = new Point(30, yPos);
            cmbCondition = FormStyleHelper.CreateStyledComboBox();
            cmbCondition.Location = new Point(labelWidth + 50, yPos - 3);
            cmbCondition.Size = new Size(controlWidth, 25);
            cmbCondition.Items.AddRange(new object[] { "Новий", "Б/У", "На ремонті" });

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
                lblName, txtName,
                lblType, cmbType,
                lblUsageCost, numUsageCost,
                lblCondition, cmbCondition,
                btnSave, btnCancel
            });

            this.Controls.Add(contentPanel);
        }

        private void LoadTechnique()
        {
            txtName.Text = Technique.Name;
            cmbType.SelectedItem = Technique.Type;
            numUsageCost.Value = (decimal)Technique.UsageCost;
            cmbCondition.SelectedItem = Technique.Condition;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введіть назву техніки!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbType.SelectedItem == null)
            {
                MessageBox.Show("Оберіть тип техніки!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (numUsageCost.Value < 0)
            {
                MessageBox.Show("Вартість використання не може бути від'ємною!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cmbCondition.SelectedItem == null)
            {
                MessageBox.Show("Оберіть стан техніки!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            Technique.Name = txtName.Text.Trim();
            Technique.Type = cmbType.SelectedItem.ToString();
            Technique.UsageCost = (double)numUsageCost.Value;
            Technique.Condition = cmbCondition.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
} 