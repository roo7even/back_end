using System;
using System.Collections.Generic;

namespace back_end
{
    public class Sensor
    {
        public Sensor()
        {
            Metrics = new();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Metric> Metrics { get; set; }
    }
    public class Metric
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public float Value { get; set; }
        public string Time { get; set; }
    }

}
