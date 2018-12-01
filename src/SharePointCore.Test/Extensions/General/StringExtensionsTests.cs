using NUnit.Framework;
using SharePointCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointCore.Test.Extensions.General
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public static void IsNullOrEmpty_NullObject_ReturnsTrue()
        {
            string input = null;
            var actual = input.IsNullOrEmpty();
            Assert.IsTrue(actual);
        }

        [Test]
        public static void ToPersianNumber_EnglishNumber_ReturnsPersianNumber()
        {
            var input = "1234567890";
            var actual = input.ToPersianNumber();
            Assert.AreEqual(actual, "۱۲۳۴۵۶۷۸۹۰");
        }

        [Test]
        public void ToNumericFormat_DoubleString_ReturnsNumericDouble()
        {
            var input = "1123442.0345655";
            var actual = input.ToNumericFormat();
            Assert.AreEqual(actual, "1,123,442.034,565,5");
        }
    }
}