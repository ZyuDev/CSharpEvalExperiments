using System.Collections.Generic;
using System.Text;
using DynamicExpresso;
using NUnit.Framework;
using System;
using System.Data;
using System.Dynamic;
using EvalExperiments.Common;

namespace EvalExperiments.DynamicExpresso.UnitTests
{
    public class ObjectArgumentTests
    {
        /// <summary>
        /// This doesn't work with DynamicExpresso
        /// </summary>
        public void CalcWithExpandoObject()
        {
            dynamic headerModel = new ExpandoObject();
            headerModel.rate = 0.05;
            headerModel.amount = 1000;
            headerModel.tax = 0;

            var expression = @"(decimal)headerModel.rate * (decimal)headerModel.amount";

            var interpreter = new Interpreter(InterpreterOptions.DefaultCaseInsensitive).SetVariable("headerModel", headerModel);

            var result = interpreter.Eval(expression);

            Assert.AreEqual(50, result);

        }

        [Test]
        public void CalcWithCalculatableObject()
        {
            var headerModel = new CalculatableObject();
            headerModel.SetPropertyValue("rate", 0.05M);
            headerModel.SetPropertyValue("amount", 1000M);
            headerModel.SetPropertyValue("tax", 0M);

            Func<CalculatableObject, string, decimal> FunctionGetPropertyValueDecimal;
            FunctionGetPropertyValueDecimal = CalculatableObject.GetPropertyValue<decimal>;

            var expression = "GetPropertyValueDecimal(headerModel, \"rate\") * GetPropertyValueDecimal(headerModel, \"amount\")";

            var interpreter = new Interpreter(InterpreterOptions.DefaultCaseInsensitive)
                .SetFunction("GetPropertyValueDecimal", FunctionGetPropertyValueDecimal)
                .SetVariable("headerModel", headerModel);

            var result = interpreter.Eval(expression);

            Assert.AreEqual(50, result);

        }

        [Test]
        public void CalcWithGetProperty()
        {
            var headerModel = new CalculatableObject();
            headerModel.SetPropertyValue("rate", 0.05M);
            headerModel.SetPropertyValue("amount", 1000M);
            headerModel.SetPropertyValue("tax", 0M);

            var expression = "headerModel[\"rate\"] + headerModel[\"amount\"]";

            var interpreter = new Interpreter(InterpreterOptions.DefaultCaseInsensitive)
                .SetVariable("headerModel", headerModel);

            var result = interpreter.Eval(expression);

            Assert.AreEqual(50, result);

        }

        [Test]
        public void CalcIndependentVarExpression()
        {
            var headerModel = new CalculatableObject();
            headerModel.SetPropertyValue("rate", 0.05M);
            headerModel.SetPropertyValue("amount", 1000M);
            headerModel.SetPropertyValue("tax", 0M);

            var rate = headerModel.GetPropertyValue("rate");
            var amount = headerModel.GetPropertyValue("amount");

            var expression = "rate * amount";

            var interpreter = new Interpreter(InterpreterOptions.DefaultCaseInsensitive)
                .SetVariable("rate", rate).SetVariable("amount", amount);

            var result = interpreter.Eval(expression);


            Assert.AreEqual(50, result);

        }

    }
}
