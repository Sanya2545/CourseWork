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
    public class TourLogic : ITourLogic
    {
        IUnitOfWork UoW;

        public TourLogic(IUnitOfWork UoW)
        {
            TourLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
            }).CreateMapper();
            this.UoW = UoW;
        }

        IMapper TourLogicMapper;

        public TourLogic()
        {
            TourLogicMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TourDTO, Tour>();
                cfg.CreateMap<Tour, TourDTO>();
            }).CreateMapper();
            UoW = LogicDependencyResolver.ResolveUoW();
        }

        public void AddTour(TourDTO NewTour)
        {
            UoW.ToursTemplates.Add(TourLogicMapper.Map<TourDTO, Tour>(NewTour));
        }

        public void DeleteTour(int Id)
        {
            UoW.ToursTemplates.Delete(Id);
        }

        public void EditTour(int Id, TourDTO Tour)
        {
            Tour tour = UoW.ToursTemplates.Get(Id);
            tour = TourLogicMapper.Map<TourDTO, Tour>(Tour);
            UoW.ToursTemplates.Modify(Id, tour);
        }

        public IEnumerable<TourDTO> GetAllToursTemplates()
        {
            return TourLogicMapper.Map<IEnumerable<Tour>, List<TourDTO>>(UoW.ToursTemplates.GetAll());
        }

        public TourDTO GetTour(int Id)
        {
            return TourLogicMapper.Map<Tour, TourDTO>(UoW.ToursTemplates.GetAll().FirstOrDefault(t => t.Id == Id));
        }
    }
}
