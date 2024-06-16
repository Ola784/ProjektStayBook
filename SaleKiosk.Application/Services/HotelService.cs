using AutoMapper;
using SaleKiosk.Domain.Contracts;
using SaleKiosk.Domain.Exceptions;
using SaleKiosk.Domain.Models;
using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IKioskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public HotelService(IKioskUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(CreateHotelDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Hotel is null");
            }

            var id = _uow.HotelRepository.GetMaxId() + 1;
            var hotel = _mapper.Map<Hotel>(dto);
            hotel.Id = id;

            // set default image url if user did not support its own
            hotel.ImageUrl = String.IsNullOrEmpty(dto.ImageUrl)
                ? "/images/no-image-icon.png" 
                : dto.ImageUrl;
            
            _uow.HotelRepository.Insert(hotel);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var hotel = _uow.HotelRepository.Get(id);
            if (hotel == null)
            {
               throw new NotFoundException("Hotel not found");
            }

            _uow.HotelRepository.Delete(hotel);
            _uow.Commit();
        }

        public List<HotelDto> GetAll()
        {
            var hotels = _uow.HotelRepository.GetAll();

            List<HotelDto> result = _mapper.Map<List<HotelDto>>(hotels);
            return result;
        }

        public HotelDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var hotel = _uow.HotelRepository.Get(id);
            if (hotel == null)
            {
                throw new NotFoundException("Hotel not found");
            }

            var result = _mapper.Map<HotelDto>(hotel);
            return result;
        }

        public void Update(UpdateHotelDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No hotel data");
            }

            var hotel = _uow.HotelRepository.Get(dto.Id);
            if (hotel == null)
            {
                throw new NotFoundException("Hotel not found");
            }

            hotel.Name = dto.Name;
            hotel.Description = dto.Desc;
            hotel.UnitPrice = dto.UnitPrice;

            // set default image url if user did not souuport its own
            hotel.ImageUrl = String.IsNullOrEmpty(dto.ImageUrl)
                ? "/images/no-image-icon.png"
                : dto.ImageUrl;

            _uow.Commit();
        }
    }


}
