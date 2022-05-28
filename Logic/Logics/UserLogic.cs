using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Logic.DTOs;
using AutoMapper;

namespace Logic
{
    public class UserLogic : IUserLogic
    {
        IUnitOfWork UoW;

        public UserLogic(IUnitOfWork UoW)
        {
            UserLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Transport, TransportDTO>();
                cfg.CreateMap<TransportDTO, Transport>();
                cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
                cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
                cfg.CreateMap<HotelRoomReservationDTO, HotelRoomReservation>();
                cfg.CreateMap<HotelRoomReservation, HotelRoomReservationDTO>();

            }).CreateMapper();
            this.UoW = UoW;
            HotelRoomToDto = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();
        }

        IMapper UserLogicMapper;
        IMapper HotelRoomToDto;

        public UserLogic()
        {
            UserLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Transport, TransportDTO>();
                cfg.CreateMap<TransportDTO, Transport>();
                cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
                cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
                cfg.CreateMap<HotelRoomReservationDTO, HotelRoomReservation>();
                cfg.CreateMap<HotelRoomReservation, HotelRoomReservationDTO>();
            }).CreateMapper();
            HotelRoomToDto = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();
            UoW = LogicDependencyResolver.ResolveUoW();
        }

        public void AddUser(UserDTO NewUser)
        {
            UoW.Users.Add(UserLogicMapper.Map<UserDTO, User>(NewUser));
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return UserLogicMapper.Map<IEnumerable<User>,List<UserDTO>>(UoW.Users.GetAll(u => u.HotelRoomReservations, u => u.TransportTickets, u => u.Tours));
        }

        public UserDTO GetUser(int Id)
        {
            return GetAllUsers().FirstOrDefault(u => u.Id == Id);
        }

        public void EditUser(int Id, UserDTO User)
        {
            UoW.Users.Modify(Id, UserLogicMapper.Map<UserDTO, User>(User));
        }

        public void DeleteUser(int Id)
        {
            UoW.Users.Delete(Id);
        }

        public UserDTO Enter(string Login, string Password)
        {
            UserDTO user = GetAllUsers().FirstOrDefault(u => u.Login == Login && u.Password == Password);
            if (user == null)
                throw new InvalidLoginPasswordCombinationException("Invalid login password combination");
            return user;
        }

        public void ReserveTour(int UserId, int TourId)
        {
            Tour tour = UoW.ToursTemplates.Get(TourId);
            User user = UoW.Users.Get(UserId);
            user.Tours.Add(tour);
            UoW.Users.Modify(user.Id, user);
        }

        public void ReserveRoom(int UserId, int HotelId, int HotelRoomId, DateTimeOffset ArrivalDate, DateTimeOffset DepartureDate)
        {
            User user = UoW.Users.GetAll(u => u.HotelRoomReservations).First(x => x.Id == UserId);
            HotelRoom hotelroom =UoW.Hotels.GetAll(h => h.Rooms).FirstOrDefault(h => h.Id == HotelId).Rooms[0];

            foreach (DateTimeOffset d in hotelroom.BookedDays)
            {
                DateTimeOffset FakeArrival = ArrivalDate;
                DateTimeOffset FakeDeparture = DepartureDate;
                while (FakeArrival.CompareTo(FakeDeparture) < 0)
                {
                    if ((d.Date.CompareTo(FakeArrival.Date) == 0))
                        throw new AlreadyBookedItemException("Room is not availible for " + d.Day + "." + d.Month + "." + d.Year);
                    FakeArrival = FakeArrival.AddDays(1);
                }
            }
            var reserv = new HotelRoomReservation(hotelroom, user.Name, user.Surname, ArrivalDate.Date, DepartureDate.Date);
            while (ArrivalDate.CompareTo(DepartureDate) < 0)
            {
                hotelroom.BookedDays.Add(ArrivalDate.Date);
                ArrivalDate = ArrivalDate.AddDays(1);
            }
            UoW.HotelsRooms.Modify(hotelroom.Id, hotelroom);
            UoW.HotelsRoomsReservations.Add(reserv);
            user.HotelRoomReservations.Add(reserv);
            UoW.Users.Modify(user.Id, user);
        }

        public void ReserveTicket(int UserId, int TransportId, int SeatNumber)
        {
            User user = UoW.Users.Get(UserId);
            Transport transport = UoW.Transports.Get(TransportId);
            TransportPlace transportplace = transport.TransportPlaces.FirstOrDefault(r => r.Number == SeatNumber);
            if (transportplace.IsBooked)
                throw new AlreadyBookedItemException("Transport place is already booked");
            else
            {
                transportplace.IsBooked = true;
                UoW.Transports.Modify(transport.Id, transport);
                user.TransportTickets.Add(new TransportTicket(transportplace, user.Name, user.Surname));
                UoW.Users.Modify(user.Id, user);
            }
        }
    }
}
