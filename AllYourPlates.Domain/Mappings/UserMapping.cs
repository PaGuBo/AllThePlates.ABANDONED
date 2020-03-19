using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using AllYourPlates.Domain.Entities;

namespace AllYourPlates.Domain.Mappings
{
    class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Id(c => c.Id).GeneratedBy.HiLo("user");
            Map(c => c.Name).Length(20).Not.Nullable();
        }
    }
}
