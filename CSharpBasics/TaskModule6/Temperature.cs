using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule6
{
    /// <summary>
    /// This class allows you to work with the temperature in celsius or fahrenheit degrees. Celsius degrees are used by default. 
    /// Supports conversion from celsius to fahrenheit and back and basic operators.
    /// </summary>
    class Temperature
    {
        private const decimal CEL_TO_FAR = 9m / 5m;
        private const decimal FAR_TO_CEL = 5m / 9m;
        private const decimal SHIFT = 32m;
        private const decimal EXP = 1e-6m;

        decimal Degree { get; set; }
        bool IsCelsius { get; set; }

        public Temperature(decimal degree, bool isCelsius)
        {
            this.Degree = degree;
            this.IsCelsius = isCelsius;
        }

        public Temperature(int degree, bool isCelsius)
        {
            this.Degree = degree;
            this.IsCelsius = isCelsius;
        }

        public Temperature(double degree, bool isCelsius) : this((decimal)degree, isCelsius) { }

        public Temperature(decimal degree) : this(degree, isCelsius: true) { }

        public Temperature(double degree) : this((decimal)degree, isCelsius: true) { }

        public Temperature(int degree) : this(degree, isCelsius: true) { }

        public static Temperature operator +(Temperature temp1, Temperature temp2)
        {
            if (!temp1.IsCelsius && !temp2.IsCelsius)
                return new Temperature(temp1.Degree + temp2.Degree, isCelsius: false);
            else
                return new Temperature(ConvertToC(temp1).Degree + ConvertToC(temp2).Degree, isCelsius: true);
        }

        public static Temperature operator +(Temperature temp, int change)
        {
            return new Temperature(temp.Degree + change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator +(int change, Temperature temp)
        {
            return new Temperature(temp.Degree + change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator +(Temperature temp, decimal change)
        {
            return new Temperature(temp.Degree + change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator +(decimal change, Temperature temp)
        {
            return new Temperature(temp.Degree + change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator -(Temperature temp1, Temperature temp2)
        {
            if (!temp1.IsCelsius && !temp2.IsCelsius)
                return new Temperature(temp1.Degree - temp2.Degree, isCelsius: false);
            else
                return new Temperature(ConvertToC(temp1).Degree - ConvertToC(temp2).Degree, isCelsius: true);
        }

        public static Temperature operator -(Temperature temp, int change)
        {
            return new Temperature(temp.Degree - change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator -(int change, Temperature temp)
        {
            return new Temperature(change - temp.Degree, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator -(Temperature temp, decimal change)
        {
            return new Temperature(temp.Degree - change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator -(decimal change, Temperature temp)
        {
            return new Temperature(change - temp.Degree, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator *(Temperature temp1, Temperature temp2)
        {
            if (!temp1.IsCelsius && !temp2.IsCelsius)
                return new Temperature(temp1.Degree * temp2.Degree, isCelsius: false);
            else
                return new Temperature(ConvertToC(temp1).Degree * ConvertToC(temp2).Degree, isCelsius: true);
        }

        public static Temperature operator *(Temperature temp, int change)
        {
            return new Temperature(temp.Degree * change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator *(int change, Temperature temp)
        {
            return new Temperature(temp.Degree * change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator *(Temperature temp, decimal change)
        {
            return new Temperature(temp.Degree * change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator *(decimal change, Temperature temp)
        {
            return new Temperature(temp.Degree * change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator /(Temperature temp1, Temperature temp2)
        {
            if (!temp1.IsCelsius && !temp2.IsCelsius)
                return new Temperature(temp1.Degree / temp2.Degree, isCelsius: false);
            else
                return new Temperature(ConvertToC(temp1).Degree / ConvertToC(temp2).Degree, isCelsius: true);
        }

        public static Temperature operator /(Temperature temp, int change)
        {
            return new Temperature(temp.Degree / change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator /(int change, Temperature temp)
        {
            return new Temperature(change / temp.Degree, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator /(Temperature temp, decimal change)
        {
            return new Temperature(temp.Degree / change, isCelsius: temp.IsCelsius);
        }

        public static Temperature operator /(decimal change, Temperature temp)
        {
            return new Temperature(change / temp.Degree, isCelsius: temp.IsCelsius);
        }

        /// <summary>
        /// Conversion from celsius to fahrenheit
        /// </summary>
        /// <param name="temp">temperature</param>
        /// <returns>temperature in fahrenheit</returns>
        public static Temperature ConvertToF(Temperature temp)
        {
            return temp.IsCelsius ? new Temperature(CEL_TO_FAR * temp.Degree + SHIFT, isCelsius: false) : temp;
        }
        
        /// <summary>
        /// Conversion from fahrenheit to celsius
        /// </summary>
        /// <param name="temp">temperature</param>
        /// <returns>temperature in celsius</returns>
        public static Temperature ConvertToC(Temperature temp)
        {
            return temp.IsCelsius ? temp : new Temperature(FAR_TO_CEL * (temp.Degree - SHIFT), isCelsius: true);
        }

        public override string ToString()
        {
            string type = this.IsCelsius ? "Celsius" : "Fahrenheit";
            return string.Format("{0:N1} degrees {1}", this.Degree, type);
        }

        public override bool Equals(object obj)
        {
            if (obj is Temperature)
                return Math.Abs(ConvertToC((Temperature)obj).Degree - ConvertToC(this).Degree) <= EXP;
            return false;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator ==(Temperature temp1, Temperature temp2)
        {
            return temp1.Equals(temp2);
        }

        public static bool operator !=(Temperature temp1, Temperature temp2)
        {
            return !temp1.Equals(temp2);
        }

        public static bool operator >(Temperature temp1, Temperature temp2)
        {
            return ConvertToC(temp1).Degree - ConvertToC(temp2).Degree > EXP;
        }

        public static bool operator <(Temperature temp1, Temperature temp2)
        {
            return ConvertToC(temp2).Degree - ConvertToC(temp1).Degree > EXP;
        }

        public static bool operator >=(Temperature temp1, Temperature temp2)
        {
            return temp1.Equals(temp2) || temp1 > temp2;
        }

        public static bool operator <=(Temperature temp1, Temperature temp2)
        {
            return temp1.Equals(temp2) || temp1 < temp2;
        }
    }
}
