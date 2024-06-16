﻿using System.ComponentModel.DataAnnotations;


namespace SaleKiosk.SharedKernel.Dto
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Username { get; set; }
        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }
    }
}
