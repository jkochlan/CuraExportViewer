using Migrations.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Migrations.DAL
{
    public class ColumnTrackingInterceptor : DbCommandInterceptor
    {
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            // Skontrolujte, či ide o príkaz na pridanie nového stĺpca
            if (command.CommandText.StartsWith("ALTER TABLE", StringComparison.OrdinalIgnoreCase) &&
                command.CommandText.Contains("ADD", StringComparison.OrdinalIgnoreCase))
            {
                // Extrahovanie názvu tabuľky, stĺpca a typu stĺpca z SQL príkazu
                var tableName = ExtractTableName(command.CommandText);
                var columnInfo = ExtractColumnInfo(command.CommandText);

                // Vloženie informácií o stĺpci do sledovacej tabuľky
                TrackColumn(tableName, columnInfo);
            }

            base.NonQueryExecuting(command, interceptionContext);
        }

        private string ExtractTableName(string sql)
        {
            // Match "ALTER TABLE [schema].[TableName]" or "ALTER TABLE [TableName]" without schema
            var match = Regex.Match(sql, @"ALTER\s+TABLE\s+(?:\[(?<schema>[^\]]+)\]\.)?\[(?<table>[^\]]+)\]", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                // If schema is matched, return "schema.table", otherwise return just "table"
                var schema = match.Groups["schema"].Value;
                var table = match.Groups["table"].Value;

                return !string.IsNullOrEmpty(schema) ? $"{schema}.{table}" : table;
            }

            return null;
        }



        private (string ColumnName, string ColumnType) ExtractColumnInfo(string sql)
        {
            // Extrahujte názov a typ stĺpca (zjednodušený príklad)
            var match = Regex.Match(sql, @"ADD \[(.*?)\] (.*?)($|,)");
            return match.Success ? (match.Groups[1].Value, match.Groups[2].Value) : (null, null);
        }

        private void TrackColumn(string tableName, (string ColumnName, string ColumnType) columnInfo)
        {
            using (var context = new DataContext())
            {
                // Uloženie informácií do sledovacej tabuľky
                //context.DBChanges.Add(new DBChange
                //{
                //    TableName = tableName,
                //    ColumnName = columnInfo.ColumnName,
                //    ColumnType = columnInfo.ColumnType,
                //    CreatedAt = DateTime.Now
                //});
                //context.SaveChanges();

                context.ChangeLogs.Add(new ChangeLog
                {
                    ChangeType = System.Data.Entity.EntityState.Added,
                    ChangeDate = DateTime.Now,
                    ChangeContext = Guid.NewGuid(),
                    Entity = "DBChange",
                    Property = "ColumnName",
                    OldValue = null,
                    NewValue = columnInfo.ColumnName,
                    IsIncrementalValidated = null
                });
            }
        }
    }
}
