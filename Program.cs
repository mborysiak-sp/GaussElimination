using System;

namespace Matrices
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = new Matrix<double>(2, 2);
			var n = new Matrix<double>(2, 2);
			m.MultiplyMatrix(n);
			var x = new Matrix<float>(2, 10);
			Console.WriteLine(x.ToString());
			var d = new Matrix<float>(10, 2);
			Console.WriteLine(d.ToString());
			var xd = x.MultiplyMatrix(d);
			Console.WriteLine(xd.ToString());
		}

	}
}
