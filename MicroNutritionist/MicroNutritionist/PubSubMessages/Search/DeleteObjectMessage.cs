using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.PubSubMessages.Search
{
    public class DeleteObjectMessage : ObjectMessage
    {
        public DeleteObjectMessage(object payload) : base(payload)
        {
        }
    }
}
