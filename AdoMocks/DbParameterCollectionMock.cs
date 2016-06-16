using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace AdoMocks
{
    public partial class DbParameterCollectionMock : IDbParameterCollectionMock
    {
        private IAdoMockTestContext adoMockTestContext;

        readonly List<IDbParameterMock> addedParameters;

        public DbParameterCollectionMock(IAdoMockTestContext adoMockTestContext)
        {
            this.adoMockTestContext = adoMockTestContext;
            addedParameters = new List<IDbParameterMock>();
        }

        public override int Add(object value)
        {
            addedParameters.Add(value as IDbParameterMock);
            return addedParameters.Count - 1;
        }
        protected override DbParameter GetParameter(int index)
        {
            return addedParameters[index] as DbParameter;
        }

        public override int Count
        {
            get
            {
                return addedParameters.Count;
            }
        }

    }

}
