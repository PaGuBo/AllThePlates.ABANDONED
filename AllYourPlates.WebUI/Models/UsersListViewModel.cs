using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AllYourPlates.Domain.Entities;

namespace AllYourPlates.WebUI.Models
{
    public class UsersListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentUser { get; set; }
    }
}