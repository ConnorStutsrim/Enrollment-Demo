﻿using Enrollment.DAL;
using Enrollment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enrollment.ViewModels
{
    public class AddDeceaseSpouseViewModel
    {
        public AddDeceaseSpouseViewModel(EnrollmentContext db)
        {
            PersonAddressViewModel = new PersonAddressViewModel(db);
        }

        public PersonAddressViewModel PersonAddressViewModel { get; set; }

        public Address ParticipantAddress { get; set; }

        [Display(Name = "Date of Marriage")]
        [DataType(DataType.Date, ErrorMessage = "Date of Marriage must be a valid date")]
        public DateTime MarriageDate { get; set; }

        [Display(Name = "Date of Death")]
        [DataType(DataType.Date, ErrorMessage = "Date of Death must be a valid date")]
        public DateTime DeathDate { get; set; }
    }
}