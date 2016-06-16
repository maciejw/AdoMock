using System;
using System.Collections.Generic;
using System.Linq;

namespace AdoMocks
{
    public interface IReaderConfiguration
    {
        IReaderConfiguration AddColumn<T>(string name);
    }

}
