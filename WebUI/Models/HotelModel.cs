using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class HotelModel
    {
        public HotelModel() { }
        public HotelModel(string Name, int Stars, string Address)
        {
            this.Name = Name;
            this.Stars = Stars;
            this.Address = Address;
            Rooms = new List<HotelRoomModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public virtual List<HotelRoomModel> Rooms { get; set; }
        public string Address { get; set; }
    }
}
