using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace build_and_calc_Madelung
{
    class Program
    {
        static void Main(string[] args)
        {
            //double[,,] q = new double[100, 100, 100]; // массив с координатами и значением заряда в кажой точке кристалла
            int dim = 10; // степень дробления одного кубика  
            int count = 10; // количество кубиков 
            string tmp = string.Empty;
            string charge = string.Empty;
            string params_str = string.Empty;
            double param_a = 0;
            double param_b = 0;
            double param_c = 0;

            int coord_x = 0;
            int coord_y = 0;
            int coord_z = 0;
            double coord_q = 0;

            bool only_first = true;

            double x_ = 0;
            double y_ = 0;
            double z_ = 0;
            double q_ = 0;
            double M = 0;

            double r_min = 100000;
            double r_tmp = 0;

            Ion middle_ion;

            //-----------

            List<Ion> ions = new List<Ion>();
            int counter = 0; // количество ионов в одном "кубике" 
            //int count_all = 0; // количество ионов во всей системе
            List<double> r_tmp_list = new List<double>();


            Console.WriteLine("Введите параметры кристаллической решетки, например, 2.3;5.6;7.8" + Environment.NewLine);

            param_a = 0.4889;
            param_b = 0.4889;
            param_c = 0.4889;

            //params_str = Console.ReadLine();
            //param_a = Convert.ToDouble(params_str.Split(';')[0]);
            //param_b = Convert.ToDouble(params_str.Split(';')[1]);
            //param_c = Convert.ToDouble(params_str.Split(';')[2]);

            Console.WriteLine("Введите координаты X, Y, Z и заряд в данной точке (например, 7.53;3.67;5.41;8). Для окончания введите \"конец\"" + Environment.NewLine);

            //while (true)
            //{
            //    Console.WriteLine("Введите координаты: ");
            //    tmp = Console.ReadLine();

            //    if (tmp == "конец") break;

            //    ions.Add(new Ion(tmp));

            //    counter++;
            //}

            ions.Add(new Ion("0;0;0;1,9", ions.Count));
            ions.Add(new Ion("0,24445;0;0;-1,9", ions.Count));
            ions.Add(new Ion("0,24445;0,24445;0;1,9", ions.Count));
            ions.Add(new Ion("0;0,24445;0;-1,9", ions.Count));
            ions.Add(new Ion("0;0;0,24445;-1,9", ions.Count));
            ions.Add(new Ion("0,24445;0;0,24445;1,9", ions.Count));
            ions.Add(new Ion("0;0,24445;0,24445;1,9", ions.Count));
            ions.Add(new Ion("0,24445;0,24445;0,24445;-1,9", ions.Count));
            counter = 8;

            //if(tmp.Split(';')[0].Contains('-'))

            // foreach(var value in tmp.Split(';'))

            //coord_x = Convert.ToInt16(Math.Round(Convert.ToDouble(tmp.Split(';')[0]) * dim / param_a));
            //coord_y = Convert.ToInt16(Math.Round(Convert.ToDouble(tmp.Split(';')[1]) * dim / param_b));
            //coord_z = Convert.ToInt16(Math.Round(Convert.ToDouble(tmp.Split(';')[2]) * dim / param_c));
            //coord_q = Convert.ToDouble(tmp.Split(';')[3]);

            //if (only_first)
            //{
            //    x_ = coord_x;
            //    y_ = coord_y;
            //    z_ = coord_z;
            //    q_ = coord_q;
            //    only_first = false;
            //}

            for (int h = 0; h < count; h++)
            {
                for(int al = 0; al < count; al ++)
                {
                    for(int l = 0; l < count; l ++)
                    {
                        if (h == 0 && al == 0 && l == 0) continue;
                        for (int k = 0; k < counter; k++)
                        {
                            double x = ions[k].x + param_a * h;
                            double y = ions[k].y + param_b * al;
                            double z = ions[k].z + param_c * l;
                            double q = ions[k].q;
                            ions.Add(new Ion(x, y, z, q, ions.Count));
                        }                           
                    }
                }
            }

            x_ = ions.First().x;
            y_ = ions.First().y;
            z_ = ions.First().z;
            q_ = ions.First().q;


            middle_ion = ions.Where(c => c.x >= 5 * param_a && c.x <= 6 * param_a && c.y >= 5 * param_b && c.y <= 6 * param_b && c.z >= 5 * param_c && c.z <= 6 * param_c).First();
           // middle_ion = ions[Convert.ToInt16(ions.Count/2)];

            foreach(var ion in ions)
            {
                if (ion.id == middle_ion.id) continue;

                double x = ion.x;
                double y = ion.y;
                double z = ion.z;
                double q = ion.q;

                ion.r = r_tmpCalc(middle_ion.x, middle_ion.y, middle_ion.z, x, y, z);
                //r_tmp_list.Add(r_tmp);

                if (q_ * q < 0 && r_min > ion.r)
                    r_min = ion.r;
            }

            foreach(var ion in ions)
            {
                if (ion.id == middle_ion.id) continue;

                M += ion.q * r_min / ion.r;
            }

            foreach (var item in ions.Take(20))
                Console.WriteLine("x = " + item.x.ToString() + ", y = " + item.y.ToString() + ", z = " + 
                    item.z.ToString() + ", q = " + item.q.ToString());

            Console.WriteLine("r_min = " + r_min.ToString());
            Console.WriteLine("Постоянная Маделунга = " + M.ToString());

            Console.ReadKey();

        }

        public static double r_tmpCalc(double x1, double y1, double z1, double x2, double y2, double z2)
        {            
            return Math.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)) + ((z1 - z2) * (z1 - z2)));
        }
    }
}
