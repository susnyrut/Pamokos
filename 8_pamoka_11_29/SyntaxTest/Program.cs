using System;

namespace SyntaxTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ok = RunExercises();

            if (ok)
            {
                Console.WriteLine("Congratulations, you have finished all exercises!");
            }
            else
            {
                Console.WriteLine("Please fix remaining exercises.");
            }
            Console.ReadKey();
        }

        private static bool RunExercises()
        {
            for (int i = 1; i <= 11; i++)
            {
                bool ok = RunExercise(i);
                if (!ok)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool RunExercise(int i)
        {
            string exerciseNo = $"{i:d2}";
            Console.WriteLine($"Exercise {exerciseNo}:");
            var actualType = typeof(Program).Assembly.GetType("SyntaxTest.Exercises.Exercise" + exerciseNo);
            bool ok = BasicTest.Engine.TypeComparer.Check(actualType, i);
            Console.WriteLine();
            return ok;
        }
    }
}
