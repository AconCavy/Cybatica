using Cybatica.Empatica;

namespace Cybatica.Models
{
    public class DataSource
    {
        public string Id { get; set; }

        public Acceleration Acceleration { get; set; }

        public GSR GSR { get; set; }

        public BVP BVP { get; set; }

        public IBI IBI { get; set; }

        public Temperature Temperature { get; set; }

        public HR HR { get; set; }

        public DataSource()
        {
            new DataSource(new Acceleration(), new GSR(), new BVP(), new IBI(), new Temperature(), new HR());
        }

        public DataSource(Acceleration acceleration, GSR gsr,
            BVP bvp, IBI ibi, Temperature temperature, HR hr)
        {
            Acceleration = acceleration;
            GSR = gsr;
            BVP = bvp;
            IBI = ibi;
            Temperature = temperature;
        }

        

    }
}
