using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Data
{
    public class ChartVm
    {
        public List<ChartVmSeries> Series { get; set; }
    }

    public class ChartVmSeries
    {
        public string Category { get; set; }
        public double Quantity { get; set; }
        public double Value { get; set; }
    }
}
