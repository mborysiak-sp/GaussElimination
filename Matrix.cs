using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Matrices
{
	class Matrix<T>
	{
		private T[,] Fields { get; set; }
		private int Columns { get; set; }
		private int Rows { get; set; }

		public Matrix(int rows, int columns)
		{
			Columns = columns;
			Rows = rows;
			Fields = new T[rows, columns];
			FillMatrix();
		}

		private void FillMatrix()
		{
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Columns; j++)
					Fields[i, j] = GetRandomNumber();
		}

		public Matrix<T> MultiplyMatrix(Matrix<T> matrix)
		{
			Matrix<T> result = new Matrix<T>(Rows, matrix.Columns);

			dynamic sum;

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < matrix.Columns; j++)
				{
					sum = 0;

					for (int k = 0; k < Rows; k++)
						sum += (dynamic)Fields[i, k] * (dynamic)matrix.Fields[k, j];

					result.Fields[i, j] = sum;
				}
			}
			return result;
		}


		private dynamic GetRandomR()
		{
			double minimum = -Math.Pow(2, 16);
			double maximum = Math.Pow(2, 16) - 1;

			Random rand = new Random();

			return rand.NextDouble() * (maximum - minimum) + minimum;
		}

		private T GetRandomNumber()
		{
			Random rand = new Random();
			if (typeof(T) == typeof(double))
				return (T)(object)(GetRandomR() / Math.Pow(2, 16));

			if (typeof(T) == typeof(float))
				return (T)(object)Convert.ToSingle(GetRandomR() / Math.Pow(2, 16));

			//        if (typeof(T) == typeof(SuperType))
			//        {
			//            var values = (GetRandomR() / Math.Pow(2, 16)).ToString().Split('.');
			//            var obj = (T)(object)new SuperType(values[0], values[1]);
			//return obj;
			//        }

			else return default;
		}

		public bool Equals(Matrix<T> matrix)
		{
			if (Rows != matrix.Rows || Columns != matrix.Columns)
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
