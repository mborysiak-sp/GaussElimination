using System;
using System.Collections.Generic;
using System.Text;

namespace GaussElimination
{
	class Elimination<T> : Support where T : new()
	{
		public Matrix<T> Eliminate(Matrix<T> matrix)
		{
			var results = Clone(matrix);

			for (int i = 0; i < results.Rows - 1; i++)
				ZeroColumn(results, i);

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
				for (int j = i; j < results.Rows; j++)
					if (Abs(typeof(T), results.Fields[i, i]) < Abs(typeof(T), results.Fields[j, i]))
						results.SwitchRows(i, j);

				ZeroColumn(results, i);
			}

			return GetResults(results);
		}

		public Matrix<T> EliminateWithFullPivoting(Matrix<T> matrix)
		{
			var results = Clone(matrix);
			var swapList = new List<Tuple<int, int>>();

			for (int i = 0; i < results.Rows; i++)
			{
				for (int j = i; j < results.Rows; j++)
				{
					for (int k = i; k < results.Columns - 1; k++)
					{
						if (Abs(typeof(T), results.Fields[i, i]) < Abs(typeof(T), results.Fields[j, k]))
						{
							//Console.WriteLine(results);

							//Console.WriteLine($"{results.Fields[i, i]} < {results.Fields[j, k]}");

							//Console.WriteLine($"Switching columns {i},{k}");

							swapList.Add(new Tuple<int, int>(i, k));

							results.SwitchColumns(i, k);

							//Console.WriteLine($"Switching rows {i},{j}");

							results.SwitchRows(i, j);

							//Console.WriteLine(results);
						}
					}
				}
	
				ZeroColumn(results, i);
			}

			return GetResults(FixColumns(results, swapList));
		}

		//https://www.bragitoff.com/2018/02/gauss-elimination-c-program/
		//https://www.sanfoundry.com/java-program-gaussian-elimination-algorithm/

		private Matrix<T> FixColumns(Matrix<T> matrix, List<Tuple<int, int>> swapList)
		{
			T[] outArr = new T[matrix.Rows];

			for (int i = 0; i < matrix.Rows; i++)
				outArr[i] = matrix.Fields[i, matrix.Columns - 1];

			swapList.Reverse();
			foreach (Tuple<int, int> swap in swapList)
			{
				T temp = outArr[swap.Item1];
				outArr[swap.Item1] = outArr[swap.Item2];
				outArr[swap.Item2] = temp;
			}

			for (int i = 0; i < matrix.Rows; i++)
				matrix.Fields[i, matrix.Columns - 1] = outArr[i];

			return matrix;
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


		private int GetPartialPivot(Matrix<T> matrix)
		{
			T max = Abs(typeof(T), matrix.Fields[0, 0]);

			int position = 0;

			for (int i = 1; i < matrix.Rows; i++)
			{
				if (Abs(typeof(T), matrix.Fields[i, 0]) > max)
				{
					max = Abs(typeof(T), matrix.Fields[i, 0]);

					position = i;
				}
			}

			return position;
		}

		private Tuple<int, int> GetFullPivot(Matrix<T> matrix)
		{
			T max = Math.Abs((dynamic)matrix.Fields[0, 0]);

			int positionI = 0, positionJ = 0;

			for (int i = 0; i < matrix.Rows; i++)
			{
				for (int j = 0; j < matrix.Columns; j++)
				{
					if (Math.Abs((dynamic)matrix.Fields[i, j]) > max)
					{
						max = Math.Abs((dynamic)matrix.Fields[i, j]);

						positionI = i;
						positionJ = j;
					}
				}
			}

			return new Tuple<int, int>(positionI, positionJ);
		}

		private void ReductRows(Matrix<T> matrix, int row1, int row2)
		{
			for (int i = 0; i < matrix.Columns; i++)
			{
				T m = (dynamic)matrix.Fields[row2, row2] / (dynamic)matrix.Fields[row2, row1];

				matrix.Fields[row2, i] -= matrix.Fields[row1, i] * (dynamic)m;
			}
		}
	}
}
