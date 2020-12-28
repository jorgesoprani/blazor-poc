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

namespace Blazor.PoC.Application.Clients.Queries.GetClientDetail
{
    public class GetClientDetailQuery : IRequest<ClientDetailVm>
    {
        public int Id { get; set; }
    }

    public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, ClientDetailVm>
    {
        private readonly IBlazorDbContext _context;
        private readonly IMapper _mapper;
        public GetClientDetailQueryHandler(IBlazorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientDetailVm> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients
                .ProjectTo<ClientDetailVm>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return client;
        }
    }
}
