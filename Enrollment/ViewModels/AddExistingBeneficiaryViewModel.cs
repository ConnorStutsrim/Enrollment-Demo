﻿using Enrollment.DAL;
using Enrollment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace Enrollment.ViewModels
{
    public class AddExistingBeneficiaryViewModel
    {
        public AddExistingBeneficiaryViewModel(EnrollmentContext db)
        {
            Guid userID = Guid.Parse(HttpContext.Current.User.Identity.GetUserId());
            var excludedDependents = new List<Dependent>();
            foreach (Dependent dep in db.Participants.Single(s => s.IdentityID == userID).Dependents)
            {
                foreach (Beneficiary ben in db.Participants.Single(s => s.IdentityID == userID).Beneficiaries)
                {
                    if (ben.PersonID == dep.PersonID) excludedDependents.Add(dep);
                }
            }
            DependentList = new SelectList(db.Participants.Single(s => s.IdentityID == userID).Dependents.Except(excludedDependents), "PersonID", "Person.FullName");
            RelationshipTypeList = new SelectList(db.RelationshipTypes, "RelationshipTypeID", "RelationshipTypeName");
        }

        public Beneficiary Beneficiary { get; set; }

        public SelectList DependentList { get; set; }

        public SelectList RelationshipTypeList { get; set; }

        [Display(Name = "Would you like to add any additional beneficiaries?")]
        public bool AdditionalBeneficiary { get; set; }
    }
}