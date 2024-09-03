using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.DAL.Entities
{
    public class ChangeLog : IEntity
    {
        public int Id { get; set; }

        [Required]
        public EntityState ChangeType { get; set; }

        [Required]
        public DateTime ChangeDate { get; set; }

        [Required]
        public Guid ChangeContext { get; set; }

        [Required]
        [MaxLength(100)]
        public string Entity { get; set; }

        [MaxLength(100)]
        public string PrimaryKey { get; set; }

        public bool IsRelationChange { get; set; }

        [MaxLength(256)]
        public string Hash { get; set; }

        [MaxLength(256)]
        public string PredecessorHashReference { get; set; }

        [Required]
        [MaxLength(100)]
        public string Property { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public bool? IsIncrementalValidated { get; set; }
    }
}
