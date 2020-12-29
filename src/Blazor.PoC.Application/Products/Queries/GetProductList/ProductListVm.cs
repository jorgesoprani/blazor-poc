using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Products.Queries.GetProductList
{
    public class ProductListVm
    {
        public IList<ProductListDto> Items { get; set; }
    }
}
