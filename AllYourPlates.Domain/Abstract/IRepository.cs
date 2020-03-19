using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllYourPlates.Domain.Entities;

namespace AllYourPlates.Domain.Abstract
{
    public interface IRepository
    {
        IQueryable<User> Users { get; }
    }
}