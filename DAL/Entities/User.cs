using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public enum UserType { Administrator, Manager, User }
    public class User
    {
        public User() {}
        public User(string Name, UserType UserType, string Surname, string Login, string Password)
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
        public virtual List<Tour> Tours { get; set; }
        public virtual List<HotelRoomReservation> HotelRoomReservations { get; set; }
        public virtual List<TransportTicket> TransportTickets { get; set; }
    }
}
