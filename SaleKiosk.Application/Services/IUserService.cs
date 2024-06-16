using SaleKiosk.SharedKernel.Dto;

namespace SaleKiosk.Application.Services
{
    public interface IUserService
    {
        List<UserDto> GetAll();
        UserDto GetById(int id);
        int Create(CreateUserDto dto);
        void Update(UpdateUserDto dto);
        void Delete(int id);
    }
}
