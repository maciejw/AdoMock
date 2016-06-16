using System;
using System.Collections.Generic;
using System.Linq;

namespace AdoMocks
{
    public interface IRecordConfiguration
    {
        IRecordConfiguration AddRecord(params object[] args);
        IAdoMockSetupContext RegisterExecuteReaderResult();
    }

}
