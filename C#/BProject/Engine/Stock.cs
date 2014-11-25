using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public abstract class Stock : Quote
    {
    }

    public class Open : Stock
    {
    }

    public class Close : Stock
    {
    }

    public class High : Stock
    {
    }

    public class Low : Stock
    {
    }

    public class Volume : Stock
    {
    }
}
