using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllYourPlates.Common.Withings.Entities
{
    public class Body
    {
        public int Updatetime {get; set;}
        public Measuregrp[] Measuregrps {get; set;}
    }
}
