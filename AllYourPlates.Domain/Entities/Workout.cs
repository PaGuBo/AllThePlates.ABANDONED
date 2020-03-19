using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllYourPlates.Domain.Entities
{
    public class Workout
    {
        public virtual int Id { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual WorkoutType Type {get; set;}
    }
}
 