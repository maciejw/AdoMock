using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace AdoMocks
{
    public partial class DbParameterMock : IDbParameterMock
    {
        private IAdoMockTestContext adoMockTestContext;

        public DbParameterMock(IAdoMockTestContext adoMockTestContext)
        {
            this.adoMockTestContext = adoMockTestContext;
        }

        public override string ParameterName { get; set; }

        DbParameter IDbParameterMock.Object
        {
            get
            {
                return this;
            }
        }
        public override DbType DbType { get; set; }
        public override int Size { get; set; }
        public override object Value { get; set; }


    }

}
