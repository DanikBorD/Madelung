﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace build_and_calc_Madelung
{
    class Ion
    {
        public int id;
        public double x, y, z, q, r;

        public Ion (string line, int id)
        {
            x = Convert.ToDouble(line.Split(';')[0]);
            y = Convert.ToDouble(line.Split(';')[1]);
            z = Convert.ToDouble(line.Split(';')[2]);
            q = Convert.ToDouble(line.Split(';')[3]);

            this.id = id;
		}

        public Ion (double a, double b, double c, double d, int id)
        {
            x = a;
            y = b;
            z = c;
            q = d;
            this.id = id;
        }
    }
}
