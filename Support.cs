using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GaussElimination
{
	[Serializable]
	public class Support
	{
		private double GetRandomR()
		{
			double minimum = -Math.Pow(2, 16);
			double maximum = Math.Pow(2, 16) - 1;

			Random rand = new Random();

			return rand.NextDouble() * (maximum - minimum) + minimum;
		}

		protected double GetRandomDouble()
		{
			return GetRandomR() / Math.Pow(2, 16);
		}

		//protected Fraction GetRandomFraction()
		//{
		//	Random rand = new Random();

		//}

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
