﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SaleKiosk.SharedKernel.Dto
{
    public class UpdateUserDto
    {
       
            public int Id { get; set; }
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Username { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }
            public string ImageUrl { get; set; }

    }
}
