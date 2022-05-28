using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class TransportPlace
    {
        public TransportPlace() { }
        public TransportPlace(Transport Transport, int Number, int Price)
        {
            this.Transport = Transport;
            this.Number = Number;
            this.Price = Price;
            IsBooked = false;
        }

        public int Id { get; set; }
        [ForeignKey("TransportId")]
        public virtual Transport Transport { get; set; }
        public int TransportId { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public bool IsBooked { get; set; }
    }
}
