using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.Tool
{
    public class MigrationAction
    {
        public string Entity { get; set; }
        public string ActionType { get; set; }
        public string PropertyName { get; set; }
        public string NewPropertyName { get; set; } // For rename operations
        public string NewEntityName { get; set; } // For rename table operations
        public string DataType { get; set; }
        public bool? IsNullable { get; set; }
        // Add other relevant properties as needed
    }


    public class MigrationAnalyzer
    {
        public void AnalyzeMigrations()
        {
            var migrationsAssembly = Assembly.Load("Migrations.DAL");
            var migrationTypes = migrationsAssembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(DbMigration)) && !t.IsAbstract)
                .ToList();

            var analyzer = new MigrationAnalyzer();

            foreach (var migrationType in migrationTypes)
            {
                Console.WriteLine($"Analyzing Migration: {migrationType.Name}");

                var migration = (DbMigration)Activator.CreateInstance(migrationType);
                var actions = analyzer.AnalyzeMigration(migration);

                foreach (var action in actions)
                {
                    Console.WriteLine($"Entity: {action.Entity}, Action: {action.ActionType}, Property: {action.PropertyName}, DataType: {action.DataType}, Nullable: {action.IsNullable}");
                }
            }
        }
        public List<MigrationAction> AnalyzeMigration(DbMigration migration)
        {
            var migrationActions = new List<MigrationAction>();

            // Get the Up method
            var upMethod = migration.GetType().GetMethod("Up", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (upMethod != null)
            {
                try
                {
                    // Invoke the Up method to populate the operations
                    upMethod.Invoke(migration, null);

                    // Access the private field "_operations" which stores the operations
                    var operationsField = typeof(DbMigration).GetField("_operations", BindingFlags.Instance | BindingFlags.NonPublic);
                    var operations = operationsField?.GetValue(migration) as IList<MigrationOperation>;

                    if (operations != null)
                    {
                        // Iterate through the operations and extract the relevant information
                        foreach (var operation in operations)
                        {
                            if (operation is AddColumnOperation addColumnOperation)
                            {
                                migrationActions.Add(new MigrationAction
                                {
                                    Entity = addColumnOperation.Table,
                                    ActionType = "AddColumn",
                                    PropertyName = addColumnOperation.Column.Name,
                                    DataType = addColumnOperation.Column.Type.ToString(),
                                    IsNullable = addColumnOperation.Column.IsNullable
                                });
                            }
                            else if (operation is DropColumnOperation dropColumnOperation)
                            {
                                migrationActions.Add(new MigrationAction
                                {
                                    Entity = dropColumnOperation.Table,
                                    ActionType = "DropColumn",
                                    PropertyName = dropColumnOperation.Name
                                });
                            }
                            else if (operation is CreateTableOperation createTableOperation)
                            {
                                foreach (var column in createTableOperation.Columns)
                                {
                                    migrationActions.Add(new MigrationAction
                                    {
                                        Entity = createTableOperation.Name,
                                        ActionType = "CreateTable",
                                        PropertyName = column.Name,
                                        DataType = column.Type.ToString(),
                                        IsNullable = column.IsNullable
                                    });
                                }
                            }
                            else if (operation is DropTableOperation dropTableOperation)
                            {
                                migrationActions.Add(new MigrationAction
                                {
                                    Entity = dropTableOperation.Name,
                                    ActionType = "DropTable"
                                });
                            }
                            else if (operation is RenameColumnOperation renameColumnOperation)
                            {
                                migrationActions.Add(new MigrationAction
                                {
                                    Entity = renameColumnOperation.Table,
                                    ActionType = "RenameColumn",
                                    PropertyName = renameColumnOperation.Name,
                                    NewPropertyName = renameColumnOperation.NewName
                                });
                            }
                            else if (operation is RenameTableOperation renameTableOperation)
                            {
                                migrationActions.Add(new MigrationAction
                                {
                                    Entity = renameTableOperation.Name,
                                    ActionType = "RenameTable",
                                    NewEntityName = renameTableOperation.NewName
                                });
                            }
                        }
                        // Handle other types of operations here (DropColumn, CreateTable, etc.)
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error analyzing migration: {ex.Message}");
                }
            }

            return migrationActions;
        }

        private List<MigrationAction> ExtractActions(DbMigration migration)
        {
            var actions = new List<MigrationAction>();

            // This is a simplified example; adjust based on your actual needs.
            var fields = migration.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields)
            {
                //if (field.FieldType == typeof(System.Data.Entity.Migrations.Model.AddColumnOperation))
                //{
                //    var operation = (System.Data.Entity.Migrations.Model.AddColumnOperation)field.GetValue(migration);
                //    actions.Add(new MigrationAction
                //    {
                //        Entity = operation.Table,
                //        ActionType = "AddColumn",
                //        PropertyName = operation.Column.Name,
                //        DataType = operation.Column.Type.ToString(),
                //        IsNullable = operation.Column.IsNullable
                //    });
                //}
                // Add more cases for other operations like DropColumn, CreateTable, etc.
            }

            return actions;
        }
    }
}
