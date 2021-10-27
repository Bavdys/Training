using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace App02._1.Tests
{
    [TestFixture]
    class BookTests
    {
        [Test]
        public void Exception_InitializingTitleLongerThanSpecified_ArgumentException()
        {
            Assert.Throws<ArgumentException>(()=>
            {
                var avtor = new Writer("Charles", "Dickens");
                var book = new Book("".PadLeft(1001,'q'), "2463376978034", Convert.ToDateTime("05.10.1838"), new List<Writer> { avtor });
            });
        }
        
        [Test]
        public void Exception_InitializingIncorrectISBN_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var avtor = new Writer("Charles", "Dickens");
                var book = new Book("Oliver Twist", "246337-6978034", Convert.ToDateTime("05.10.1838"), new List<Writer> { avtor });
            });
        }
       
        [Test]
        public void IsEqual_ObjectEqual_ReturnTrue()
        { 
            var avtor = new Writer("Charles", "Dickens");
            var bookOliverTwist = new Book("Oliver Twist", "2463376978034", Convert.ToDateTime("05.10.1838"), new List<Writer> { avtor });
            var bookChristmasCarol = new Book("A Christmas Carol", "2463376978034", Convert.ToDateTime("16.5.1843"), new List<Writer> { avtor });

            bool result = bookChristmasCarol.Equals(bookOliverTwist);

            Assert.IsTrue(result);
        }
    }
}
