namespace MatModelingConsole
{
    internal class Program
    {
        static void Main(string[] args)
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
                    sum += table[i, j] * tableWithTariff[i,j]; 
                }
            }

            Console.WriteLine("L(x) - " + sum);
        }
    }
}
