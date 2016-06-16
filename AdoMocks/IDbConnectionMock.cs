using System;
using System.Collections.Generic;
using System.Linq;

namespace AdoMocks
{
    public interface IDbConnectionMock
    {
        int OpenedCount { get; }
        int ClosedCount { get; }
        List<IDbCommandMock> CommandCreated { get; }
    }

}
