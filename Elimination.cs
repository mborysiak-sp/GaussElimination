using System;
using System.Collections.Generic;
using System.Text;

//https://www.bragitoff.com/2018/02/gauss-elimination-c-program/
//https://www.sanfoundry.com/java-program-gaussian-elimination-algorithm/

namespace GaussElimination
{
	public class Elimination<T> : Support where T : new()
	{
		public Matrix<T> Eliminate(Matrix<T> matrix)
		{
			var results = Clone(matrix);

			for (int i = 0; i < results.Rows - 1; i++)
			{
				Console.WriteLine($"{i} / {results.Rows} DONE");
				ZeroColumn(results, i);
			}

			return GetResults(results);
		}

		private void ZeroColumn(Matrix<T> results, int i)
		{
			for (int j = i + 1; j < results.Rows; j++) 
			{
				T m = (dynamic)results.Fields[j, i] / (dynamic)results.Fields[i, i];

				for (int k = 0; k < results.Columns; k++)
					results.Fields[j, k] -= (dynamic)results.Fields[i, k] * m;
			}

		}

		public Matrix<T> EliminateWithPartialPivoting(Matrix<T> matrix)
		{
			var results = Clone(matrix);

			for (int i = 0; i < results.Rows - 1; i++)
			{
				PartialPivot(results, i);
				ZeroColumn(results, i);
			}

			return GetResults(results);
		}

		public Matrix<T> EliminateWithFullPivoting(Matrix<T> matrix)
		{
			var results = Clone(matrix);
			var swapList = new List<Tuple<int, int>>();

			for (int i = 0; i < matrix.Rows - 1; i++)
			{
				FullPivot(results, i, swapList);
				ZeroColumn(results, i);
			}

			return FixColumns(GetResults(results), swapList);
		}

		private Matrix<T> FixColumns(Matrix<T> results, List<Tuple<int, int>> swapList)
		{

			T[] order = new T[results.Rows];

			for (int i = 0; i < results.Rows; i++)
				order[i] = results.Fields[i, results.Columns - 1];

			swapList.Reverse();

			foreach (Tuple<int, int> swap in swapList)
			{
				T temp = order[swap.Item1];
				order[swap.Item1] = order[swap.Item2];
				order[swap.Item2] = temp;
			}

			for (int i = 0; i < results.Rows; i++)
				results.Fields[i, results.Columns - 1] = order[i];

			return results;
		}

		private Matrix<T> GetResults(Matrix<T> matrix)
		{
			var results = new Matrix<T>(matrix.Rows, 1);

			for (int i = matrix.Rows - 1; i >= 0; i--)
			{
				T sum = new T();

				for (int j = i + 1; j < matrix.Rows; j++)
					sum += (dynamic)matrix.Fields[i, j] * (dynamic)results.Fields[j, 0];

				results.Fields[i, 0] = ((dynamic)matrix.Fields[i, matrix.Columns - 1] - (dynamic)sum) / matrix.Fields[i, i];
			}

			return results;
		}


		private void PartialPivot(Matrix<T> matrix, int p)
		{

			for (int j = p; j < matrix.Rows; j++)
				if (Abs(typeof(T), matrix.Fields[p, p]) < Abs(typeof(T), matrix.Fields[j, p]))
					matrix.SwitchRows(p, j);
		}

		private void FullPivot(Matrix<T> matrix, int p, List<Tuple<int, int>> swapList)
		{
			for (int j = p; j < matrix.Rows; j++)
			{
				for (int k = p; k < matrix.Rows; k++)
				{
					if (Abs(typeof(T), matrix.Fields[p, p]) < Abs(typeof(T), matrix.Fields[j, k]))
					{
						matrix.SwitchColumns(p, j);
						swapList.Add(new Tuple<int, int>(p, j));
						matrix.SwitchRows(p, k);
					}
				}
			}
		}
	} 
}

