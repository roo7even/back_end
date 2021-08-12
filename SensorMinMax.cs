using System;
using System.Collections.Generic;

namespace back_end
{
    public class SensorMinMax
    {
        public SensorMinMax()
        {
            Metrics = new();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MetricMinMax> Metrics { get; set; }
    }
    public class MetricMinMax
    {
        public int Id { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }

    }

}
