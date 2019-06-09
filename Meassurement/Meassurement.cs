using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meassurement
{
    public class Meassurement
    {
        public int Id { get; set; }
        public decimal Pressure { get; set; }
        public decimal Humidity { get; set; }
        public decimal Temperature { get; set; }

        public DateTime TimeStamp { get; set; }
        public Meassurement(decimal pressure, decimal humidity, decimal temperature, DateTime timestamp)
        {

            this.Pressure = pressure;
            this.Humidity = humidity;
            this.Temperature = temperature;
            this.TimeStamp = timestamp;

        }
        public Meassurement()
        {

        }
        public override string ToString()
        {
            return $"{nameof(Id)}, {nameof(Pressure)},{nameof(Humidity)},{nameof(Temperature)},{nameof(TimeStamp)}";
        }
        //ALT + INSERT Laver automatisk ToString (en string der returnerer flere string's)
    }
}
