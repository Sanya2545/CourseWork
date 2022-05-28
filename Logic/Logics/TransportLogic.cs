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
    public class TransportLogic : ITransportLogic
    {
        IUnitOfWork UoW;

        public TransportLogic(IUnitOfWork UoW)
        {
            TransportLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransportDTO, Transport>();
                cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                cfg.CreateMap<Transport, TransportDTO>();
                cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
            }).CreateMapper();
            this.UoW = UoW;
        }

        IMapper TransportLogicMapper;

        public TransportLogic()
        {
            TransportLogicMapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TransportDTO, Transport>();
                    cfg.CreateMap<TransportPlaceDTO, TransportPlace>();
                    cfg.CreateMap<Transport, TransportDTO>();
                    cfg.CreateMap<TransportPlace, TransportPlaceDTO>();
                }).CreateMapper();
            UoW = LogicDependencyResolver.ResolveUoW();
        }

        public void AddTransport(TransportDTO NewTransport, int AvailibleSeats, int PriceForTicket)
        {
            for (int i = 1; i <= AvailibleSeats; i++)
                NewTransport.TransportPlaces.Add(new TransportPlaceDTO(NewTransport, i, PriceForTicket));
            UoW.Transports.Add(TransportLogicMapper.Map<TransportDTO, Transport>(NewTransport));
        }

        public void DeleteTransport(int Id)
        {
            UoW.Transports.Delete(Id);
        }

        public IEnumerable<TransportDTO> GetAllTransport()
        {
            return TransportLogicMapper.Map<IEnumerable<Transport>, List<TransportDTO>>(UoW.Transports.GetAll(t => t.TransportPlaces));
        }

        public TransportDTO GetTransport(int Id)
        {
            return GetAllTransport().FirstOrDefault(t => t.Id == Id);
        }
    }
}
