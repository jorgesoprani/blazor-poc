using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blazor.PoC.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Store.Queries.GetOrderDetails
{
    public class GetOrderDetailsQuery : IRequest<OrderDetailsVm>
    {
        public int OrderId { get; set; }
    }

    public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsVm>
    {
        private readonly IBlazorDbContext _context;
        private readonly IMapper _mapper;
        public GetOrderDetailsHandler(IBlazorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDetailsVm> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Where(x => x.Id == request.OrderId)
                .ProjectTo<OrderDetailsVm>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return order;
        }
    }
}
