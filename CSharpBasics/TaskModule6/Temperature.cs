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
    public class Temperature
    {
        private const decimal CEL_TO_FAR = 9m / 5m;
        private const decimal FAR_TO_CEL = 1 / CEL_TO_FAR;
        private const decimal SHIFT = 32m;
        private const decimal EPS = 1e-6m;

        public decimal Degree { get; }
        public bool IsCelsius { get; }
        /// <summary>
        /// Constructor for temperature class
        /// </summary>
        /// <param name="degree">Degrees</param>
        /// <param name="isCelsius">true if Celsius, false if Fahrenheit</param>
        public Temperature(decimal degree, bool isCelsius = true)
        {
            Degree = degree;
            IsCelsius = isCelsius;
        }

        public static Temperature operator +(Temperature temp1, Temperature temp2)
        {
            if (temp1.IsCelsius == temp2.IsCelsius)
                return new Temperature(temp1.Degree + temp2.Degree, isCelsius: false);
            else
                return new Temperature(ConvertToCelsius(temp1).Degree + ConvertToCelsius(temp2).Degree, isCelsius: true);
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
            if (temp1.IsCelsius == temp2.IsCelsius)
                return new Temperature(temp1.Degree - temp2.Degree, isCelsius: false);
            else
                return new Temperature(ConvertToCelsius(temp1).Degree - ConvertToCelsius(temp2).Degree, isCelsius: true);
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
            if (temp1.IsCelsius == temp2.IsCelsius)
                return new Temperature(temp1.Degree * temp2.Degree, isCelsius: false);
            else
                return new Temperature(ConvertToCelsius(temp1).Degree * ConvertToCelsius(temp2).Degree, isCelsius: true);
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
            if (temp1.IsCelsius == temp2.IsCelsius)
                return new Temperature(temp1.Degree / temp2.Degree, isCelsius: false);
            else
                return new Temperature(ConvertToCelsius(temp1).Degree / ConvertToCelsius(temp2).Degree, isCelsius: true);
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
        public static Temperature ConvertToFahrenheit(Temperature temp)
        {
            return temp.IsCelsius ? new Temperature(CEL_TO_FAR * temp.Degree + SHIFT, isCelsius: false) : temp;
        }
        
        /// <summary>
        /// Conversion from fahrenheit to celsius
        /// </summary>
        /// <param name="temp">temperature</param>
        /// <returns>temperature in celsius</returns>
        public static Temperature ConvertToCelsius(Temperature temp)
        {
            return temp.IsCelsius ? temp : new Temperature(FAR_TO_CEL * (temp.Degree - SHIFT), isCelsius: true);
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            string type = IsCelsius ? "Celsius" : "Fahrenheit";
            return $"{Degree:N1} degrees {type}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Temperature temp)
            {
                return Math.Abs(ConvertToCelsius(temp).Degree - ConvertToCelsius(this).Degree) <= EPS;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ConvertToCelsius(this).Degree.GetHashCode();
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
            return ConvertToCelsius(temp1).Degree - ConvertToCelsius(temp2).Degree > EPS;
        }

        public static bool operator <(Temperature temp1, Temperature temp2)
        {
            return ConvertToCelsius(temp2).Degree - ConvertToCelsius(temp1).Degree > EPS;
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
