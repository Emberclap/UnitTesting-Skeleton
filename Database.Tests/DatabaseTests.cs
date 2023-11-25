namespace Database.Tests
{
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;
        [SetUp]
        public void Setup()
        {
           this.database = new Database(1,2);
        }
        [Test]
        public void IsDatabaseCreated()
        {
            
            Assert.IsNotNull(database);
        }
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15,16 })]
        public void DatabaseAddElementCorrectly(int[] data)
        {
            database = new(data);
            int[] result = database.Fetch();
            Assert.AreEqual(data, result);
        }
        [TestCase(new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18})]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18,19,20 })]
        public void DatabaseThrowErrorWhen_ThereIsMoreThan16(int[] data)
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database = new Database(data));  
            
            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        //[TestCase(-3)]
        [TestCase(0)]
        public void DatabaseAddMethodShouldIncreaseCount(int data)
        {
            int expectedResult = 3;
            database.Add(data);
            Assert.AreEqual(expectedResult, database.Count);
        }
        [TestCase(new int[] {1,2,3,4,5,6,7,8})]
        public void DatabaseAddMethodShouldAddElementsCorrectly(int[] data)
        {
            database = new Database();

            foreach (int element in data)
            {
                database.Add(element);
            }
            Assert.AreEqual(data, database.Fetch());
        }
        [Test]
        public void DatabaseRemoveMethodShouldDecreseCount()
        {
            int expectedResult = 1;
            database.Remove();
            Assert.AreEqual(expectedResult, database.Count);
        }
        [Test]
        public void DatabaseRemoveMethodShouldRemoveElementsCorrectly()
        {
            int[] expectedResult = Array.Empty<int>();
            database.Remove();
            database.Remove();
            Assert.AreEqual(expectedResult, database.Fetch());
        }
        [Test]
        public void DatabaseThrowErrorWhen_ThereIsMoreThan16()
        {
            database = new Database();
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Remove());
            
            Assert.AreEqual("The collection is empty!", ex.Message);
        }


    }
}
