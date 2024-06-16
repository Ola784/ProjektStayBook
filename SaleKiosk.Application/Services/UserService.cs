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
    public class UserService :IUserService
    {
        private readonly IKioskUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IKioskUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        public int Create(CreateUserDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("User is null");
            }

            var id = _uow.UserRepository.GetMaxId() + 1;
            var user = _mapper.Map<User>(dto);
            user.Id = id;

            // set default image url if user did not souuport its own
            user.ImageUrl = String.IsNullOrEmpty(dto.ImageUrl)
                ? "/images/no-image-icon.png"
                : dto.ImageUrl;

            _uow.UserRepository.Insert(user);
            _uow.Commit();

            return id;
        }

        public void Delete(int id)
        {
            var user = _uow.UserRepository.Get(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            _uow.UserRepository.Delete(user);
            _uow.Commit();
        }

        public List<UserDto> GetAll()
        {
            var users = _uow.UserRepository.GetAll();

            List<UserDto> result = _mapper.Map<List<UserDto>>(users);
            return result;
        }

        public UserDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id is less than zero");
            }

            var user = _uow.UserRepository.Get(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public void Update(UpdateUserDto dto)
        {
            if (dto == null)
            {
                throw new BadRequestException("No user data");
            }

            var user = _uow.UserRepository.Get(dto.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;



            // set default image url if user did not souuport its own
            user.ImageUrl = String.IsNullOrEmpty(dto.ImageUrl)
                ? "/images/no-image-icon.png"
                : dto.ImageUrl;

            _uow.Commit();
        }
    }

}
