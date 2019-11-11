using System;
using System.Diagnostics;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
			var size = 4;

            //var a = new Matrix<Fraction>(size);
			//a.FillMatrix();
			//var x = new Matrix<Fraction>(size, 1);
			//x.FillMatrix();
			//var b = a.Multiply(x);

			var elimination = new Elimination<Fraction>();
			//Console.WriteLine($"a: \n {a}");
			//Console.WriteLine($"x: \n {x}");
			//Console.WriteLine($"b: \n {b}");

			//Console.WriteLine($"Simple: \n {elimination.Eliminate(a.ConcatenateWithVector(b))}");
			//Console.Out.WriteLine(elimination.Eliminate(a.ConcatenateWithVector(b)));
			//Console.WriteLine($"Pivot: \n {elimination.EliminateWithPartialPivoting(a.ConcatenateWithVector(b))}");
			//Console.Out.WriteLine(elimination.EliminateWithPartialPivoting(a.ConcatenateWithVector(a.Multiply(x))));
			//Console.Out.WriteLine(elimination.EliminateWithFullPivoting(m));

			for (int i = 0; i < 1; i++)
			{
				var a = new Matrix<Fraction>(size);
				a.FillMatrix();
				var x = new Matrix<Fraction>(size, 1);
				x.FillMatrix();
				var b = a.Multiply(x);
				Console.WriteLine(x);
				Stopwatch stopwatch = new Stopwatch();

				stopwatch.Start();

				Console.WriteLine(x.Difference(elimination.Eliminate(a.ConcatenateWithVector(b))));

				stopwatch.Stop();

				Console.WriteLine($"NONE: \n{stopwatch.Elapsed}");

				//Console.WriteLine($" NONE: {elimination.EliminateWithPartialPivoting(a.ConcatenateWithVector(b))}");

				stopwatch.Reset();

				stopwatch.Start();

				Console.WriteLine(x.Difference(elimination.EliminateWithPartialPivoting(a.ConcatenateWithVector(b))));

				stopwatch.Stop();

				Console.WriteLine($"PARTIAL: \n{stopwatch.Elapsed}");
				//Console.WriteLine($" PARTIAL: {stopwatch.ElapsedMilliseconds}");
			}
		}
			

	}
}
