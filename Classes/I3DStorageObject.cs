using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public interface I3DStorageObject
    {
        int ID { get; }
        string Description { get; }
        double Weight { get; }
        double Volume { get; }
        double Area { get;  }
        bool IsFragile { get; }
        double Maxdimension { get;  }
        int InsuranceValue { get; }
    }
}
