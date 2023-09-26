using MicroNutritionist.Common.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.PubSubMessages
{
    public class NutritionSelectedMessage
    {
        public Nutrition Nutrition { get; private set; }

        public NutritionSelectedMessage(Nutrition nutrition)
        {
            Nutrition = nutrition;
        }
    }
}
