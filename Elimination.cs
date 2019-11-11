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
			{
				//Console.WriteLine(i);

				for (int j = i + 1; j < results.Rows; j++)
				{
					T m = (dynamic)results.Fields[j, i] / (dynamic)results.Fields[i, i];

					for (int k = 0; k < results.Columns; k++)
						results.Fields[j, k] -= (dynamic)results.Fields[i, k] * m;
				}
				//Console.WriteLine(results);
			}
			return GetResults(results);
		}

		public Matrix<T> EliminateWithPartialPivoting(Matrix<T> matrix)
		{
			var results = Clone(matrix);

			for (int i = 0; i < results.Rows - 1; i++)
			{
				//Console.WriteLine(i);

				for (int k = i + 1; k < results.Rows; k++)
					if (Abs(typeof(T), results.Fields[i, i]) < Abs(typeof(T), results.Fields[k, i]))
						results.SwitchRows(k, i);

				for (int j = i + 1; j < results.Rows; j++)
				{
					T m = (dynamic)results.Fields[j, i] / (dynamic)results.Fields[i, i];

					for (int k = 0; k < results.Columns; k++)
						results.Fields[j, k] -= (dynamic)results.Fields[i, k] * m;
				}
				//Console.WriteLine(results);
			}

			return GetResults(results); ;
		}

		public Matrix<T> EliminateWithFullPivoting(Matrix<T> matrix)
		{
			var results = Clone(matrix);

			for (int i = 0; i < results.Rows; i++)
			{

				int positionI = GetFullPivot(results).Item1;
				int positionJ = GetFullPivot(results).Item2;

				//SwitchRows(results, i, positionI);
				//SwitchColumns(results, i, positionJ);

				for (int j = 0; j < results.Columns; j++)
				{
					if (i > j)
					{
						T m = (dynamic)results.Fields[i, j] / (dynamic)results.Fields[j, j];

						for (int k = 0; k < results.Rows; k++)
						{
							results.Fields[i, k] -= (dynamic)results.Fields[j, k] * m;
						}
					}
				}
			}

			return results;
		}

		//https://www.bragitoff.com/2018/02/gauss-elimination-c-program/
		//https://www.sanfoundry.com/java-program-gaussian-elimination-algorithm/
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
