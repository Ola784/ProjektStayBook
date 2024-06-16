using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleKiosk.SharedKernel.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
    }
}
