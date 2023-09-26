using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.ViewModels.Search.Model
{
    public class StringSearchPageModel
    {
        public object[] Collection { get; private set; }
        public Func<object, string> StringDisplay { get; private set; }
        public StringSearchPageModelType ModelType { get; private set; }    

        public StringSearchPageModel(object[] collection, Func<object, string> stringDisplay, StringSearchPageModelType modelType)
        {
            Collection = collection;
            StringDisplay = stringDisplay;
            ModelType = modelType;
        }
    }

    public enum StringSearchPageModelType
    { 
        Select,
        Edit,
        Delete
    }
}
