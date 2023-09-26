using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroNutritionist.Common.Extensions
{
    public static class TaskExtensions
    {
        public static T WaitForResult<T>(this Task<T> task)
        { 
            task.Wait();
            return task.Result;
        }
    }
}
