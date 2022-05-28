using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Transport
    {
        public Transport() { }
        public Transport(string Type, string DeparturePoint, DateTimeOffset DepartureTime, string ArrivalPoint, DateTimeOffset ArrivalTime)
        {
            this.Type = Type;
            this.DeparturePoint = DeparturePoint;
            this.DepartureTime = DepartureTime;
            this.ArrivalPoint = ArrivalPoint;
            this.ArrivalTime = ArrivalTime;
            TransportPlaces = new List<TransportPlace>();

        }
        public int Id { get; set; }
        public string Type { get; set; }
        public string DeparturePoint { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public string ArrivalPoint { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public virtual List<TransportPlace> TransportPlaces { get; set; }
    }
}
