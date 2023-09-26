using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MicroNutritionist.Common.Database.Models
{
    public class Nutrition
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed(Unique = true)]
        public string Name { get; set; }
    }
}
