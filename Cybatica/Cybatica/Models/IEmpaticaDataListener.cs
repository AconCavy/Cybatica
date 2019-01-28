using System;
using System.Collections.Generic;
using System.Text;

namespace Cybatica.Models
{
    public interface IEmpaticaDataListener
    {
        BatteryLevel GetBatteryLevel();
        Acceleration GetAcceleration();
        GSR GetEDA();
        BVP GetBVP();
        IBI GetIBI();
        Temperature GetTemperature();
        Tag GetTag();
    }
}
