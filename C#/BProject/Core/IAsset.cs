using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Core
{
    public interface IAsset
    {
        string Name { get; }
        Dictionary<QuoteType, Curve> Curves { get; set;}
    }
}
