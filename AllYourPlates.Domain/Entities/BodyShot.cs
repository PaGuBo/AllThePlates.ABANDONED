using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllYourPlates.Domain.Entities
{
    public class BodyShot
    {
        public virtual int Id { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual decimal? Weight { get; set; }
        public virtual string FilePath { get; set; }
    }
}
