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
    public class HotelLogic : IHotelLogic
    {
         IUnitOfWork UoW;

        public HotelLogic(IUnitOfWork UoW)
        {
            HotelLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDTO, Hotel>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO>();
            }).CreateMapper();
            this.UoW = UoW;
        }

        IMapper HotelLogicMapper;
    
        public HotelLogic()
        {
            HotelLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HotelDTO, Hotel>();
                cfg.CreateMap<HotelRoomDTO, HotelRoom>();
                cfg.CreateMap<Hotel, HotelDTO>();
                cfg.CreateMap<HotelRoom, HotelRoomDTO > ();
            }).CreateMapper();
            UoW = LogicDependencyResolver.ResolveUoW();
        }

        public void AddHotel(HotelDTO NewHotel)
        {
            UoW.Hotels.Add(HotelLogicMapper.Map<HotelDTO, Hotel>(NewHotel));
        }

        public void DeleteHotel(int Id)
        {
            UoW.Hotels.Delete(Id);
        }

        public void AddHotelRoom(int HotelId, HotelRoomDTO NewHotelRoom)
        {
            Hotel hotel = UoW.Hotels.GetAll(x => x.Rooms).FirstOrDefault(x => x.Id == HotelId);
            HotelRoom room = HotelLogicMapper.Map<HotelRoomDTO, HotelRoom>(NewHotelRoom);
            room.Hotel = hotel;
            hotel.Rooms.Add(room);
            UoW.Hotels.Modify(hotel.Id, hotel);
        }

        public IEnumerable<HotelDTO> GetAllHotels()
        {
            //return new List<HotelDTO> { new HotelDTO("Hust", 3, "Leleki 25"), new HotelDTO("Tisa", 3, "Mokili 255") };
            return HotelLogicMapper.Map<IEnumerable<Hotel>, List<HotelDTO>>(UoW.Hotels.GetAll(h => h.Rooms));
        }

        public HotelDTO GetHotel(int Id)
        {
            return GetAllHotels().FirstOrDefault(h => h.Id == Id);
        }
    }
}
