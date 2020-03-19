using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllYourPlates.Domain.Entities
{
    public class Plate
    {
        public virtual int Id { get; set; }
        public virtual Meal Meal { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime Time { get; set; }
    }
}
