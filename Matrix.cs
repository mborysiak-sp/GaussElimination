using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace GaussElimination
{
	[Serializable]
	public class Matrix<T> : Support
	{

		public T[,] Fields { get ; set; }
		public int Rows { get; set; }
		public int Columns { get; set; }

		public Matrix(int size)
		{
			Rows = size;
			Columns = size;
			Fields = new T[Rows, Columns];
		}

		public Matrix(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
			Fields = new T[Rows, Columns];
		}
		
		public void FillMatrix()
		{
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Columns; j++)
					Fields[i, j] = GetRandomNumber(typeof(T));
		}

		public Matrix<T> Multiply(Matrix<T> matrix)
		{
			Matrix<T> results = new Matrix<T>(Rows, matrix.Columns);

			dynamic sum;

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < matrix.Columns; j++)
				{
					sum = 0;

					for (int k = 0; k < matrix.Rows; k++)
						sum += (dynamic)Fields[i, k] * (dynamic)matrix.Fields[k, j];

					results.Fields[i, j] = sum;
				}
			}
			return results;
		}

		public Matrix<T> ConcatenateWithVector(Matrix<T> vector)
		{
			var result = new Matrix<T>(Rows, Columns + 1);

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns + 1; j++)
				{
					if (j == Columns)
						result.Fields[i, j] = vector.Fields[i, 0];
					else
						result.Fields[i, j] = Fields[i, j];
				}
			}

			return result;
		}

		public bool Equals(Matrix<T> matrix)
		{
			if (Rows != matrix.Rows && Columns != matrix.Columns)
				return false;
			else
				for (int i = 0; i < Rows; i++)
					for (int j = 0; j < Columns; j++)
						if ((dynamic)Fields[i, j] != (dynamic)matrix.Fields[i, j])
							return false;
			return true;
		}

		public override string ToString()
		{
			string result = "";

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
					result += $"{Fields[i, j]} \t";

				result += "\n";
			}

			return result;
		}
	}
}
