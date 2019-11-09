using System;
using System.Diagnostics;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
			var size = 500;

            var m = new Matrix<double>(size);
			m.FillMatrix();
			var n = new Matrix<double>(size);

			Elimination<double> elimination = new Elimination<double>();

			//Console.Out.WriteLine(elimination.Eliminate(m));
			//Console.Out.WriteLine(elimination.EliminateWithPartialPivoting(m));
			//Console.Out.WriteLine(elimination.EliminateWithFullPivoting(m));
			for (int i = 0; i < 10; i++)
			{
				Stopwatch stopwatch = new Stopwatch();

				stopwatch.Start();

				elimination.Eliminate(m);

				stopwatch.Stop();

				Console.WriteLine($" NONE: {stopwatch.ElapsedMilliseconds}");

				stopwatch.Reset();

				stopwatch.Start();

				elimination.EliminateWithPartialPivoting(m);

				stopwatch.Stop();

				Console.WriteLine($" PARTIAL: {stopwatch.ElapsedMilliseconds}");
			}
		}
			

	}
}
