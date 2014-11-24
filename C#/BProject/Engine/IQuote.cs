using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public interface IQuote
    {
        double Value { get; set; }
        DateTime Date { get; set; }
    }
}
