using System;
using System.Diagnostics;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
			var size = 3;

            var a = new Matrix<double>(size);
			a.FillMatrix();
			var x = new Matrix<double>(size, 1);
			x.FillMatrix();

			Elimination<double> elimination = new Elimination<double>();

			Console.WriteLine($"x: \n {x}");

			Console.WriteLine($"Simple: \n {elimination.Eliminate(a.ConcatenateWithVector(a.Multiply(x)))}");
			//Console.Out.WriteLine(elimination.Eliminate(a.ConcatenateWithVector(a.Multiply(x))));
			Console.WriteLine($"Pivot: \n {elimination.EliminateWithPartialPivoting(a.ConcatenateWithVector(a.Multiply(x)))}");
			//Console.Out.WriteLine(elimination.EliminateWithPartialPivoting(a.ConcatenateWithVector(a.Multiply(x))));
			//Console.Out.WriteLine(elimination.EliminateWithFullPivoting(m));

			//for (int i = 0; i < 10; i++)
			//{
			//	Stopwatch stopwatch = new Stopwatch();

			//	stopwatch.Start();

			//	elimination.Eliminate(m);

			//	stopwatch.Stop();

			//	Console.WriteLine($" NONE: {stopwatch.ElapsedMilliseconds}");

			//	stopwatch.Reset();

			//	stopwatch.Start();

			//	elimination.EliminateWithPartialPivoting(m);

			//	stopwatch.Stop();

			//	Console.WriteLine($" PARTIAL: {stopwatch.ElapsedMilliseconds}");
			//}
		}
			

	}
}
