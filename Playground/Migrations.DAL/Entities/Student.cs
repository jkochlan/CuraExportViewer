using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.DAL.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

    }
}
