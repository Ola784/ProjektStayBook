using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.Application.Services
{
    public interface IHotelService
    {
        List<HotelDto> GetAll();
        HotelDto GetById(int id);
        int Create(CreateHotelDto dto);
        void Update(UpdateHotelDto dto);
        void Delete(int id);
    }
}
