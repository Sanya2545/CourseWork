using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic
{
    public interface ITourLogic
    {
        void AddTour(TourDTO NewTour);
        void EditTour(int Id, TourDTO Tour);
        IEnumerable<TourDTO> GetAllToursTemplates();
        TourDTO GetTour(int Id);
        void DeleteTour(int Id);
    }
}
