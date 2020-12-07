using DynamicExpresso;
using NUnit.Framework;
using System;

namespace EvalExperiments.DynamicExpresso.UnitTests
{
    public class SimpleTests
    {

        [Test]
        public void AddWithLiterals_ReturnResult()
        {
            var expression = @"1+2";

            var interpreter = new Interpreter();
            var result = interpreter.Eval(expression);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void AddWithParameters_ReturnResult()
        {
            var a = 1;
            var b = 2;
            var expression = @"a + b";

            var interpreter = new Interpreter();
            interpreter.SetVariable("a", a);
            interpreter.SetVariable("b", b);
            var result = interpreter.Eval(expression);

            Assert.AreEqual(3, result);

        }


        [Test]
        public void AddDifferentTypes_ReturnResult()
        {
            int a = 1;
            decimal b = 2.1M;
            var expression = @"a + b";

            var interpreter = new Interpreter();
            interpreter.SetVariable("a", a);
            interpreter.SetVariable("b", b);
            var result = interpreter.Eval(expression);

            Console.WriteLine($"result: {result}, type: {result.GetType()}");
            Assert.AreEqual(3.1, result);

        }
    }
}