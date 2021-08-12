using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back_end.Controllers
{
    [ApiController]
    [Route("sensors")]
    public class SensorController : ControllerBase
    {
        private readonly Database dbController = new();

        private readonly ILogger<SensorController> _logger;

        public SensorController(ILogger<SensorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Sensor> Get()
        {

            dbController.OpenConnection();

            string sqliteQuery = "SELECT s.sensor_id, s.name, last, m.metric_id, rvalue, metric_name, units.unit_name FROM sensors s "+
                "LEFT JOIN(SELECT max(rtime) as last, *FROM measures GROUP BY sensor_id, metric_id) m ON s.sensor_id = m.sensor_id " +
                "LEFT JOIN metrics ON metrics.metric_id = m.metric_id " + 
                "LEFT JOIN units ON units.unit_id = metrics.unit_id";

            SQLiteCommand dbCommand = new(sqliteQuery, dbController.dbConnection);
            SQLiteDataReader result = dbCommand.ExecuteReader();

            List<Sensor> sensorList = new();

            if (result.HasRows)
            {

                while (result.Read())
                {

                    Sensor s = new();
                    s.Id = int.Parse(result["sensor_id"].ToString());
                    s.Name = result["name"].ToString();

                    int mId = result["metric_id"].ToString() != "" ? int.Parse(result["metric_id"].ToString()) : 0;
                    float value = result["metric_id"].ToString() != "" ? float.Parse(result["rvalue"].ToString()) : 0;

                    if (sensorList.Any(item => item.Id == s.Id))
                    {

                        var existingSensor = sensorList.FirstOrDefault(item => item.Id == s.Id);
                        existingSensor.Metrics.Add(new Metric
                        {
                            Id = mId,
                            Value = value,
                            Name = result["metric_name"].ToString(),
                            Unit = result["unit_name"].ToString(),
                            Time = result["last"].ToString()
                        }); 
                    }
                    else
                    {
                        if( mId > 0 )
                        {
                            s.Metrics.Add(new Metric
                            {
                                Id = mId,
                                Value = value,
                                Name = result["metric_name"].ToString(),
                                Unit = result["unit_name"].ToString(),
                                Time = result["last"].ToString()
                            });
                        }

                        sensorList.Add(s);
                    }

                }
                
            }

            dbController.CloseConnection();

            return sensorList;

        }
        [HttpGet("{date}")]
        public List<SensorMinMax> GetByDate(string date)
        {
            List<SensorMinMax> sensorList = new();

            DateTime dt;
            string[] formats = { "yyyy-MM-dd" };
            if (!DateTime.TryParseExact(date, formats, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {

                // Can return error if needed or [];

                return sensorList;
            }

            dbController.OpenConnection();

            string sqliteQuery = "SELECT min(rvalue) as min_val, max(rvalue) as max_val, s.sensor_id, s.name, m.metric_id FROM measures m " + 
                    "INNER JOIN sensors s ON s.sensor_id = m.sensor_id " + 
                    "WHERE DATE(m.rtime)= DATE(@date) GROUP BY m.sensor_id, m.metric_id ";

            SQLiteCommand dbCommand = new(sqliteQuery, dbController.dbConnection);
            dbCommand.Parameters.AddWithValue("@date", date);
            SQLiteDataReader result = dbCommand.ExecuteReader();

            if (result.HasRows)
            {

                while (result.Read())
                {

                    SensorMinMax s = new();
                    s.Id = int.Parse(result["sensor_id"].ToString());
                    s.Name = result["name"].ToString();

                    int mId = result["metric_id"].ToString() != "" ? int.Parse(result["metric_id"].ToString()) : 0;
                    float min = result["min_val"].ToString() != "" ? float.Parse(result["min_val"].ToString()) : 0;
                    float max = result["max_val"].ToString() != "" ? float.Parse(result["max_val"].ToString()) : 0;

                    if (sensorList.Any(item => item.Id == s.Id))
                    {

                        var existingSensor = sensorList.FirstOrDefault(item => item.Id == s.Id);
                        existingSensor.Metrics.Add(new MetricMinMax
                        {
                            Id = mId,
                            Min = min,
                            Max = max,
                        });
                    }
                    else
                    {
                        if (mId > 0)
                        {
                            s.Metrics.Add(new MetricMinMax
                            {
                                Id = mId,
                                Min = min,
                                Max = max,
                            });
                        }

                        sensorList.Add(s);
                    }

                }

            }

            dbController.CloseConnection();

            return sensorList;
        }
    }
}
