using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Data
{
    public class ChartVm
    {
        public List<ChartSeries> Series { get; set; }
    }

    public class ChartSeries
    {
        public string Category { get; set; }
        public double Quantity { get; set; }
        public double Value { get; set; }
    }
}
