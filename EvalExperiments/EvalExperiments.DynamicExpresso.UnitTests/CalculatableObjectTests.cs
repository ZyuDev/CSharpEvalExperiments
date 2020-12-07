using System.Collections.Generic;
using System.Text;
using DynamicExpresso;
using NUnit.Framework;
using System;
using System.Data;
using EvalExperiments.Common;

namespace EvalExperiments.DynamicExpresso.UnitTests
{
    public class CalculatableObjectTests
    {
        [Test]
        public void GetProperty_DecimalPropertyGeneric_ReturnDecimalValue()
        {
            var cObject = new CalculatableObject();
            cObject.SetPropertyValue("rate", 0.5M);

            var result = cObject.GetPropertyValue<decimal>("rate");

            Console.WriteLine($"result: {result}, type: {result.GetType()}");

            Assert.AreEqual(0.5, result);
        }

        [Test]
        public void GetProperty_DecimalProperty_ReturnDecimalValue()
        {
            var cObject = new CalculatableObject();
            cObject.SetPropertyValue("rate", 0.5M);

            var result = cObject.GetPropertyValue("rate");

            Console.WriteLine($"result: {result}, type: {result.GetType()}");

            Assert.AreEqual(0.5, result);
        }
    }
}
