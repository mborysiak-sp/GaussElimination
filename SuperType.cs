using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Globalization;

namespace Matrices
{
    class SuperType
    {
        private BigInteger Denominator { get; set; }
        private float Numerator { get; set; }

        public SuperType(string numerator, string denominator)
        {
            Denominator = BigInteger.Parse(denominator);
			Numerator = float.Parse(numerator);
        }

		//private BigInteger ConvertToBigInteger(string number)
		//{
		//	var bigInt = BigInteger.Parse(number, CultureInfo.InvariantCulture);
		//	if (number.TrimStart().StartsWith("-"))
		//		return BigInteger.Negate(bigInt);
		//	else
		//		return bigInt;
		//}

        public bool Equals(SuperType obj)
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
