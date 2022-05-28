using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class TransportModel
    {
        public TransportModel() { }
        public TransportModel(string Type, string DeparturePoint, DateTimeOffset DepartureTime, string ArrivalPoint, DateTimeOffset ArrivalTime)
        {
            this.Type = Type;
            this.DeparturePoint = DeparturePoint;
            this.DepartureTime = DepartureTime;
            this.ArrivalPoint = ArrivalPoint;
            this.ArrivalTime = ArrivalTime;
            TransportPlaces = new List<TransportPlaceModel>();

        }
        public int Id { get; set; }
        public string Type { get; set; }
        public string DeparturePoint { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public string ArrivalPoint { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public virtual List<TransportPlaceModel> TransportPlaces { get; set; }
    }
}
