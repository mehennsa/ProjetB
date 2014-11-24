﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Core
{
    public interface IAsset
    {
        string Name { get; set; }
        Dictionary<string, Curve> Stocks { get; set;}
    }
}