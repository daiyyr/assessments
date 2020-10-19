using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;


namespace ClassLibraryCalculator
{
    public class Calculator
    {
        public double Add(double a, double b)
        {
            return (a + b);
        }
        public double Sub(double a, double b)
        {
            return (a - b);
        }
        public double Mul(double a, double b)
        {
            return Math.Round(a * b,2);
        }
        public double Div(double a, double b)
        {
            return ((b == 0) ?  throw new DivideByZeroException("Can't divide by 0"): Math.Round(a / b, 2));
        }
        public string SquareRootString(double a)
        {
            //    Complex[] imaginaryPart = { new Complex(0, Complex.Sqrt(a).Imaginary), new Complex(0, -Complex.Sqrt(a).Imaginary) };
            //    Complex[] realPart = { new Complex(Complex.Sqrt(a).Real, 0), new Complex((-Complex.Sqrt(a).Real), 0) };
            Complex postiveImaginaryPart = new Complex(0, Complex.Sqrt(a).Imaginary);
            Complex negativeImaginaryPart = new Complex(0, -(Complex.Sqrt(a).Imaginary));
            Complex negativeRealPart = new Complex(-(Complex.Sqrt(a).Real), 0);
            Complex positiveRealPart = new Complex(Complex.Sqrt(a).Real, 0);
            Complex[] imaginaryPart = new Complex[2] { postiveImaginaryPart, negativeImaginaryPart };
            Complex[] realPart = new Complex[2] { positiveRealPart, negativeRealPart };
            string result = "";
            if (a < 0)
            {
                result = String.Format("{0}i,+{1}i", imaginaryPart[1].Imaginary, imaginaryPart[0].Imaginary);

            }
            else if (a >= 0)
            {
                result = String.Format("{0},+{1}", realPart[1].Real, realPart[0].Real);
            }
            return result;
        }

        public Complex[] SquareRoot(double a)
        {
            Complex postiveImaginaryPart = new Complex(0, Complex.Sqrt(a).Imaginary);
            Complex negativeImaginaryPart = new Complex(0, -(Complex.Sqrt(a).Imaginary));
            Complex negativeRealPart = new Complex(-(Complex.Sqrt(a).Real), 0);
            Complex positiveRealPart = new Complex(Complex.Sqrt(a).Real, 0);
            //Complex[] imaginaryPart = { new Complex(0, Complex.Sqrt(a).Imaginary), new Complex(0, -Complex.Sqrt(a).Imaginary) };
            //Complex[] realPart = { new Complex(Complex.Sqrt(a).Real, 0), new Complex((-Complex.Sqrt(a).Real), 0) };
            Complex[] imaginaryPart = new Complex[2] { postiveImaginaryPart, negativeImaginaryPart };
            Complex[] realPart = new Complex[2] { positiveRealPart, negativeRealPart };
            Complex[] myComplex = new Complex[2];
            if (a < 0)
            {
                //result = String.Format("{0}i,+{1}i", imaginaryPart[1].Imaginary, imaginaryPart[0].Imaginary);
                myComplex[0] = negativeImaginaryPart.Imaginary;
                myComplex[1] = postiveImaginaryPart.Imaginary ;
            }
            else if (a >= 0)
            {
                //result = String.Format("{0},+{1}", realPart[1].Real, realPart[0].Real);
                myComplex[0] = negativeRealPart.Real;
                myComplex[1] = positiveRealPart.Real;
            }
            return myComplex;
        }
    }
}
