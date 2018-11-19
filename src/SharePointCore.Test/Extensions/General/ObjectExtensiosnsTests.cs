using SharePointCore.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointCore.Extensions.Tests
{
    [TestFixture()]
    public class ObjectExtensiosnsTests
    {
        [Test()]
        public void ToStringSafe_NullObject_ReturnsStringEmpty()
        {
            string input = null;
            var actual = input.ToStringSafe();
            Assert.AreEqual(actual, string.Empty);
        }

        [Test()]
        public void ToStringSafe_IntegerValue_ReturnsString()
        {
            string input = "123";
            var actual = input.ToStringSafe();
            Assert.AreEqual(actual, "123");
        }

        [Test()]
        public void ToDouble_BadValue_ReturnsZero()
        {
            string input = "12/d3$$%";
            var actual = input.ToDouble();
            Assert.AreEqual(actual, 0);
        }

        [Test()]
        public void ToDouble_StringDoublealue_ReturnsDoubleValue()
        {
            string input = "123.456";
            var actual = input.ToDouble();
            Assert.AreEqual(actual, 123.456);
        }

        [Test()]
        public void ToInt_BadValue_ReturnsZero()
        {
            string input = "12/d3";
            var actual = input.ToInt();
            Assert.AreEqual(actual, 0);
        }
    }
}
