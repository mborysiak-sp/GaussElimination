using System;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
			var size = 4;

            var m = new Matrix<double>(size);
			m.FillMatrix();
			var n = new Matrix<double>(size);

			Elimination<double> elimination = new Elimination<double>();

			Console.Out.WriteLine(elimination.Eliminate(m));
			Console.Out.WriteLine(elimination.EliminateWithPartialPivoting(m));
			Console.Out.WriteLine(elimination.EliminateWithFullPivoting(m));
		}

	}
}
