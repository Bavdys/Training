using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace App02._1.Tests
{
    [TestFixture]
    public class CatalogTests
    {
        [Test]
        public void Count_GetBooksForNameAvtor_CollectionBooks()
        {
            var avtor = new Writer("Charles", "Dickens");
            var book = new Book("Oliver Twist","2463376978034",Convert.ToDateTime("05.10.1838"),new List<Writer> { avtor});
            var catalog = new Catalog();

            catalog.Add(book);
            var result = catalog.GetBooksForNameAvtor(avtor);

            Assert.AreEqual(1,result.Count);
        }
    }
}