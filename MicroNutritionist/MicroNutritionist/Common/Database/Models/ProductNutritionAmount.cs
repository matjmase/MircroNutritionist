using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MicroNutritionist.Common.Database.Models
{
    public class ProductNutritionAmount
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed(Unique = false)]
        public int ProductId { get; set; }
        [Indexed(Unique = false)]
        public int NutritionId { get; set; }
        public double AmountMg { get; set; }
    }
}
