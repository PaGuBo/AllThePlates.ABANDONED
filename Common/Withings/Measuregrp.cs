using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllYourPlates.Common.Withings.Entities
{
    public class Measuregrp
    {
        private static TimeSpan ts = (new DateTime(1970, 1, 1)).Subtract(new DateTime(1, 1, 1));
        public int Grpid { get; set; }

        public int Attrib { get; set; }

        public long Date { get; set; }

        public DateTime FormatedDate
        {
            get
            {
                DateTime date = new DateTime(this.Date * 10000000);
                date += ts;
                return date;
            }
        }

        public int Category { get; set; }

        public Measure[] Measures { get; set; }
    }
}
