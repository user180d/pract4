using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract4
{
    internal class Lab2 : MasterLab
    {
        string inputFile;
        string outputFile;
        int answer = 0;
        public Lab2(string i, string o)
        {
            inputFile = i;
            outputFile = o;
        }
        void entry(int shop, int steps)
        {
            if (steps == shop)
            {
                answer = 1;
                return;
            }
            else if (shop == steps - 1)
            {
                answer = 0;
                return;
            }
            else if ((steps - shop) % 2 != 0)
            {
                answer = 0;
                return;
            }
            else if (steps == shop + 4)
            {
                float n = shop, a = 0;
                a += (n + 3) * (n / 2);
                answer = (int)a;
                return;
            }
            else
            {
                solve(shop, steps);
            }
        }

        void solve(int shop, int steps)
        {

            if (shop == 0)
            {
                return;
            }
            else if (shop == steps - 2)
            {
                answer += shop;
            }
            else
            {
                solve(shop - 1, steps - 1);
                solve(shop + 1, steps - 1);
            }
        }
        public override bool start()
        {
            string[] input = File.ReadAllText(inputFile).Split();
            try
            {

                int n = int.Parse(input[0]);

                int k = int.Parse(input[1]);
                if (n != 0 && n <= k && k <= 37)
                {
                    entry(n, k);
                    Console.WriteLine(answer);
                    File.WriteAllText(outputFile, answer.ToString());
                    return true;
                }
                else
                {
                    Console.WriteLine("Distance to shop equal 0, bigger than amount of steps or some of paramets bigger than 37");
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
    }
}
