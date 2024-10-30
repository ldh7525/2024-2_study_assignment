using System;
using System.Linq;

namespace statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] data = {
                {"StdNum", "Name", "Math", "Science", "English"},
                {"1001", "Alice", "85", "90", "78"},
                {"1002", "Bob", "92", "88", "84"},
                {"1003", "Charlie", "79", "85", "88"},
                {"1004", "David", "94", "76", "92"},
                {"1005", "Eve", "72", "95", "89"}
            };

            int stdCount = data.GetLength(0) - 1;
            //1. 배열에 할당하기
            double[] mathScores = new double[stdCount];
            double[] scienceScores = new double[stdCount];
            double[] englishScores = new double[stdCount];
            string[] studentNames = new string[stdCount];
            double[] totalScores = new double[stdCount];

            for (int i = 1; i <= stdCount; i++)
            {
                mathScores[i - 1] = double.Parse(data[i, 2]);
                scienceScores[i - 1] = double.Parse(data[i, 3]);
                englishScores[i - 1] = double.Parse(data[i, 4]);
                studentNames[i - 1] = data[i, 1];
                totalScores[i - 1] = mathScores[i - 1] + scienceScores[i - 1] + englishScores[i - 1];
            }

            //2. 평균 계산
            double mathAverage = mathScores.Average();
            double scienceAverage = scienceScores.Average();
            double englishAverage = englishScores.Average();

            Console.WriteLine("Average Scores:");
            Console.WriteLine($"Math: {mathAverage:F2}");
            Console.WriteLine($"Science: {scienceAverage:F2}");
            Console.WriteLine($"English: {englishAverage:F2}\n");

            //3.최대/ 최소 계산
            double mathMax = mathScores.Max();
            double mathMin = mathScores.Min();
            double scienceMax = scienceScores.Max();
            double scienceMin = scienceScores.Min();
            double englishMax = englishScores.Max();
            double englishMin = englishScores.Min();

            Console.WriteLine("Max and min Scores:");
            Console.WriteLine($"Math: ({mathMax}, {mathMin})");
            Console.WriteLine($"Science: ({scienceMax}, {scienceMin})");
            Console.WriteLine($"English: ({englishMax}, {englishMin})\n");

            //4. 순위 매기기
            string[] rankedNames = new string[stdCount];

            Array.Copy(studentNames, rankedNames, stdCount);

            for (int i = 0; i < stdCount - 1; i++)
            {
                for (int j = i + 1; j < stdCount; j++)
                {
                    if (totalScores[i] < totalScores[j])
                    {
                        double tempScore = totalScores[i];
                        totalScores[i] = totalScores[j];
                        totalScores[j] = tempScore;

                        string tempName = rankedNames[i];
                        rankedNames[i] = rankedNames[j];
                        rankedNames[j] = tempName;
                    }
                }
            }
            Console.WriteLine("Students rank by total scores:");
            for (int i = 0; i < stdCount; i++)
            {
                string suffix = (i + 1) == 1 ? "st" : (i + 1) == 2 ? "nd" : (i + 1) == 3 ? "rd" : "th";
                Console.WriteLine($"{rankedNames[i]}: {i + 1}{suffix}");
            }
        }
    }
}


/* example output

Average Scores: 
Math: 84.40
Science: 86.80
English: 86.20

Max and min Scores: 
Math: (94, 72)
Science: (95, 76)
English: (92, 78)

Students rank by total scores:
Alice: 4th
Bob: 1st
Charlie: 5st
David: 2nd
Eve: 3rd

*/