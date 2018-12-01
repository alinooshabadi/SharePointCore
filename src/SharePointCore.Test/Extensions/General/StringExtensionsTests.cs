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
            Assert.AreEqual(actual, true);
        }
    }
}