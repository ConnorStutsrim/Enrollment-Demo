using Enrollment;
using Enrollment.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Enrollment.Tests.Controllers
{
    [TestClass]
    public class EnrollControllerTest
    {
        [TestMethod]
        public void CreateParticipantPerson()
        {
            EnrollController controller = new EnrollController();

            ViewResult result = controller.CreateParticipantPerson() as ViewResult;
        }

        [TestMethod]
        public void CreateParticipant()
        {

        }
    }
}
