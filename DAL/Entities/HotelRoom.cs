using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HotelRoom
    {
        public HotelRoom() { }
        public HotelRoom(Hotel Hotel, int Number, int SleepingPlaces, int Price)
        {
            this.Hotel = Hotel;
            this.Number = Number;
            this.SleepingPlaces = SleepingPlaces;
            this.Price = Price;
            BookedDays = new List<DateTimeOffset>();
        }

        public int Id { get; set; }
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
        public int Number { get; set; }
        public int SleepingPlaces { get; set; }
        public int Price { get; set; }
        public List<DateTimeOffset> BookedDays { get; set; }
    }
}
