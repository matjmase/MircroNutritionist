using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.PubSubMessages.Search
{
    public class SelectObjectMessage : ObjectMessage
    {
        public SelectObjectMessage(object payload) : base(payload)
        {
        }
    }
}
