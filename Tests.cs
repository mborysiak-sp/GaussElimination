using GaussElimination;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GaussianElimination
{
	public class Tests<T> : Support where T : new()
	{

		public Tests() { }

		public void ExecuteNONE(Elimination<T> elimination, Matrix<T> x, Matrix<T> a, Matrix<T> b)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();

			Matrix<T> result = elimination.Eliminate(a.ConcatenateWithVector(b));

			stopwatch.Stop();

			File.WriteAllText(@$"C:\Users\marci\Tests\none_{typeof(T)}{x.Rows}.txt", ("NONE: \nTime:" + stopwatch.Elapsed));
		}

		public void ExecutePARTIAL(Elimination<T> elimination, Matrix<T> x, Matrix<T> a, Matrix<T> b)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();

			Matrix<T> result = elimination.EliminateWithPartialPivoting(a.ConcatenateWithVector(b));

			stopwatch.Stop();

			File.WriteAllText(@$"C:\Users\marci\Tests\partial_{typeof(T)}{x.Rows}.txt", ("PARTIAL: \nTime:" + stopwatch.Elapsed));

		}

		public void ExecuteFULL(Elimination<T> elimination, Matrix<T> x, Matrix<T> a, Matrix<T> b)
		{
			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();

			Matrix<T> result = elimination.EliminateWithFullPivoting(a.ConcatenateWithVector(b));

			stopwatch.Stop();

			File.WriteAllText(@$"C:\Users\marci\Tests\full_{typeof(T)}{x.Rows}.txt", ("FULL: \nTime:" + stopwatch.Elapsed));
		}
	}
}
