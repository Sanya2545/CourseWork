using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public enum UserType { Administrator, Manager, User }
    public class UserModel
    {
        public UserModel() { }
        public UserModel(string Name, UserType UserType, string Surname, string Login, string Password)
        {
            this.Name = Name;
            this.UserType = UserType;
            this.Surname = Surname;
            this.Login = Login;
            this.Password = Password;
        }
        public int Id { get; set; }

        public string Name { get; set; }
        public UserType UserType { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual List<TourModel> Tours { get; set; }
        public virtual List<HotelRoomReservationModel> HotelRoomReservations { get; set; }
        public virtual List<TransportTicketModel> TransportTickets { get; set; }

    }
}
