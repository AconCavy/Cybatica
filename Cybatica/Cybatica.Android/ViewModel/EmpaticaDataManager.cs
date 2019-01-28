using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Cybatica.Models;
using Com.Empatica.Empalink.Delegate;

namespace Cybatica.Droid.ViewModel
{
    class EmpaticaDataManager : IEmpaticaDataListener, IEmpaDataDelegate
    {
        public IntPtr Handle => throw new NotImplementedException();

        public void DidReceiveAcceleration(int p0, int p1, int p2, double p3)
        {
            throw new NotImplementedException();
        }

        public void DidReceiveBatteryLevel(float p0, double p1)
        {
            throw new NotImplementedException();
        }

        public void DidReceiveBVP(float p0, double p1)
        {
            throw new NotImplementedException();
        }

        public void DidReceiveGSR(float p0, double p1)
        {
            throw new NotImplementedException();
        }

        public void DidReceiveIBI(float p0, double p1)
        {
            throw new NotImplementedException();
        }

        public void DidReceiveTag(double p0)
        {
            throw new NotImplementedException();
        }

        public void DidReceiveTemperature(float p0, double p1)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Acceleration GetAcceleration()
        {
            
            throw new NotImplementedException();
        }

        public BatteryLevel GetBatteryLevel()
        {
            throw new NotImplementedException();
        }

        public BVP GetBVP()
        {
            throw new NotImplementedException();
        }

        public GSR GetEDA()
        {
            throw new NotImplementedException();
        }

        public IBI GetIBI()
        {
            throw new NotImplementedException();
        }

        public Tag GetTag()
        {
            throw new NotImplementedException();
        }

        public Temperature GetTemperature()
        {
            throw new NotImplementedException();
        }
    }
}