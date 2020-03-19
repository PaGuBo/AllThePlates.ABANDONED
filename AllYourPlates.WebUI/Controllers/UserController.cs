using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllYourPlates.Domain.Abstract;
using AllYourPlates.WebUI.Models;

namespace AllYourPlates.WebUI.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Day/

        public int PageSize = 10;

        private IRepository repository;

        public UserController(IRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List(string userName)
        {
            UsersListViewModel viewModel = new UsersListViewModel
            {
                Users = repository.Users
                .Where(u => userName == null || u.Name == userName)
                .OrderBy(u => u.Name), 
                CurrentUser = userName
            };
            return View(viewModel);
        }

        public ViewResult Snapshot(string userName)
        {
            SnapshotViewModel viewModel = new SnapshotViewModel
            {
                User = repository.Users.Where(u => u.Name == userName).First()
            };

            return View(viewModel);
        }

    }
}
