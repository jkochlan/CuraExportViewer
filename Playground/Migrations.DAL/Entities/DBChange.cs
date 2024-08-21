using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.DAL.Entities
{
    public class DBChange
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public string ColumnType { get; set; }

        public string CommandText { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
