using System;

namespace Blazor.PoC.Common
{
    public interface IDateTime
    {
        DateTime Now { get; }
        int CurrentYear { get; }
    }
}
