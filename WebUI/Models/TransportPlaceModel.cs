using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class TransportPlaceModel
    {
        public TransportPlaceModel() { }
        public TransportPlaceModel(TransportModel Transport, int Number, int Price)
        {
            this.Transport = Transport;
            this.Number = Number;
            this.Price = Price;
            IsBooked = false;
        }

        public int Id { get; set; }
        public virtual TransportModel Transport { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public bool IsBooked { get; set; }

    }
}
