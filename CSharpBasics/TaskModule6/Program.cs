/*Task: 
 *Create a Temperature class that supports conversion from celsius to fahrenheit and back. 
 *Also your type must support basic operators such as +,-,/,* by your type, int and decimal. 
 *Also provide correct equality comparison operators.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule6
{
    class Program
    {
        static void Main(string[] args)
        {
            //test
            Temperature temp1 = new Temperature(15);
            Temperature temp2 = new Temperature(-12.5);
            Temperature temp3 = new Temperature(12m, isCelsius: false);
            Temperature temp4 = new Temperature(48, isCelsius: false);

            Console.WriteLine(temp2);
            Console.WriteLine(temp3);
            Console.WriteLine(temp1 + temp2);
            Console.WriteLine(temp2 - temp3);
            Console.WriteLine(temp1 * 3);
            Console.WriteLine(temp4 / 6.5m);

            Console.WriteLine(Temperature.ConvertToC(temp4));
            Console.WriteLine(Temperature.ConvertToF(temp1));
            Console.WriteLine(Temperature.ConvertToC(temp1));

            Temperature temp5 = temp1;
            Temperature temp6 = temp4;
            Console.WriteLine(temp1 > temp2);
            Console.WriteLine(temp2 < temp1);
            Console.WriteLine(temp1 == temp5);
            Console.WriteLine(temp1 == temp3);
            Console.WriteLine(temp1 <= temp4);
            Console.WriteLine(temp1 == Temperature.ConvertToF(temp1));
            Console.WriteLine(temp4 == temp6);
            Console.WriteLine(temp1.GetHashCode() == temp5.GetHashCode());


            Console.ReadLine();
        }
    }
}
