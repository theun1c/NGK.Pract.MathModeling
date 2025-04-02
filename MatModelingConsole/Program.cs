using System.Globalization;
using System.Threading.Channels;

namespace MatModelingConsole
{
    internal class Program
    {
        static void Method1_Sever()
        {
            int N = 4, M = 5;

            int[] bj = { 13, 5, 13, 12, 13 }; // 56

            int[] ai = { 14, 14, 14, 14, }; // 56

            int[,] tableWithTariff =
            {
                { 16, 26, 12, 24,  3 },
                {  5,  2, 19, 27,  2 },
                { 29, 23, 25, 16,  8 },
                {  2, 25, 14, 15, 21 },
            };

            int[,] table = new int[N, M];

            int countA = 0, countB = 0;

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (ai[countA] > bj[countB])
                    {
                        table[countA, countB] = bj[countB];
                        ai[countA] -= bj[countB];
                        countB++;
                    }
                    else if (ai[countA] < bj[countB])
                    {
                        table[countA, countB] = ai[countA];
                        bj[countB] -= ai[countA];
                        countA++;
                    }
                    else
                    {
                        table[countA, countB] = ai[countA];
                    }
                }
            }

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write("{0,4}", table[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            int sum = 0;
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    sum += table[i, j] * tableWithTariff[i, j];
                }
            }

            Console.WriteLine("L(x) - " + sum);
        }

        static int[,] Method2_Minimal()
        {
            //int N = 4, M = 5;

            //int[] ai = { 14, 14, 14, 14, }; // 56

            //int[] bj = { 13, 5, 13, 12, 13 }; // 56

            //int[,] tableWithTariff =
            //{
            //    { 16, 26, 12, 24,  3 },
            //    {  5,  2, 19, 27,  2 },
            //    { 29, 23, 25, 16,  8 },
            //    {  2, 25, 14, 15, 21 },
            //};

            int N = 3, M = 3;

            int[] ai = { 90, 400, 110 };

            int[] bj = { 140, 300, 160 };

            int[,] tableWithTariff =
            {
                { 2, 5, 2 },
                { 4, 1, 5 },
                { 3, 6, 8 },
            };

            int[,] table = new int[N, M];


            int prevMinEl = 0;
            int count = M * N;
            while (count >= 0) 
            {
                int minEl = 999;
                for (int i = 0; i < table.GetLength(0); i++)
                {
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        if ((tableWithTariff[i, j] < minEl) && (tableWithTariff[i, j] > prevMinEl))
                        {
                            minEl = tableWithTariff[i, j];
                        }
                    }
                }

                prevMinEl = minEl;
                
                for (int i = 0; i < table.GetLength(0); i++)
                {
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        if (tableWithTariff[i, j] == prevMinEl) 
                        {
                            if (ai[i] > bj[j])
                            {
                                table[i, j] = bj[j];
                                ai[i] -= bj[j];
                                bj[j] = 0;
                            }
                            else if (ai[i] < bj[j])
                            {
                                table[i, j] = ai[i];
                                bj[j] -= ai[i];
                                ai[i] = 0;
                            }
                            else
                            {
                                table[i, j] = ai[i];
                            }
                        }
                    }
                    
                }      
                count--;
            }
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write("{0,4}", table[i, j]);
                   
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            int sum = 0;
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    sum += table[i, j] * tableWithTariff[i, j];
                }
            }

            Console.WriteLine("L(x) - " + sum);

            return table;
        }


        static public void Method_Potencialov(int[,] table_with_opor_plan, int[] ai, int[] bj, int[,] tableWithTariff)
        {

            int[] u = new int[ai.Length];
            int[] v = new int[bj.Length];
            Array.Fill(u, int.MinValue);
            Array.Fill(v, int.MinValue);
            u[0] = 0;

            bool help_flag = false;
            do
            {
                help_flag = false;
                for (int i = 0; i < ai.Length; i++)
                {
                    for (int j = 0; j < bj.Length; j++)
                    {
                        if (table_with_opor_plan[i, j] > 0)
                        {
                            if (u[i] != int.MinValue && v[j] == int.MinValue)
                            {
                                v[j] = tableWithTariff[i, j] - u[i];
                                help_flag = true;
                            }
                            else if (v[j] != int.MinValue && u[i] == int.MinValue)
                            {
                                u[i] = tableWithTariff[i, j] - v[j];
                                help_flag = true;
                            }
                        }
                    }
                }
            } while (help_flag);

            Console.WriteLine();
            Console.Write("ui: ");
            for (int i = 0; i < ai.Length; i++) 
            {
                Console.Write($"{u[i]} ");
            }
            Console.WriteLine();
            Console.Write("vj: ");
            for (int i = 0; i < bj.Length; i++)
            {
                Console.Write($"{v[i]} ");
            }
            Console.WriteLine();
            Console.WriteLine();
            bool isOptimal = true;
            for (int i = 0; i < ai.Length; i++)
            {
                for (int j = 0; j < bj.Length; j++)
                {
                    if (table_with_opor_plan[i, j] == 0)
                    {
                        int delta_uv = (u[i] + v[j]) - tableWithTariff[i, j];

                        if (delta_uv > 0)
                        {
                            Console.WriteLine($"delta[{i + 1}][{j + 1}]: {delta_uv} - опорный план не оптимален!");
                            isOptimal = false;
                        }
                        else
                        {
                            Console.WriteLine($"delta[{i + 1}][{j + 1}]: {delta_uv}");
                        }
                    }
                }
            }

            Console.WriteLine();
            if (isOptimal) 
            {
                Console.WriteLine("Опорный план оптимален!");
            }
            else
            {
                Console.WriteLine("Опорный план не оптимален!");
            }
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("метод северо-западного угла: ");
            //Method1_Sever();
            //Console.WriteLine();

            ////Неоптимальный метод
            //int[] ai = { 90, 400, 110 };

            //int[] bj = { 140, 300, 160 };

            //int[,] tableWithTariff =
            //{
            //    { 2, 5, 2 },
            //    { 4, 1, 5 },
            //    { 3, 6, 8 },
            //};

            //Method_Potencialov(Method2_Minimal(), ai, bj, tableWithTariff);


            ////Оптимальный метод
            //int[,] table = {
            //                 { 10, 0, 80, 0},
            //                 { 1, 100, 0, 0},
            //                 { 100, 0, 0, 40}
            //               };

            //int[] ai = { 90, 100, 140 };

            //int[] bj = { 110, 100, 80, 40 };

            //int[,] tableWithTariff =
            //{
            //    { 2, 4, 6, 10 },
            //    { 1, 3, 7, 4 },
            //    { 4, 8, 13, 7 },
            //};

            //Method_Potencialov(table, ai, bj, tableWithTariff);


        }
    }
}
