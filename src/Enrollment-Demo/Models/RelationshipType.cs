using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enrollment.Models
{
    public class RelationshipType
    {
        public int RelationshipTypeID { get; set; }

        public string RelationshipTypeName { get; set; }

        public bool DependBenefitsDropDown { get; set; }

        public bool DependPensionDropDown { get; set; }

        public bool BeneficDropDown { get; set; }
    }
}