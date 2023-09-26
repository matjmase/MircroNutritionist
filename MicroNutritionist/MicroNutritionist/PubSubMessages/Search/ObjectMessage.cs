using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.PubSubMessages.Search
{
    public class ObjectMessage
    {
        public object Payload { get; private set; }

        public ObjectMessage(object payload)
        {
            Payload = payload;
        }
    }
}
