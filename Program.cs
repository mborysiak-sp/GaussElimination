using GaussianElimination;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

//https://stackoverflow.com/questions/7320491/simplest-way-to-run-three-methods-in-parallel-in-c-sharp

namespace GaussElimination
{
    class Program
    {
		static void Main(string[] args)
        {
			var size = 500;

			var eliminationFraction = new Elimination<Fraction>();

			var a = new Matrix<Fraction>(size);

			a.FillMatrix();

			var x = new Matrix<Fraction>(size, 1);

			x.FillMatrix();

			var b = a.Multiply(x);

			var eliminationFloat = new Elimination<float>();

			var aFloat = new Matrix<float>(size);

			var xFloat = new Matrix<float>(size, 1);

			var eliminationDouble = new Elimination<double>();

			var aDouble = new Matrix<double>(size);

			var xDouble = new Matrix<double>(size, 1);

			for (int i = 0; i < a.Rows; i++)
			{
				for (int j = 0; j < a.Columns; j++)
				{
					aFloat.Fields[i, j] = (float)a.Fields[i, j];
					aDouble.Fields[i, j] = (double)a.Fields[i, j];
				}
			}

			for (int i = 0; i < x.Rows; i++)
			{
				for (int j = 0; j < x.Columns; j++)
				{
					xFloat.Fields[i, j] = (float)x.Fields[i, j];
					xDouble.Fields[i, j] = (double)x.Fields[i, j];
				}
			}

			var bFloat = aFloat.Multiply(xFloat);

			var bDouble = aDouble.Multiply(xDouble);

			var testsFraction = new Tests<Fraction>();
			var testsFloat= new Tests<float>();
			var testsDouble = new Tests<double>();

			Parallel.Invoke(
				() => testsFraction.ExecuteNONE(eliminationFraction, x, a, b),
				() => testsFraction.ExecutePARTIAL(eliminationFraction, x, a, b),
				() => testsFraction.ExecuteFULL(eliminationFraction, x, a, b),
				() => testsFloat.ExecuteNONE(eliminationFloat, xFloat, aFloat, bFloat),
				() => testsFloat.ExecutePARTIAL(eliminationFloat, xFloat, aFloat, bFloat),
				() => testsFloat.ExecuteFULL(eliminationFloat, xFloat, aFloat, bFloat),
				() => testsDouble.ExecuteNONE(eliminationDouble, xDouble, aDouble, bDouble),
				() => testsDouble.ExecutePARTIAL(eliminationDouble, xDouble, aDouble, bDouble),
				() => testsDouble.ExecuteFULL(eliminationDouble, xDouble, aDouble, bDouble));
		}
	}
}
