using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GaussElimination
{
	[Serializable]
	public class Support
	{
		protected dynamic Abs(Type type, dynamic value)
		{
			if (type == typeof(double))
				return Math.Abs(value);

			if (type == typeof(float))
				return Math.Abs(value);

			if (type == typeof(Fraction))
				return GetRandomFraction();

			else return null;
		}

		private Fraction AbsForFraction(Fraction value)
		{
			if (value.Numerator < 0)
				return new Fraction(BigInteger.Negate(value.Numerator), value.Denominator);
			else
				return value;
		}

		private int GetRandomR()
		{
			int minimum = (int)Math.Round(-Math.Pow(2, 16));
			int maximum = (int)Math.Round(Math.Pow(2, 16) - 1);

			Random rand = new Random();

			return rand.Next(minimum, maximum);
		}

		protected double GetRandomDouble()
		{
			return GetRandomR() / Math.Pow(2, 16);
		}

		protected Fraction GetRandomFraction()
		{
			return new Fraction(GetRandomR().ToString(), Math.Pow(2, 16).ToString());
		}

		protected float GetRandomFloat()
		{
			return Convert.ToSingle(GetRandomR() / Math.Pow(2, 16));
		}

		protected dynamic GetRandomNumber(Type type)
		{
			if (type == typeof(double))
				return GetRandomDouble();

			if (type == typeof(float))
				return GetRandomFloat();

			if (type == typeof(Fraction))
				return GetRandomFraction();

			else return null;
		}

		//https://www.codeproject.com/Articles/23832/Implementing-Deep-Cloning-via-Serializing-objects
		public static T Clone<T>(T source)
		{
			if (!typeof(T).IsSerializable)
			{
				throw new ArgumentException("The type must be serializable.", nameof(source));
			}

			// Don't serialize a null object, simply return the default for that object
			if (Object.ReferenceEquals(source, null))
			{
				return default(T);
			}

			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();
			using (stream)
			{
				formatter.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}
	}
}
