﻿using Blazor.PoC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Domain.Interfaces
{
    public interface IBlazorDbContext
    {
        DbSet<Client> Clients { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
