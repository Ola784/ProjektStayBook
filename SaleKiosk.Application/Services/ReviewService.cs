using AutoMapper;
using SaleKiosk.Domain.Contracts;
using SaleKiosk.Domain.Exceptions;
using SaleKiosk.Domain.Models;
using SaleKiosk.SharedKernel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SaleKiosk.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IKioskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ReviewService(IKioskUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(CreateReviewDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("Review is null");
            }

            var id = _uow.ReviewRepository.GetMaxId() + 1;
            var review = _mapper.Map<Review>(dto);
            review.Id = id;

            _uow.ReviewRepository.Insert(review);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var review = _uow.ReviewRepository.Get(id);
            if (review == null)
            {
                throw new NotFoundException("Review not found");
            }

            _uow.ReviewRepository.Delete(review);
            _uow.Commit();
        }

        public List<ReviewDto> GetAll()
        {
            var reviews = _uow.ReviewRepository.GetAllWithDetails();

            List<ReviewDto> result = _mapper.Map<List<ReviewDto>>(reviews);
            return result;
        }

        public ReviewDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var review = _uow.ReviewRepository.GetByIdWithDetails(id);
            if (review == null)
            {
                throw new NotFoundException("Review not found");
            }

            var result = _mapper.Map<ReviewDto>(review);
            return result;
        }

        public void Update(UpdateReviewDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No user data");
            }

            var review = _uow.ReviewRepository.Get(dto.Id);
            if (review == null)
            {
                throw new NotFoundException("User not found");
            }

            review.ReviewText = dto.ReviewText;
            review.Rate = dto.Rate;

            _uow.Commit();
        }
    }

}
