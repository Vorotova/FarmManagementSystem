# Інструкції з інтеграції сутностей в MainForm

## Що вже зроблено ✅

1. **Культури** - повністю інтегровано з фільтрами
2. **Поля** - повністю інтегровано  
3. **Працівники** - повністю інтегровано з контролем доступу
4. **Закупки та Контракти** - повністю інтегровано

## Що потрібно зробити 📋

Для кожної з наступних сутностей потрібно:

### 1. Техніка (🚜)
```csharp
private void ShowTechniquesContent()
{
    // Колонки: ID, Name, Type, Status, PurchaseDate, MaintenanceDate
    // Методи: GetAllTechniques, AddTechnique, UpdateTechnique, DeleteTechnique
    // EditForm: TechniqueEditForm
}
```

### 2. Типи матеріалів (📦)
```csharp
private void ShowMaterialTypesContent()
{
    // Колонки: ID, Name, Unit, Description
    // Методи: GetAllMaterialTypes, AddMaterialType, UpdateMaterialType, DeleteMaterialType
    // EditForm: MaterialTypeEditForm
}
```

### 3. Постачальники (🏪)
```csharp
private void ShowSuppliersContent()
{
    // Колонки: ID, Name, ContactInfo, Address
    // Методи: GetAllSuppliers, AddSupplier, UpdateSupplier, DeleteSupplier
    // EditForm: SupplierEditForm
}
```

### 4. Посадки (🌿)
```csharp
private void ShowPlantingsContent()
{
    // Колонки: ID, FieldName, CultureName, PlantingDate, Area, ExpectedYield
    // Методи: GetAllPlantings, AddPlanting, UpdatePlanting, DeletePlanting
    // EditForm: PlantingEditForm
}
```

### 5. Урожай (🌾)
```csharp
private void ShowHarvestsContent()
{
    // Колонки: ID, FieldName, CultureName, HarvestDate, ActualYield, Quality
    // Методи: GetAllHarvests, AddHarvest, UpdateHarvest, DeleteHarvest
    // EditForm: HarvestEditForm
}
```

### 6. Роботи (⚒️)
```csharp
private void ShowWorksContent()
{
    // Колонки: ID, WorkTypeName, FieldName, EmployeeName, Date, Duration, Description
    // Методи: GetAllWorks, AddWork, UpdateWork, DeleteWork
    // EditForm: WorkEditForm
}
```

### 7. Використання матеріалу (📊)
```csharp
private void ShowMaterialUsagesContent()
{
    // Колонки: ID, MaterialTypeName, FieldName, Date, Quantity, Purpose
    // Методи: GetAllMaterialUsages, AddMaterialUsage, UpdateMaterialUsage, DeleteMaterialUsage
    // EditForm: MaterialUsageEditForm
}
```

### 8. Витрати (💸)
```csharp
private void ShowExpensesContent()
{
    // Колонки: ID, Category, Description, Amount, Date
    // Методи: GetAllExpenses, AddExpense, UpdateExpense, DeleteExpense
    // EditForm: ExpenseEditForm
}
```

## Шаблон для створення методу

```csharp
private void ShowXXXContent()
{
    ClearContentPanel();
    
    // Створюємо заголовок
    var titleLabel = new Label
    {
        Text = "🔸 Назва сутності",
        Font = new Font("Arial", 16F, FontStyle.Bold),
        ForeColor = FormStyleHelper.TextColor,
        AutoSize = true,
        Location = new Point(20, 20)
    };

    // Створюємо DataGridView
    var dgvXXX = FormStyleHelper.CreateStyledDataGridView();
    dgvXXX.Location = new Point(20, 60);
    dgvXXX.Size = new Size(contentPanel.Width - 60, contentPanel.Height - 160);
    dgvXXX.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

    // Додаємо колонки
    dgvXXX.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
    // ... додати інші колонки

    // Завантажуємо дані
    var allXXX = dbService.GetAllXXX();
    dgvXXX.DataSource = allXXX;

    // Створюємо кнопки (копіювати з існуючих методів)
    // ...

    // Додаємо всі елементи до панелі
    contentPanel.Controls.Add(titleLabel);
    contentPanel.Controls.Add(dgvXXX);
    contentPanel.Controls.Add(btnAdd);
    contentPanel.Controls.Add(btnEdit);
    contentPanel.Controls.Add(btnDelete);
}
```

## Оновлення обробників

Для кожного методу потрібно оновити відповідний обробник:

```csharp
private void BtnOpenXXX_Click(object sender, EventArgs e)
{
    ShowXXXContent();
}
```

## Контроль доступу за ролями

- **Менеджер**: має доступ до всіх операцій з Культурами, Полями, Працівниками, Типами матеріалів, Постачальниками, Закупками, Витратами
- **Агроном**: має доступ тільки до перегляду та редагування Культур, Полів, Техніки, Типів матеріалів, Посадок, Урожаю, Робіт, Використання матеріалу

## Переваги нового підходу

✅ **Все в одному вікні** - користувач не втрачає контекст  
✅ **Швидке перемикання** - один клік для зміни розділу  
✅ **Сучасний дизайн** - консистентний інтерфейс  
✅ **Зручність** - редагування в модальних вікнах  
✅ **Продуктивність** - менше вікон = менше ресурсів  

## Наступні кроки

1. Створіть методи для решти сутностей за шаблоном
2. Оновіть обробники кнопок
3. Протестуйте кожну сутність
4. Перевірте контроль доступу за ролями
5. Видаліть старі окремі форми (опціонально)

Після завершення всіх інтеграцій у вас буде сучасна односторінкова програма з чудовим UX! 