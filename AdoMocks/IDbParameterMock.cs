using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace AdoMocks
{
    public interface IDbParameterMock
    {
        DbParameter Object { get; }
    }

}
