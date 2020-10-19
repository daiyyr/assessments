using ClassLibraryCalculator;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
namespace A2_15058803
{
    [TestClass]
    public class UnitTestCalculator
    {
        [TestMethod]
        public void TestMethodAdd()
        {
            Calculator myCalculator = new Calculator();
            double expected = 3.1;
            double actual = myCalculator.Add(1, 2.1);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestMethodSub()
        {
            Calculator myCalculator = new Calculator();
            double expected = -1;
            double actual = myCalculator.Sub(1, 2);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestMethodMul()
        {
            Calculator myCalculator = new Calculator();
            double expected = 2.2;
            double actual = myCalculator.Mul(1.1, 2);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestMethodDiv()
        {
            Calculator myCalculator = new Calculator();
            double expected = 0.5;
            double actual = myCalculator.Div(1, 2);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        [ExpectedException(typeof(System.DivideByZeroException), "Can't divide by 0")]
        public void TestMethodDivByZero()
        {
            Calculator myCalculator = new Calculator();
            double actual = myCalculator.Div(1, 0);
            Exception expected = new DivideByZeroException("Can't divide by 0");
            Assert.AreEqual(expected, actual);


        }
        [TestMethod]

        public void TestMethodSquareRootForNegativeNum()
        {
            Calculator myCalculator = new Calculator();
            Complex[] expected = new Complex[] { -2, 2 };
            Complex[] actual = myCalculator.SquareRoot(-4);
            CollectionAssert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void TestMethodSquareRootForPositiveNum()
        {
            Calculator myCalculator = new Calculator();
            Complex[] expected = new Complex[] { -2, 2 };
            Complex[] actual = myCalculator.SquareRoot(4);
            CollectionAssert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void TestMethodSquareRootForNegativeNumString()
        {
            Calculator myCalculator = new Calculator();
            string expected = "-2i,+2i";
            string actual = myCalculator.SquareRootString(-4);
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void TestMethodSquareRootForPositiveNumString()
        {
            Calculator myCalculator = new Calculator();
            string expected = "-2,+2";
            string actual = myCalculator.SquareRootString(4);
            Assert.AreEqual(expected, actual);

        }
    }
}
