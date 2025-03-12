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

        static void Method2_Minimal()
        {
            int N = 4, M = 5;

            int[] ai = { 14, 14, 14, 14, }; // 56

            int[] bj = { 13, 5, 13, 12, 13 }; // 56

            int[,] tableWithTariff =
            {
                { 16, 26, 12, 24,  3 },
                {  5,  2, 19, 27,  2 },
                { 29, 23, 25, 16,  8 },
                {  2, 25, 14, 15, 21 },
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

        }

        static void Main(string[] args)
        {
            Console.WriteLine("метод северо-западного угла: ");
            Method1_Sever();
            Console.WriteLine();

            Console.WriteLine("метод минимального элемента: ");
            Method2_Minimal();
            Console.WriteLine();

        }
    }
}
