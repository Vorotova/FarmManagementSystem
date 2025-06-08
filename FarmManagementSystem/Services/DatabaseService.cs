using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using FarmManagementSystem.Models;
using System.Windows.Forms;

namespace FarmManagementSystem.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string dbPath)
        {
            if (!System.IO.File.Exists(dbPath))
            {
                throw new System.IO.FileNotFoundException("База даних не знайдена", dbPath);
            }

            _connectionString = $"Data Source={dbPath};Version=3;";
        }

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public void TestConnection()
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Підключення до бази даних успішне");
                }
                catch (Exception ex)
                {
                    throw new Exception("Помилка підключення до бази даних", ex);
                }
            }
        }

        public List<Culture> GetAllCultures()
        {
            var cultures = new List<Culture>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Culture", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cultures.Add(new Culture
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Seasonality = reader.IsDBNull(2) ? null : reader.GetString(2),
                                AverageYield = reader.IsDBNull(3) ? 0 : reader.GetInt32(3)
                            });
                        }
                    }
                }
            }
            return cultures;
        }

        public void AddCulture(Culture culture)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Culture (name, seasonality, average_yield) VALUES (@name, @seasonality, @averageYield)",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", culture.Name);
                    command.Parameters.AddWithValue("@seasonality", (object)culture.Seasonality ?? DBNull.Value);
                    command.Parameters.AddWithValue("@averageYield", culture.AverageYield);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCulture(Culture culture)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Culture SET name = @name, seasonality = @seasonality, average_yield = @averageYield WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", culture.Name);
                    command.Parameters.AddWithValue("@seasonality", (object)culture.Seasonality ?? DBNull.Value);
                    command.Parameters.AddWithValue("@averageYield", culture.AverageYield);
                    command.Parameters.AddWithValue("@id", culture.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCulture(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "DELETE FROM Culture WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Field> GetAllFields()
        {
            var fields = new List<Field>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Field", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            fields.Add(new Field
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Area = reader.GetInt32(2),
                                SoilType = reader.IsDBNull(3) ? null : reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return fields;
        }

        public void AddField(Field field)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Field (name, area, soil_type) VALUES (@name, @area, @soilType)",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", field.Name);
                    command.Parameters.AddWithValue("@area", field.Area);
                    command.Parameters.AddWithValue("@soilType", (object)field.SoilType ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateField(Field field)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Field SET name = @name, area = @area, soil_type = @soilType WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", field.Name);
                    command.Parameters.AddWithValue("@area", field.Area);
                    command.Parameters.AddWithValue("@soilType", (object)field.SoilType ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", field.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteField(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "DELETE FROM Field WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Employee", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Phone = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Position = reader.IsDBNull(3) ? null : reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Employee (full_name, phone, position) VALUES (@fullName, @phone, @position)",
                    connection))
                {
                    command.Parameters.AddWithValue("@fullName", employee.FullName);
                    command.Parameters.AddWithValue("@phone", (object)employee.Phone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@position", (object)employee.Position ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Employee SET full_name = @fullName, phone = @phone, position = @position WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@fullName", employee.FullName);
                    command.Parameters.AddWithValue("@phone", (object)employee.Phone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@position", (object)employee.Position ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", employee.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "DELETE FROM Employee WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable ExecuteQuery(string query)
        {
            var dataTable = new DataTable();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        public int ExecuteNonQuery(string commandText)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(commandText, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        public void CreateTables()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Technique (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            name TEXT NOT NULL,
                            type TEXT NOT NULL,
                            usage_cost REAL NOT NULL,
                            condition TEXT NOT NULL
                        )";
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Client> GetAllClients()
        {
            var clients = new List<Client>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Client", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(new Client
                            {
                                Id = reader.GetInt32(0),
                                CompanyName = reader.GetString(1),
                                ContactPerson = reader.GetString(2),
                                Phone = reader.GetInt32(3).ToString(),
                                Email = reader.IsDBNull(4) ? null : reader.GetString(4)
                            });
                        }
                    }
                }
                
                foreach (var client in clients)
                {
                    using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Sale WHERE client_id = @clientId", connection))
                    {
                        command.Parameters.AddWithValue("@clientId", client.Id);
                        var count = command.ExecuteScalar();
                    }
                }
            }
            return clients;
        }

        public void AddClient(Client client)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Client (company_name, contact_person, phone, email) VALUES (@companyName, @contactPerson, @phone, @email)",
                    connection))
                {
                    command.Parameters.AddWithValue("@companyName", client.CompanyName);
                    command.Parameters.AddWithValue("@contactPerson", client.ContactPerson);
                    
                    if (int.TryParse(client.Phone, out int phoneNumber))
                    {
                        command.Parameters.AddWithValue("@phone", phoneNumber);
                    }
                    else
                    {
                        throw new ArgumentException("Телефон має містити тільки цифри");
                    }
                    
                    command.Parameters.AddWithValue("@email", client.Email ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateClient(Client client)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Client SET company_name = @companyName, contact_person = @contactPerson, phone = @phone, email = @email WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@companyName", client.CompanyName);
                    command.Parameters.AddWithValue("@contactPerson", client.ContactPerson);
                    
                    if (int.TryParse(client.Phone, out int phoneNumber))
                    {
                        command.Parameters.AddWithValue("@phone", phoneNumber);
                    }
                    else
                    {
                        throw new ArgumentException("Телефон має містити тільки цифри");
                    }
                    
                    command.Parameters.AddWithValue("@email", client.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@id", client.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteClient(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Sale WHERE client_id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                using (var command = new SQLiteCommand("DELETE FROM Client WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int GetClientContractsCount(int clientId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Sale WHERE client_id = @clientId", connection))
                {
                    command.Parameters.AddWithValue("@clientId", clientId);
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public List<Harvest> GetAvailableHarvests()
        {
            var harvests = new List<Harvest>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT h.id, h.harvest_date, h.field_id, h.culture_id, h.volume, h.price_per_kg, 
                                                        f.name as field_name, c.name as culture_name, COALESCE(SUM(s.quantity), 0) as sold_quantity
                                                        FROM Harvest h 
                                                        JOIN Field f ON h.field_id = f.id 
                                                        JOIN Culture c ON h.culture_id = c.id
                                                        LEFT JOIN Sale s ON h.id = s.harvest_id
                                                        GROUP BY h.id, h.harvest_date, h.field_id, h.culture_id, h.volume, h.price_per_kg, f.name, c.name", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var harvest = new Harvest
                            {
                                Id = Convert.ToInt32(reader[0]),
                                FieldId = Convert.ToInt32(reader[2]),
                                CultureId = Convert.ToInt32(reader[3]),
                                HarvestDate = DateTime.Parse(reader[1].ToString()),
                                Volume = Convert.ToInt32(reader[4]),
                                PricePerKg = Convert.ToInt32(reader[5]),
                                Field = new Field { Id = Convert.ToInt32(reader[2]), Name = reader[6].ToString() },
                                Culture = new Culture { Id = Convert.ToInt32(reader[3]), Name = reader[7].ToString() }
                            };
                            
                            int soldQuantity = Convert.ToInt32(reader[8]);
                            harvest.AvailableQuantity = harvest.Volume - soldQuantity;
                            
                            harvests.Add(harvest);
                        }
                    }
                }
            }
            return harvests;
        }

        public int GetAvailableQuantity(int harvestId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT h.volume - COALESCE(SUM(s.quantity), 0) as available
                                                        FROM Harvest h
                                                        LEFT JOIN Sale s ON h.id = s.harvest_id
                                                        WHERE h.id = @harvestId
                                                        GROUP BY h.id, h.volume", connection))
                {
                    command.Parameters.AddWithValue("@harvestId", harvestId);
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        // Techniques
        public List<Technique> GetAllTechniques()
        {
            var techniques = new List<Technique>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Technique", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            techniques.Add(new Technique
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Type = reader.GetString(2),
                                UsageCost = reader.GetInt32(3),
                                Condition = reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return techniques;
        }

        public void AddTechnique(Technique technique)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Technique (name, type, usage_cost, condition) VALUES (@name, @type, @usageCost, @condition)",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", technique.Name);
                    command.Parameters.AddWithValue("@type", technique.Type);
                    command.Parameters.AddWithValue("@usageCost", technique.UsageCost);
                    command.Parameters.AddWithValue("@condition", technique.Condition);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTechnique(Technique technique)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Technique SET name = @name, type = @type, usage_cost = @usageCost, condition = @condition WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", technique.Name);
                    command.Parameters.AddWithValue("@type", technique.Type);
                    command.Parameters.AddWithValue("@usageCost", technique.UsageCost);
                    command.Parameters.AddWithValue("@condition", technique.Condition);
                    command.Parameters.AddWithValue("@id", technique.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTechnique(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Technique WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // MaterialTypes
        public List<MaterialType> GetAllMaterialTypes()
        {
            var materialTypes = new List<MaterialType>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM MaterialType", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materialTypes.Add(new MaterialType
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Type = reader.GetString(2),
                                Unit = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            return materialTypes;
        }

        public void AddMaterialType(MaterialType materialType)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO MaterialType (name, type, unit) VALUES (@name, @type, @unit)",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", materialType.Name);
                    command.Parameters.AddWithValue("@type", materialType.Type);
                    command.Parameters.AddWithValue("@unit", materialType.Unit);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMaterialType(MaterialType materialType)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE MaterialType SET name = @name, type = @type, unit = @unit WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", materialType.Name);
                    command.Parameters.AddWithValue("@type", materialType.Type);
                    command.Parameters.AddWithValue("@unit", materialType.Unit);
                    command.Parameters.AddWithValue("@id", materialType.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMaterialType(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM MaterialType WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Suppliers
        public List<Supplier> GetAllSuppliers()
        {
            var suppliers = new List<Supplier>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Supplier", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new Supplier
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                ContactPerson = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                ProductType = reader.IsDBNull(4) ? null : reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return suppliers;
        }

        public void AddSupplier(Supplier supplier)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Supplier (name, contact_person, phone, product_type) VALUES (@name, @contactPerson, @phone, @productType)",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", supplier.Name);
                    command.Parameters.AddWithValue("@contactPerson", (object)supplier.ContactPerson ?? DBNull.Value);
                    command.Parameters.AddWithValue("@phone", (object)supplier.Phone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@productType", (object)supplier.ProductType ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Supplier SET name = @name, contact_person = @contactPerson, phone = @phone, product_type = @productType WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@name", supplier.Name);
                    command.Parameters.AddWithValue("@contactPerson", (object)supplier.ContactPerson ?? DBNull.Value);
                    command.Parameters.AddWithValue("@phone", (object)supplier.Phone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@productType", (object)supplier.ProductType ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", supplier.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSupplier(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Supplier WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Plantings
        public List<Planting> GetAllPlantings()
        {
            var plantings = new List<Planting>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT p.*, f.name as field_name, c.name as culture_name 
                                                        FROM Planting p 
                                                        JOIN Field f ON p.field_id = f.id 
                                                        JOIN Culture c ON p.culture_id = c.id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            plantings.Add(new Planting
                            {
                                Id = reader.GetInt32(0),
                                FieldId = reader.GetInt32(1),
                                CultureId = reader.GetInt32(2),
                                SowingDate = DateTime.Parse(reader.GetString(3)),
                                Field = new Field { Id = reader.GetInt32(1), Name = reader.GetString(4) },
                                Culture = new Culture { Id = reader.GetInt32(2), Name = reader.GetString(5) }
                            });
                        }
                    }
                }
            }
            return plantings;
        }

        public void AddPlanting(Planting planting)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Planting (field_id, culture_id, sowing_date) VALUES (@fieldId, @cultureId, @sowingDate)",
                    connection))
                {
                    command.Parameters.AddWithValue("@fieldId", planting.FieldId);
                    command.Parameters.AddWithValue("@cultureId", planting.CultureId);
                    command.Parameters.AddWithValue("@sowingDate", planting.SowingDate.ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePlanting(Planting planting)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Planting SET field_id = @fieldId, culture_id = @cultureId, sowing_date = @sowingDate WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@fieldId", planting.FieldId);
                    command.Parameters.AddWithValue("@cultureId", planting.CultureId);
                    command.Parameters.AddWithValue("@sowingDate", planting.SowingDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@id", planting.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePlanting(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Planting WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Harvests
        public List<Harvest> GetAllHarvests()
        {
            var harvests = new List<Harvest>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT h.id, h.harvest_date, h.field_id, h.culture_id, h.volume, h.price_per_kg, f.name as field_name, c.name as culture_name 
                                                        FROM Harvest h 
                                                        JOIN Field f ON h.field_id = f.id 
                                                        JOIN Culture c ON h.culture_id = c.id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            harvests.Add(new Harvest
                            {
                                Id = Convert.ToInt32(reader[0]),
                                FieldId = Convert.ToInt32(reader[2]),
                                CultureId = Convert.ToInt32(reader[3]),
                                HarvestDate = DateTime.Parse(reader[1].ToString()),
                                Volume = Convert.ToInt32(reader[4]),
                                PricePerKg = Convert.ToInt32(reader[5]),
                                Field = new Field { Id = Convert.ToInt32(reader[2]), Name = reader[6].ToString() },
                                Culture = new Culture { Id = Convert.ToInt32(reader[3]), Name = reader[7].ToString() }
                            });
                        }
                    }
                }
            }
            return harvests;
        }

        public void AddHarvest(Harvest harvest)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Harvest (field_id, culture_id, harvest_date, volume, price_per_kg) VALUES (@fieldId, @cultureId, @harvestDate, @volume, @pricePerKg)",
                    connection))
                {
                    command.Parameters.AddWithValue("@fieldId", harvest.FieldId);
                    command.Parameters.AddWithValue("@cultureId", harvest.CultureId);
                    command.Parameters.AddWithValue("@harvestDate", harvest.HarvestDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@volume", harvest.Volume);
                    command.Parameters.AddWithValue("@pricePerKg", harvest.PricePerKg);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateHarvest(Harvest harvest)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Harvest SET field_id = @fieldId, culture_id = @cultureId, harvest_date = @harvestDate, volume = @volume, price_per_kg = @pricePerKg WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@fieldId", harvest.FieldId);
                    command.Parameters.AddWithValue("@cultureId", harvest.CultureId);
                    command.Parameters.AddWithValue("@harvestDate", harvest.HarvestDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@volume", harvest.Volume);
                    command.Parameters.AddWithValue("@pricePerKg", harvest.PricePerKg);
                    command.Parameters.AddWithValue("@id", harvest.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteHarvest(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Harvest WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Works
        public List<Work> GetAllWorks()
        {
            var works = new List<Work>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT w.id, w.duration, w.date, w.work_type_id, w.field_id, w.technique_id, w.employee_id, wt.name as work_type_name, f.name as field_name, e.full_name as employee_name, t.name as technique_name 
                                                        FROM Work w 
                                                        JOIN WorkType wt ON w.work_type_id = wt.id 
                                                        JOIN Field f ON w.field_id = f.id 
                                                        LEFT JOIN Employee e ON w.employee_id = e.id 
                                                        LEFT JOIN Technique t ON w.technique_id = t.id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            works.Add(new Work
                            {
                                Id = Convert.ToInt32(reader[0]),
                                WorkTypeId = Convert.ToInt32(reader[3]),
                                FieldId = Convert.ToInt32(reader[4]),
                                EmployeeId = reader.IsDBNull(6) ? (int?)null : Convert.ToInt32(reader[6]),
                                TechniqueId = reader.IsDBNull(5) ? (int?)null : Convert.ToInt32(reader[5]),
                                Date = DateTime.Parse(reader[2].ToString()),
                                Duration = Convert.ToInt32(reader[1]),
                                WorkType = new WorkType { Id = Convert.ToInt32(reader[3]), Name = reader[7].ToString() },
                                Field = new Field { Id = Convert.ToInt32(reader[4]), Name = reader[8].ToString() },
                                Employee = reader.IsDBNull(6) ? null : new Employee { Id = Convert.ToInt32(reader[6]), FullName = reader.IsDBNull(9) ? "" : reader[9].ToString() },
                                Technique = reader.IsDBNull(5) ? null : new Technique { Id = Convert.ToInt32(reader[5]), Name = reader.IsDBNull(10) ? "" : reader[10].ToString() }
                            });
                        }
                    }
                }
            }
            return works;
        }

        public List<WorkType> GetAllWorkTypes()
        {
            var workTypes = new List<WorkType>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM WorkType", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            workTypes.Add(new WorkType
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return workTypes;
        }

        public void AddWork(Work work)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Work (work_type_id, field_id, employee_id, technique_id, date, duration) VALUES (@workTypeId, @fieldId, @employeeId, @techniqueId, @date, @duration)",
                    connection))
                {
                    command.Parameters.AddWithValue("@workTypeId", work.WorkTypeId);
                    command.Parameters.AddWithValue("@fieldId", work.FieldId);
                    command.Parameters.AddWithValue("@employeeId", work.EmployeeId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@techniqueId", work.TechniqueId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@date", work.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@duration", work.Duration);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateWork(Work work)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Work SET work_type_id = @workTypeId, field_id = @fieldId, employee_id = @employeeId, technique_id = @techniqueId, date = @date, duration = @duration WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@workTypeId", work.WorkTypeId);
                    command.Parameters.AddWithValue("@fieldId", work.FieldId);
                    command.Parameters.AddWithValue("@employeeId", work.EmployeeId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@techniqueId", work.TechniqueId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@date", work.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@duration", work.Duration);
                    command.Parameters.AddWithValue("@id", work.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteWork(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Work WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Purchases
        public List<Purchase> GetAllPurchases()
        {
            var purchases = new List<Purchase>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT p.*, mt.name as material_name, s.name as supplier_name 
                                                        FROM Purchase p 
                                                        JOIN MaterialType mt ON p.material_id = mt.id 
                                                        JOIN Supplier s ON p.supplier_id = s.id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            purchases.Add(new Purchase
                            {
                                Id = Convert.ToInt32(reader[0]),
                                MaterialId = Convert.ToInt32(reader[1]),
                                SupplierId = Convert.ToInt32(reader[2]),
                                Date = DateTime.Parse(reader[3].ToString()),
                                Quantity = Convert.ToInt32(reader[4]),
                                UnitPrice = Convert.ToInt32(reader[5]),
                                ContractDate = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader[6].ToString()),
                                DeliveryDate = reader.IsDBNull(7) ? (DateTime?)null : DateTime.Parse(reader[7].ToString()),
                                Notes = reader.IsDBNull(8) ? null : reader[8].ToString(),
                                Status = reader.IsDBNull(9) ? null : reader[9].ToString(),
                                Material = new MaterialType { Id = Convert.ToInt32(reader[1]), Name = reader[10].ToString() },
                                Supplier = new Supplier { Id = Convert.ToInt32(reader[2]), Name = reader[11].ToString() }
                            });
                        }
                    }
                }
            }
            return purchases;
        }

        public void AddPurchase(Purchase purchase)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Purchase (material_id, supplier_id, date, quantity, unit_price, ContractDate, DeliveryDate, Status, Notes) VALUES (@materialId, @supplierId, @date, @quantity, @unitPrice, @contractDate, @deliveryDate, @status, @notes)",
                    connection))
                {
                    command.Parameters.AddWithValue("@materialId", purchase.MaterialId);
                    command.Parameters.AddWithValue("@supplierId", purchase.SupplierId);
                    command.Parameters.AddWithValue("@date", purchase.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@quantity", purchase.Quantity);
                    command.Parameters.AddWithValue("@unitPrice", purchase.UnitPrice);
                    command.Parameters.AddWithValue("@contractDate", purchase.ContractDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@deliveryDate", purchase.DeliveryDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@status", (object)purchase.Status ?? DBNull.Value);
                    command.Parameters.AddWithValue("@notes", (object)purchase.Notes ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePurchase(Purchase purchase)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Purchase SET material_id = @materialId, supplier_id = @supplierId, date = @date, quantity = @quantity, unit_price = @unitPrice, ContractDate = @contractDate, DeliveryDate = @deliveryDate, Status = @status, Notes = @notes WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@materialId", purchase.MaterialId);
                    command.Parameters.AddWithValue("@supplierId", purchase.SupplierId);
                    command.Parameters.AddWithValue("@date", purchase.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@quantity", purchase.Quantity);
                    command.Parameters.AddWithValue("@unitPrice", purchase.UnitPrice);
                    command.Parameters.AddWithValue("@contractDate", purchase.ContractDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@deliveryDate", purchase.DeliveryDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@status", (object)purchase.Status ?? DBNull.Value);
                    command.Parameters.AddWithValue("@notes", (object)purchase.Notes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", purchase.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePurchase(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Purchase WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // MaterialUsages
        public List<MaterialUsage> GetAllMaterialUsages()
        {
            var materialUsages = new List<MaterialUsage>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT mu.id, mu.material_type_id, mu.quantity, mu.work_id, mt.name as material_name, mt.unit as material_unit,
                                                        w.id as work_id, wt.name as work_type_name, f.name as field_name
                                                        FROM MaterialUsage mu 
                                                        JOIN MaterialType mt ON mu.material_type_id = mt.id 
                                                        JOIN Work w ON mu.work_id = w.id
                                                        JOIN WorkType wt ON w.work_type_id = wt.id
                                                        JOIN Field f ON w.field_id = f.id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materialUsages.Add(new MaterialUsage
                            {
                                Id = Convert.ToInt32(reader[0]),
                                MaterialTypeId = Convert.ToInt32(reader[1]),
                                Quantity = Convert.ToInt32(reader[2]),
                                WorkId = Convert.ToInt32(reader[3]),
                                MaterialType = new MaterialType { Id = Convert.ToInt32(reader[1]), Name = reader[4].ToString(), Unit = reader[5].ToString() },
                                Work = new Work 
                                { 
                                    Id = Convert.ToInt32(reader[6]),
                                    WorkType = new WorkType { Name = reader[7].ToString() },
                                    Field = new Field { Name = reader[8].ToString() }
                                }
                            });
                        }
                    }
                }
            }
            return materialUsages;
        }

        public void AddMaterialUsage(MaterialUsage materialUsage)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO MaterialUsage (material_type_id, quantity, work_id) VALUES (@materialTypeId, @quantity, @workId)",
                    connection))
                {
                    command.Parameters.AddWithValue("@materialTypeId", materialUsage.MaterialTypeId);
                    command.Parameters.AddWithValue("@quantity", materialUsage.Quantity);
                    command.Parameters.AddWithValue("@workId", materialUsage.WorkId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMaterialUsage(MaterialUsage materialUsage)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE MaterialUsage SET material_type_id = @materialTypeId, quantity = @quantity, work_id = @workId WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@materialTypeId", materialUsage.MaterialTypeId);
                    command.Parameters.AddWithValue("@quantity", materialUsage.Quantity);
                    command.Parameters.AddWithValue("@workId", materialUsage.WorkId);
                    command.Parameters.AddWithValue("@id", materialUsage.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMaterialUsage(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM MaterialUsage WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Expenses
        public List<Expense> GetAllExpenses()
        {
            var expenses = new List<Expense>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT e.id, e.amount, e.date, e.expense_type, e.work_id, 
                                                        w.id as work_id_join, wt.name as work_type_name, f.name as field_name
                                                        FROM Expense e 
                                                        LEFT JOIN Work w ON e.work_id = w.id
                                                        LEFT JOIN WorkType wt ON w.work_type_id = wt.id
                                                        LEFT JOIN Field f ON w.field_id = f.id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expenses.Add(new Expense
                            {
                                Id = Convert.ToInt32(reader[0]),
                                Amount = Convert.ToInt32(reader[1]),
                                Date = DateTime.Parse(reader[2].ToString()),
                                ExpenseType = reader[3].ToString(),
                                WorkId = Convert.ToInt32(reader[4]),
                                Work = reader.IsDBNull(5) ? null : new Work 
                                { 
                                    Id = Convert.ToInt32(reader[5]),
                                    WorkType = reader.IsDBNull(6) ? null : new WorkType { Name = reader[6].ToString() },
                                    Field = reader.IsDBNull(7) ? null : new Field { Name = reader[7].ToString() }
                                }
                            });
                        }
                    }
                }
            }
            return expenses;
        }

        public void AddExpense(Expense expense)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Expense (expense_type, amount, date, work_id) VALUES (@expenseType, @amount, @date, @workId)",
                    connection))
                {
                    command.Parameters.AddWithValue("@expenseType", expense.ExpenseType);
                    command.Parameters.AddWithValue("@amount", expense.Amount);
                    command.Parameters.AddWithValue("@date", expense.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@workId", expense.WorkId == 0 ? (object)DBNull.Value : expense.WorkId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateExpense(Expense expense)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Expense SET expense_type = @expenseType, amount = @amount, date = @date, work_id = @workId WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@expenseType", expense.ExpenseType);
                    command.Parameters.AddWithValue("@amount", expense.Amount);
                    command.Parameters.AddWithValue("@date", expense.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@workId", expense.WorkId == 0 ? (object)DBNull.Value : expense.WorkId);
                    command.Parameters.AddWithValue("@id", expense.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteExpense(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Expense WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Sales
        public List<Sale> GetAllSales()
        {
            var sales = new List<Sale>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT s.*, c.company_name, h.id as harvest_id 
                                                        FROM Sale s 
                                                        JOIN Client c ON s.client_id = c.id 
                                                        JOIN Harvest h ON s.harvest_id = h.id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sales.Add(new Sale
                            {
                                Id = reader.GetInt32(0),
                                ClientId = reader.GetInt32(1),
                                HarvestId = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                UnitPrice = reader.GetInt32(4),
                                ContractDate = reader.IsDBNull(5) ? (DateTime?)null : DateTime.Parse(reader.GetString(5)),
                                DeliveryDate = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Status = reader.IsDBNull(7) ? null : reader.GetString(7),
                                Notes = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CreatedDate = DateTime.Parse(reader.GetString(9)),
                                Client = new Client { Id = reader.GetInt32(1), CompanyName = reader.GetString(10) },
                                Harvest = new Harvest { Id = reader.GetInt32(11) }
                            });
                        }
                    }
                }
            }
            return sales;
        }

        public List<Sale> GetSalesByClientId(int clientId)
        {
            var sales = new List<Sale>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(@"SELECT s.*, c.company_name, h.id as harvest_id, cu.name as culture_name, f.name as field_name
                                                        FROM Sale s 
                                                        JOIN Client c ON s.client_id = c.id 
                                                        JOIN Harvest h ON s.harvest_id = h.id
                                                        JOIN Culture cu ON h.culture_id = cu.id
                                                        JOIN Field f ON h.field_id = f.id
                                                        WHERE s.client_id = @clientId", connection))
                {
                    command.Parameters.AddWithValue("@clientId", clientId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sales.Add(new Sale
                            {
                                Id = reader.GetInt32(0),
                                ClientId = reader.GetInt32(1),
                                HarvestId = reader.GetInt32(2),
                                Quantity = reader.GetInt32(3),
                                UnitPrice = reader.GetInt32(4),
                                ContractDate = reader.IsDBNull(5) ? (DateTime?)null : DateTime.Parse(reader.GetString(5)),
                                DeliveryDate = reader.IsDBNull(6) ? (DateTime?)null : DateTime.Parse(reader.GetString(6)),
                                Status = reader.IsDBNull(7) ? null : reader.GetString(7),
                                Notes = reader.IsDBNull(8) ? null : reader.GetString(8),
                                CreatedDate = DateTime.Parse(reader.GetString(9)),
                                Client = new Client { Id = reader.GetInt32(1), CompanyName = reader.GetString(10) },
                                Harvest = new Harvest 
                                { 
                                    Id = reader.GetInt32(11),
                                    Culture = new Culture { Name = reader.GetString(12) },
                                    Field = new Field { Name = reader.GetString(13) }
                                }
                            });
                        }
                    }
                }
            }
            return sales;
        }

        public void AddSale(Sale sale)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO Sale (client_id, harvest_id, quantity, unit_price, contract_date, delivery_date, status, notes, created_date) VALUES (@clientId, @harvestId, @quantity, @unitPrice, @contractDate, @deliveryDate, @status, @notes, @createdDate)",
                    connection))
                {
                    command.Parameters.AddWithValue("@clientId", sale.ClientId);
                    command.Parameters.AddWithValue("@harvestId", sale.HarvestId);
                    command.Parameters.AddWithValue("@quantity", sale.Quantity);
                    command.Parameters.AddWithValue("@unitPrice", sale.UnitPrice);
                    command.Parameters.AddWithValue("@contractDate", sale.ContractDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@deliveryDate", sale.DeliveryDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@status", (object)sale.Status ?? DBNull.Value);
                    command.Parameters.AddWithValue("@notes", (object)sale.Notes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@createdDate", sale.CreatedDate.ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateSale(Sale sale)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE Sale SET client_id = @clientId, harvest_id = @harvestId, quantity = @quantity, unit_price = @unitPrice, contract_date = @contractDate, delivery_date = @deliveryDate, status = @status, notes = @notes WHERE id = @id",
                    connection))
                {
                    command.Parameters.AddWithValue("@clientId", sale.ClientId);
                    command.Parameters.AddWithValue("@harvestId", sale.HarvestId);
                    command.Parameters.AddWithValue("@quantity", sale.Quantity);
                    command.Parameters.AddWithValue("@unitPrice", sale.UnitPrice);
                    command.Parameters.AddWithValue("@contractDate", sale.ContractDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@deliveryDate", sale.DeliveryDate?.ToString("yyyy-MM-dd") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@status", (object)sale.Status ?? DBNull.Value);
                    command.Parameters.AddWithValue("@notes", (object)sale.Notes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@id", sale.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSale(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand("DELETE FROM Sale WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
} 