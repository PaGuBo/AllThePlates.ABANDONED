using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllYourPlates.Domain.Entities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Plate> Plates { get; set; }
        public virtual IList<BodyShot> BodyShots { get; set; }
        public virtual IList<Workout> Workouts { get; set; }
    }
}
