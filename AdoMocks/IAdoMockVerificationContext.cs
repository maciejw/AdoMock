using System;
using System.Collections.Generic;
using System.Linq;

namespace AdoMocks
{
    public interface IAdoMockVerificationContext
    {
        List<IDbConnectionMock> ConnectionsCreated { get; }
    }

}
