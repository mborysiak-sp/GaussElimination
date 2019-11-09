using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Globalization;

namespace GaussElimination
{
    class Fraction
    {
        private BigInteger Denominator { get; set; }
        private BigInteger Numerator { get; set; }

        public Fraction(string numerator, string denominator)
        {
            Denominator = BigInteger.Parse(denominator);
			Numerator = BigInteger.Parse(numerator);
        }

		//private BigInteger ConvertToBigInteger(string number)
		//{
		//	var bigInt = BigInteger.Parse(number, CultureInfo.InvariantCulture);
		//	if (number.TrimStart().StartsWith("-"))
		//		return BigInteger.Negate(bigInt);
		//	else
		//		return bigInt;
		//}

        public bool Equals(Fraction obj)
        {
            if (obj.Denominator == Denominator && obj.Numerator == Numerator)
                return true;
            else return false;
        }

        public override string ToString()
        {
			return $"{Numerator}.{Denominator}";
        }

    }
}
