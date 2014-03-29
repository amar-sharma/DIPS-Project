using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DIPS.Calculator
{
    class Operations
    {
        private static double result; // create result

        public Operations()
        {
            result = 0; // initialize result to '0'
        }

        // addition function
        public static void add(double num)
        {
            if(result == 0)
            {
                result = num; // save first number in result
            }
            else
                 result += num; // adding second. third.... numbers to result
        }

        // subtraction function
        public static void sub(double num)
        {
            if(result == 0)
            {
                result = num; // save first number in result
            }
            else
            {
                result -= num;
            }
        }
 
        // starting with minus ex: -3-3
        public static void Ssub(double num)
        {
            if(result == 0)
            {
                result = num; // save first number in result
            }
            else
            {
                result -= num;
            }
        }

        // multiplication method
        public static void mult(double num)
        {
            if(result == 0)
            {
                result = num; // save first number in result
            }
            else
                result *= num;
        }

        // division method
        public static void div(double num)
        {
            if (result != 0 && num == 0)
            {
                MessageBox.Show("Cannot divide by 0");
            }               
            if(result == 0)
            {
                result = num; // save first number in result
            }
            else
                result /= num;
        }

        // sin method
        public static void sin(double num)
        {
            if(result == 0)
            {
                result = Math.Sin(num);
            }
            else
                result = Math.Sin(result);          
        }

        // cos method
        public static void cos(double num)
        {
            if(result == 0)
            {
                result = Math.Cos(num);
            }
            else
                result = Math.Cos(result);
        }

        // tan method
        public static void tan(double num)
        {
            if(result == 0)
            {
                result = Math.Tan(num);
            }
            else
                result = Math.Tan(result); 
        }

        // square root method
        public static void sqrt(double num)
        {
            if(result == 0)
            {
                result = Math.Sqrt(num);
            }
            else
                result = Math.Sqrt(result);
        }

        // modulus method
        public static void modulo(double num)
        {
            if(result == 0)
            {
                result = num;
            }
            else
            {
                result %= num;
            }
        }

        // square method
        public static void square(double num)
        {
                result = num * num;
        }

        // cube method
        public static void cube(double num)
        {
            result = num * num * num;
        }

        // factorial method
        public static long factorial(long num)
        {
            if(num >= 21 || num < 0)
            {
                MessageBox.Show("Enter number between 0 & 20", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            else
            {
                if(num <= 1)
                    return 1;
                else
                    return num * factorial(num - 1); // recursive function
            }
        }

        // fibonacci method
        public static long fibonacci(long num)
        {
            if(num >= 40 || num < 0)
            {
                MessageBox.Show("Enter number between 0 & 40", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            else
            {
                if(num == 0 || num == 1)
                    return num;
                else
                    return fibonacci(num - 1) + fibonacci(num - 2);
            }
        }

        public static void clear()
        {
            result = 0; // clear result
        }

        public static double getResult()
        {
            return result; // get result
        }

    } // end class Operations
} // end namespace Calculator_GUI