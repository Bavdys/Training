using NUnit.Framework;
using System;

namespace App02._1.Tests
{
    [TestFixture]
    class WriterTests
    {
        [Test]
        public void Exception_InitializingFirstNameLongerThanSpecified_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var avtor = new Writer("".PadLeft(201,'q'), "Dickens");
            });
        }
        
        [Test]
        public void Exception_InitializingLastNameLongerThanSpecified_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var avtor = new Writer("Charles", "".PadLeft(201,'q'));
            });
        }
    }
}
