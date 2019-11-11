using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Globalization;

namespace GaussElimination
{
	//http://csharphelper.com/blog/2015/06/make-a-fraction-class-in-c/
	[Serializable]
	public class Fraction
    {
        public BigInteger Denominator { get; set; }
		public BigInteger Numerator { get; set; }
		public bool Sign { get; set; }

        public Fraction(bool sign, string numerator, string denominator)
        {
			Sign = sign;
            Denominator = BigInteger.Parse(denominator);
			Numerator = BigInteger.Parse(numerator);
			Simplify();
        }

		public Fraction()
		{
			Denominator = 0;
			Numerator = 0;
		}

		public Fraction(string numerator, string denominator)
		{
			Denominator = BigInteger.Parse(denominator);
			Numerator = BigInteger.Parse(numerator);
			Simplify();
		}


		public Fraction(BigInteger numerator, BigInteger denominator)
		{
			Denominator = denominator;
			Numerator = numerator;
			Simplify();
		}

		private void Simplify()
		{
			if (Denominator < 0)
			{
				Numerator = BigInteger.Negate(Numerator);
				Denominator = BigInteger.Negate(Denominator);
			}

			BigInteger gcd = BigInteger.GreatestCommonDivisor(Numerator, Denominator);

			Numerator = Numerator / gcd;
			Denominator = Denominator / gcd;
		}
		
		public static Fraction operator +(Fraction a, Fraction b)
		{
			if(a.Numerator == 0 && b.Numerator != 0)
				return b;

			if (b.Numerator == 0 && a.Numerator != 0)
				return a;

			BigInteger gcd = BigInteger.GreatestCommonDivisor(a.Denominator, b.Denominator);

			BigInteger numerator = a.Numerator * (b.Denominator / gcd) + b.Numerator * (a.Denominator / gcd);
			BigInteger denominator = a.Denominator * (b.Denominator / gcd);

			return new Fraction(numerator, denominator);
		}

		public static Fraction operator -(Fraction a, Fraction b)
		{
			return a + -b;
		}

		public static Fraction operator -(Fraction a)
		{
			if (a.Numerator == 0)
				return a;
			else
			return new Fraction(BigInteger.Negate(a.Numerator), a.Denominator);
		}

		public static Fraction operator *(Fraction a, Fraction b)
		{
			Fraction result1 = new Fraction(a.Numerator, b.Denominator);
			Fraction result2 = new Fraction(b.Numerator, a.Denominator);

			return new Fraction(result1.Numerator * result2.Numerator, result1.Denominator * result2.Denominator);
		}

		public static bool operator >(Fraction a, Fraction b)
		{
			BigInteger gcd = BigInteger.GreatestCommonDivisor(a.Denominator, b.Denominator);

			Fraction result1 = new Fraction(a.Numerator * (b.Denominator / gcd), a.Denominator * (b.Denominator / gcd));
			Fraction result2 = new Fraction(b.Numerator * (a.Denominator / gcd), a.Denominator * (b.Denominator / gcd));

			if (result1.Numerator > result2.Numerator)
				return true;
			else return false;
		}

		public static bool operator <(Fraction a, Fraction b)
		{
			BigInteger gcd = BigInteger.GreatestCommonDivisor(a.Denominator, b.Denominator);

			Fraction result1 = new Fraction(a.Numerator * (b.Denominator / gcd), a.Denominator * (b.Denominator / gcd));
			Fraction result2 = new Fraction(b.Numerator * (a.Denominator / gcd), a.Denominator * (b.Denominator / gcd));

			if (result1.Numerator < result2.Numerator)
				return true;
			else return false;
		}

		public static bool operator ==(Fraction a, Fraction b)
		{
			BigInteger gcd = BigInteger.GreatestCommonDivisor(a.Denominator, b.Denominator);

			Fraction result1 = new Fraction(a.Numerator * (b.Denominator / gcd), a.Denominator * (b.Denominator / gcd));
			Fraction result2 = new Fraction(b.Numerator * (a.Denominator / gcd), a.Denominator * (b.Denominator / gcd));

			if (result1.Numerator == result2.Numerator)
				return true;
			else return false;
		}

		public static bool operator !=(Fraction a, Fraction b)
		{
			BigInteger gcd = BigInteger.GreatestCommonDivisor(a.Denominator, b.Denominator);

			Fraction result1 = new Fraction(a.Numerator * (b.Denominator / gcd), a.Denominator * (b.Denominator / gcd));
			Fraction result2 = new Fraction(b.Numerator * (a.Denominator / gcd), a.Denominator * (b.Denominator / gcd));

			if (result1.Numerator != result2.Numerator)
				return true;
			else return false;
		}

		public static Fraction operator /(Fraction a, Fraction b)
		{
			return a * new Fraction(b.Denominator, b.Numerator);
		}

		public bool Equals(Fraction obj)
        {
            if (obj.Denominator == Denominator && obj.Numerator == Numerator)
                return true;
            else return false;
        }

        public override string ToString()
        {
			Simplify();

			return $"{Numerator}/{Denominator}";
        }

    }
}
