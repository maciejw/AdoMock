using System;
using System.Collections.Generic;
using System.Linq;

namespace AdoMocks
{
    public interface IAdoMockSetupContext
    {
        IAdoMockSetupContext RegisterExecuteReaderResult(Exception exception);
        IRecordConfiguration SetupExecuteReaderResult(Action<IReaderConfiguration> p);
        IAdoMockSetupContext RegisterExecuteNonQueryResult(int result);
        IAdoMockSetupContext RegisterExecuteNonQueryResult(Exception exception);
    }

}
