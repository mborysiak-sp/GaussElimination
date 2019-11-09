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
		public int Size { get; set; }

		public Matrix(int size)
		{
			Size = size;
			Fields = new T[Size, Size];
		}
		
		public void FillMatrix()
		{
			for (int i = 0; i < Size; i++)
				for (int j = 0; j < Size; j++)
					Fields[i, j] = GetRandomNumber(typeof(T));
		}

		public Matrix<T> MultiplyMatrix(Matrix<T> matrix)
		{
			Matrix<T> result = new Matrix<T>(Size);

			dynamic sum;

			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size; j++)
				{
					sum = 0;

					for (int k = 0; k < Size; k++)
						sum += (dynamic)Fields[i, k] * (dynamic)matrix.Fields[k, j];

					result.Fields[i, j] = sum;
				}
			}
			return result;
		}

		public bool Equals(Matrix<T> matrix)
		{
			if (Size != matrix.Size)
				return false;
			else
				for (int i = 0; i < Size; i++)
					for (int j = 0; j < Size; j++)
						if ((dynamic)Fields[i, j] != (dynamic)matrix.Fields[i, j])
							return false;
			return true;
		}

		public override string ToString()
		{
			string result = "";

			for (int i = 0; i < Size; i++)
			{
				for (int j = 0; j < Size; j++)
					result += $"{Fields[i, j]} \t";

				result += "\n";
			}

			return result;
		}
	}
}
