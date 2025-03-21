﻿using MatFrem.Models.DomainModel;
using System.ComponentModel.DataAnnotations;

namespace MatFrem.Models.ViewModel
{
    public class ViewDriverModel
    {

        public int? DriverID { get; set; }
        public string? DriverName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNr { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? CurrentLocation { get; set; }
        public string? LicenseNumber { get; set; }

    }
}
