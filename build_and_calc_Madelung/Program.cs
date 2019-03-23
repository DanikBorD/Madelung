using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace build_and_calc_Madelung
{
    class Program
    {
        static void Main(string[] args)
        {
            #region variables 
            int count = 50; // количество кубиков 
            string tmp = string.Empty;
            string charge = string.Empty;
            string params_str = string.Empty;
            double param_a = 0;
            double param_b = 0;
            double param_c = 0;

            double M = 0;

            double r_min = 100000;
            Ion middle_ion;
            List<Ion> ions = new List<Ion>();
            int counter = 0; // количество ионов в одном "кубике" 
            string path = "ion_params.txt";
            string full_path = Directory.GetCurrentDirectory() + "\\" + path;

            int[] arr = new int[100];
             #endregion

            if (File.Exists(full_path))
            {
                Console.WriteLine("Начальные параметры были взяты по адресу:" + Environment.NewLine + full_path);
                Console.WriteLine();

                // Считывание параметров и координат ионов в первом "кубике"
                readParameters(ref param_a, ref param_b, ref param_c, ions, ref counter, path);
            }
            else
                Console.WriteLine("Файл с начальными парамерами не найден в папке с проектом");

            // Расчет и построение кристалла  
            buildCrystal(count, param_a, param_b, param_c, ions, counter);
            
            // Получение иона близкого к центру кристалла
            middle_ion = ions
                .Where(c => c.x >= Convert.ToInt16(count/2) * param_a && c.x <= Convert.ToInt16(count / 2)+1 * param_a 
                && c.y >= Convert.ToInt16(count / 2) * param_b && c.y <= Convert.ToInt16(count / 2)+1 * param_b 
                && c.z >= Convert.ToInt16(count / 2) * param_c && c.z <= Convert.ToInt16(count / 2)+1 * param_c).First();

            // Определение минимального расстояния между ионами в кристалле, имеющим разные по знаку заряды
            r_min = calcR(r_min, middle_ion, ions);

            // Расчет Постоянной Маделунга
            foreach (var ion in ions)
            {
                if (ion.id == middle_ion.id) continue;

                M += ion.q * r_min / ion.r;
            }

            // Вывод значений в консоль
            foreach (var item in ions.Take(20))
                Console.WriteLine("x = " + item.x.ToString() + ", y = " + item.y.ToString() + ", z = " +
                    item.z.ToString() + ", q = " + item.q.ToString() + ", id =" + item.id);

            Console.WriteLine(Environment.NewLine + "Количество ионов с нулевым R: " + ions.Where(c => c.r == 0).Count());
            Console.WriteLine();
            Console.WriteLine("Целевой ион = (" + middle_ion.x + "; " + middle_ion.y + "; " + middle_ion.z + ") c зарядом = " + middle_ion.q);
            Console.WriteLine();
            Console.WriteLine("Минимальное расстояние  = " + r_min.ToString());
            Console.WriteLine();
            Console.WriteLine("Постоянная Маделунга = " + M.ToString());
            Console.WriteLine();
            Console.WriteLine("Количество ионов в кристалле = " + ions.Count());
            Console.WriteLine();
     
            Console.ReadKey();

        }

        private static double calcR(double r_min, Ion middle_ion, List<Ion> ions)
        {
            foreach (var ion in ions)
            {
                if (ion.id == middle_ion.id) continue;

                double x = ion.x;
                double y = ion.y;
                double z = ion.z;
                double q = ion.q;

                ion.r = r_tmpCalc(middle_ion.x, middle_ion.y, middle_ion.z, x, y, z);
                //r_tmp_list.Add(r_tmp);

                if (middle_ion.q * q < 0 && r_min > ion.r)
                    r_min = ion.r;
            }

            return r_min;
        }

        private static void buildCrystal(int count, double param_a, double param_b, double param_c, List<Ion> ions, int counter)
        {
            for (int h = 0; h < count; h++)
            {
                for (int al = 0; al < count; al++)
                {
                    for (int l = 0; l < count; l++)
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
        }

        private static void readParameters(ref double param_a, ref double param_b, ref double param_c, List<Ion> ions, ref int counter, string path)
        {
            int ind = 1;
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                { 
                    string line = reader.ReadLine();
                    string[] input = line.Split(';');

                    switch (input.Count())
                    {
                        case 3:
                            param_a = Convert.ToDouble(input[0]);
                            param_b = Convert.ToDouble(input[1]);
                            param_c = Convert.ToDouble(input[2]);
                            Console.WriteLine("Параметры: " + param_a + ", " + param_b + ", " + param_c);
                            Console.WriteLine();
                            break;
                        case 4:
                            ions.Add(new Ion(line, ions.Count));
                            counter++;
                            break;
                        default:
                            Console.WriteLine(Environment.NewLine+"Ошибка считывания!"+Environment.NewLine + " На строке " + ind);
                            break;
                    }
                    ind++;
                }
            }
        }

        private static double r_tmpCalc(double x1, double y1, double z1, double x2, double y2, double z2)
        {            
            return Math.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)) + ((z1 - z2) * (z1 - z2)));
        }
    }
}
