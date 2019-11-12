using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GaussElimination
{
    class Program
    {
		private static void ExecuteNONE(Elimination<Fraction> elimination, Matrix<Fraction> a, Matrix<Fraction> b)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();

			Task.Factory.StartNew(() => elimination.Eliminate(a.ConcatenateWithVector(b)));

			stopwatch.Stop();

			Console.WriteLine($"NONE: \n{stopwatch.Elapsed}");
		}

		private static void ExecutePARTIAL(Elimination<Fraction> elimination, Matrix<Fraction> a, Matrix<Fraction> b)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();

			Task.Factory.StartNew(() => elimination.EliminateWithPartialPivoting(a.ConcatenateWithVector(b)));

			stopwatch.Stop();

			Console.WriteLine($"PARTIAL: \n{stopwatch.Elapsed}");
		}

		private static void ExecuteFULL(Elimination<Fraction> elimination, Matrix<Fraction> a, Matrix<Fraction> b)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();

			Task.Factory.StartNew(() => elimination.EliminateWithFullPivoting(a.ConcatenateWithVector(b)));

			stopwatch.Stop();

			Console.WriteLine($"FULL: \n{stopwatch.Elapsed}");
		}
	
		static void Main(string[] args)
        {
			var size = 1000;

			var elimination = new Elimination<double>();

			var a = new Matrix<double>(size);

			a.FillMatrix();

			var x = new Matrix<double>(size, 1);

			x.FillMatrix();

			var b = a.Multiply(x);

			File.WriteAllText(@"C:\Users\marci\Tests\none_double.txt", (x.Difference(elimination.Eliminate(a.ConcatenateWithVector(b)))).ToString());
			File.WriteAllText(@"C:\Users\marci\Tests\partial_double.txt", (x.Difference(elimination.EliminateWithPartialPivoting(a.ConcatenateWithVector(b)))).ToString());
			File.WriteAllText(@"C:\Users\marci\Tests\full_double.txt", (x.Difference(elimination.EliminateWithFullPivoting(a.ConcatenateWithVector(b)))).ToString());
			//stopwatch.Stop();

			//Console.WriteLine($"FULL: \n{stopwatch.Elapsed}");



		}
			

	}
}
