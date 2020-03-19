using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllYourPlates.Domain.Abstract;

namespace AllYourPlates.WebUI.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/

        private IRepository repository;

        public NavController(IRepository repository)
        {
            this.repository = repository;
        }

        public PartialViewResult Menu(string userName = null)
        {
            ViewBag.userName = userName;

            IEnumerable<string> users = repository.Users
                                        .Select(x => x.Name)
                                        .Distinct()
                                        .OrderBy(x => x);
            return PartialView(users);
        }

    }
}
