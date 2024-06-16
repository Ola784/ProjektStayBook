using AutoMapper;
using SaleKiosk.Domain.Contracts;
using SaleKiosk.Domain.Exceptions;
using SaleKiosk.Domain.Models;
using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IKioskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OrderService(IKioskUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(OrderDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Order is null");
            }

            var id = _uow.OrderRepository.GetMaxId() + 1;
            var order = _mapper.Map<Order>(dto);
            order.Id = id;

            _uow.OrderRepository.Insert(order);
            _uow.Commit();

            return id;
        }

        public List<OrderDto> GetAll()
        {
            var orders = _uow.OrderRepository.GetAll();

            List<OrderDto> result = _mapper.Map<List<OrderDto>>(orders);

            return result;
        }

        public OrderDto GetByIdWithDetails(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var order = _uow.OrderRepository.GetByIdWithDetails(id);
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }

            var result = _mapper.Map<OrderDto>(order);
            return result;
        }
    }


}
