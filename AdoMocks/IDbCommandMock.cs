using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace AdoMocks
{
    public interface IDbCommandMock
    {
        List<IDbParameterMock> CreatedParameters { get; }
        DbCommand Object { get; }
    }

}
