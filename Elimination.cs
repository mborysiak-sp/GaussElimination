using System;
using System.Collections.Generic;
using System.Text;

namespace GaussElimination
{
	class Elimination<T> : Support
	{
		public Matrix<T> Eliminate(Matrix<T> matrix)
		{
			var results = Clone(matrix);

			for (int i = 0; i < results.Size; i++)
			{
				for (int j = 0; j < results.Size; j++)
				{
					if (i > j)
					{
						T m = (dynamic)results.Fields[i, j] / (dynamic)results.Fields[j, j];

						for (int k = 0; k < results.Size; k++)
						{
							results.Fields[i, k] -= (dynamic)results.Fields[j, k] * m;
						}
					}
				}
			}

			return results;
		}
		
		public Matrix<T> EliminateWithPartialPivoting(Matrix<T> matrix)
		{
			var results = Clone(matrix);

			for (int i = 0; i < results.Size; i++)
			{
				SwitchRows(results, i, GetPartialPivot(results));

				for (int j = 0; j < results.Size; j++)
				{
					if (i > j)
					{
						T m = (dynamic)results.Fields[i, j] / (dynamic)results.Fields[j, j];

						for (int k = 0; k < results.Size; k++)
						{
							results.Fields[i, k] -= (dynamic)results.Fields[j, k] * m;
						}
					}
				}
			}

			return results;
		}

		public Matrix<T> EliminateWithFullPivoting(Matrix<T> matrix)
		{
			var results = Clone(matrix);

			for (int i = 0; i < results.Size; i++)
			{

				int positionI = GetFullPivot(results).Item1;
				int positionJ = GetFullPivot(results).Item2;

				SwitchRows(results, i, positionI);
				SwitchColumns(results, i, positionJ);

				for (int j = 0; j < results.Size; j++)
				{
					if (i > j)
					{
						T m = (dynamic)results.Fields[i, j] / (dynamic)results.Fields[j, j];

						for (int k = 0; k < results.Size; k++)
						{
							results.Fields[i, k] -= (dynamic)results.Fields[j, k] * m;
						}
					}
				}
			}

			return results;
		}
		private void SwitchRows(Matrix<T> matrix, int row1, int row2)
		{
			for (int i = 0; i < matrix.Size; i++)
			{
				T temp;

				temp = matrix.Fields[row1, i];

				matrix.Fields[row1, i] = matrix.Fields[row2, i];

				matrix.Fields[row2, i] = temp;
			}
		}
		
		private void SwitchColumns(Matrix<T> matrix, int col1, int col2)
		{
			for (int i = 0; i < matrix.Size; i++)
			{
				T temp;

				temp = matrix.Fields[col1, i];

				matrix.Fields[col1, i] = matrix.Fields[col2, i];

				matrix.Fields[col2, i] = temp;
			}
		}

		private int GetPartialPivot(Matrix<T> matrix)
		{
			T max = Math.Abs((dynamic)matrix.Fields[0, 0]);

			int position = 0;

			for (int i = 1; i < matrix.Size; i++)
			{
				if (Math.Abs((dynamic)matrix.Fields[i, 0]) > max)
				{
					max = Math.Abs((dynamic)matrix.Fields[i, 0]);

					position = i;
				}
			}

			return position;
		}

		private Tuple<int, int> GetFullPivot(Matrix<T> matrix)
		{
			T max = Math.Abs((dynamic)matrix.Fields[0, 0]);

			int positionI = 0, positionJ = 0; 

			for (int i = 0; i < matrix.Size; i++)
			{
				for(int j = 0; j < matrix.Size; j++)
				{
					if (Math.Abs((dynamic)matrix.Fields[i, j]) > max)
					{
						max = Math.Abs((dynamic)matrix.Fields[i, j]);

						positionI = i;
						positionJ = j;
					}
				}
			}

			return new Tuple<int,int> (positionI, positionJ);
		}

		private void ReductRows(Matrix<T> matrix, int row1, int row2)
		{
			for (int i = 0; i < matrix.Size; i++)
			{
				T m = (dynamic) matrix.Fields[row2, row2] / (dynamic) matrix.Fields[row2, row1];

				matrix.Fields[row2, i] -= matrix.Fields[row1, i] * (dynamic) m;
			}
		}
	}
}
