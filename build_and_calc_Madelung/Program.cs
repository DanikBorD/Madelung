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
            double[,,] q = new double[100, 100, 100]; // массив с координатами и значением заряда в кажой точке кристалла
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

            int x_ = 0;
            int y_ = 0;
            int z_ = 0;
            double q_ = 0;
            double M = 0;

            double r_min = 1000;
            double r_tmp = 0;


            Console.WriteLine("Введите параметры кристаллической решетки, например, 2.3;5.6;7.8" + Environment.NewLine);

            params_str = Console.ReadLine();
            param_a = Convert.ToDouble(params_str.Split(';')[0]);
            param_b = Convert.ToDouble(params_str.Split(';')[1]);
            param_c = Convert.ToDouble(params_str.Split(';')[2]);

            Console.WriteLine("Введите координаты X, Y, Z и заряд в данной точке (например, 7.53;3.67;5.41;8). Для окончания введите \"конец\"" + Environment.NewLine);

            while (true)
            {
                Console.WriteLine("Введите координаты: ");
                tmp = Console.ReadLine();

                if (tmp == "конец") break;

                //if(tmp.Split(';')[0].Contains('-'))

                // foreach(var value in tmp.Split(';'))

                coord_x = Convert.ToInt16(Math.Round(Convert.ToDouble(tmp.Split(';')[0]) * dim / param_a));
                coord_y = Convert.ToInt16(Math.Round(Convert.ToDouble(tmp.Split(';')[1]) * dim / param_b));
                coord_z = Convert.ToInt16(Math.Round(Convert.ToDouble(tmp.Split(';')[2]) * dim / param_c));
                coord_q = Convert.ToDouble(tmp.Split(';')[3]);

                if (only_first)
                {
                    x_ = coord_x;
                    y_ = coord_y;
                    z_ = coord_z;
                    q_ = coord_q;
                    only_first = false;
                }

                for (int x = 0; x < count; x++)
                {
                    for(int y = 0; y < count; y ++)
                    {
                        for(int z = 0; z < count; z ++)
                        {
                            int xx = coord_x + (x * dim);
                            int yy = coord_y + (y * dim);
                            int zz = coord_z + (z * dim);

                            q[xx, yy, zz] = coord_q;

                            if (coord_q * q_ < 0)
                            {
                                r_tmp = Math.Sqrt(((xx - x_) * (xx - x_) * param_a * param_a) + ((yy - y_) * (yy - y_) * param_b * param_b) +
                                    ((zz - z_) * (zz - z_) * param_c * param_c)) / dim;

                                if (r_min > r_tmp) r_min = r_tmp;
                            }                            
                        }
                    }
                }

                Console.WriteLine();

            }

            for (int i = 0; i < count * dim; i++)
                for (int j = 0; j < count * dim; j++)
                    for (int k = 0; k < count * dim; k++)
                        if (!((i == x_) && (j == y_) && (k == z_)))
                        {
                            r_tmp = Math.Sqrt(((i - x_) * (i - x_) * param_a * param_a) + ((j - y_) * (j - y_) * param_b * param_b) +
                                   ((k - z_) * (k - z_) * param_c * param_c)) / dim;

                            M += q[i, j, k] * r_min / r_tmp;
                        }

            Console.WriteLine("Постоянная Маделунга = " + M.ToString());

            Console.ReadKey();

        }
    }
}
