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
            int[,,] q = new int[100, 100, 100]; // массив с координатами и значением заряда в кажой точке кристалла
            int dim = 10; // степень дробления одного кубика  
            int count = 10; // количество кубиков 
            string tmp = string.Empty;
            string charge = string.Empty;

            Console.WriteLine("Введите координаты X, Y, Z и заряд в данной точке (например, 7,53;3,67;5,41;8). Для окончания введите \"конец\"" + Environment.NewLine);

            while (true)
            {
                Console.WriteLine("Введите координаты: ");
                tmp = Console.ReadLine();

                if (tmp == "конец") break;

                //if(tmp.Split(';')[0].Contains('-'))

                // foreach(var value in tmp.Split(';'))
                q[(int)Math.Round(Convert.ToDouble(tmp.Split(';')[0])), 
                    (int)Math.Round(Convert.ToDouble(tmp.Split(';')[1])), 
                    (int)Math.Round(Convert.ToDouble(tmp.Split(';')[2]))] = Convert.ToInt16(tmp.Split(';')[3]); //проверить

                Console.WriteLine();

            }

            Console.ReadKey();
        }

        // i – номер квадрата по длине, j – номер квадрата по ширине, k – номер квадрата по высоте

        // x – координата в длину, y – координата в ширину, z – координата в высоту.

        //        for (int i = 1; i < 10; i++) {
        //            for (int j = 1; j < 10; j++) {
        //                for (int k = 1; k < 10; k++) {
        //                    for (int x = 0; x < 100; x++) {
        //                        for (int y = 0; y < 100; y++) {
        //                            for (int z = 0; z < 100; z++) {
        //                                q[100 * i + x, 100 * j + y, 100 * k + z] = q[x,y,z];
        //   
        //}
        //}
        //}
        //}
        //}
        //}

    }
}
