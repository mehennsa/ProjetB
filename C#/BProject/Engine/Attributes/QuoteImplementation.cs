using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct | System.AttributeTargets.Interface, 
                           AllowMultiple = true)]
    public class QuoteImplementation : Attribute
    {
        // Type of the quote   
        public QuoteType Type;

        // Implementation type info
        public Type ImplType;

        public QuoteImplementation()
        {

        }
    }
}
