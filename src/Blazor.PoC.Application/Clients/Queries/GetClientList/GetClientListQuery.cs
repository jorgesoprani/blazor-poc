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

namespace Blazor.PoC.Application.Clients.Queries.GetClientList
{
    public class GetClientListQuery : IRequest<ClientListVm>
    {
    }

    public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, ClientListVm>
    {
        private readonly IBlazorDbContext _context;
        private readonly IMapper _mapper;
        public GetClientListQueryHandler(IBlazorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientListVm> Handle(GetClientListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Clients
                .ProjectTo<ClientListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new()
            {
                Items = list
            };
        }
    }
}
