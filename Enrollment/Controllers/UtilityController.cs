using Enrollment.DAL;
using Enrollment.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enrollment.Controllers
{
    public class UtilityController : Controller
    {
        protected EnrollmentContext db = new EnrollmentContext();

        protected Participant GetCurrentParticipant()
        {
            Guid userID = Guid.Parse(User.Identity.GetUserId());
            return db.Participants.Single(s => s.IdentityID == userID);
        }
    }
}