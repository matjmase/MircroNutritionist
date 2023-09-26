using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.Common.Database.Models
{
    public class ProfileNutritionAmount
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed(Unique = false)]
        public int ProfileId { get; set; }
        [Indexed(Unique = false)]
        public int NutritionId { get; set; }
        public int AmountMg { get; set; }
    }
}
