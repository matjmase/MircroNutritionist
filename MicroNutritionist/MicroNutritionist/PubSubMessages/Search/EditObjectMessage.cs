using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.PubSubMessages.Search
{
    public class EditObjectMessage : ObjectMessage
    {
        public EditObjectMessage(object payload) : base(payload)
        {
        }
    }
}
