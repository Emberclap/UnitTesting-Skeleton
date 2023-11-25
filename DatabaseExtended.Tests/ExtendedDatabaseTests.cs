namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Person person1;
        private Person person2;
        private Person person3;
        private Database database;

        [SetUp]
        public void SetUp()
        {
            this.person1 = new(123, "Pesho");
            this.person2 = new(456, "Gosho");
            this.person3 = new(789, "Ivan");
            this.database = new Database(person1, person2, person3);

        }

        [Test]
        public void IsDatabaseCreated()
        {

            Assert.AreEqual(3, this.database.Count);

        }

        [Test]
        public void DatabaseCountPeopleCorrectly()
        {
            Person person = new Person(111, "Joro");
            database.Add(person);
            Assert.AreEqual(4, database.Count);
        }
        [Test]
        public void NewDatabaseShouldThrowErrorWhenThereIsMoreThan16People()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => database = new Database(person1, person2, person3, person1, person2, person3, person1, person2, person3,
                person1, person2, person3, person1, person2, person3, person1, person2, person3));

            Assert.AreEqual("Provided data length should be in range [0..16]!", ex.Message);
        }
        [Test]
        public void WhenDatabaseAlreadyHave16PeopleAddShouldThrowError()
        {
            Database database = new Database();
            FullDatabaseCreator(database);
            Person person = new Person(111, "Joro");
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(person));
            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }
        [Test]
        public void WhenDatabaseAlreadyHavePersonNameAddShouldThrowError()
        {
            Person person = new Person(1, "Gosho");
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(person));

            Assert.AreEqual("There is already user with this username!", ex.Message);
        }
        [Test]
        public void WhenDatabaseAlreadyHavePersonIDAddShouldThrowError()
        {
            Person person = new Person(123, "Joro");
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(person));
            Assert.AreEqual("There is already user with this Id!", ex.Message);
        }
        [Test]
        public void DatabaseShouldAddPeopleCorrectly ()
        {
            Person person = new Person(1, "Joro");
            database.Add(person);
            Assert.AreEqual(4, database.Count);
        }
        [Test]
        public void DatabaseRemoveShouldWorkCorrectly()
        {
            database.Remove();
            Assert.AreEqual(2, database.Count);
        }
        [Test]
        public void DatabaseRemoveShouldThrowErrorWhenThereIsNoMorePersons()
        {
            Database database = new Database();
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
        [Test]
        public void FindByUsernameShouldThrowErrorWhenNameIsNullOrEmpty()
        {
            
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(()
                => database.FindByUsername(null));
            Assert.AreEqual("Username parameter is null!", ex.ParamName);
            
        }
        [Test]
        public void FindByUsernameShouldThrowErrorWhenNameNotFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.FindByUsername("Grigor"));

            Assert.AreEqual("No user is present by this username!", ex.Message);
        }
        [Test]
        public void DatabaseFindByUsernameMethodShouldBeCaseSensitive()
        {
            string expectedResult = "PeSho";
            string actualResult = database.FindByUsername("Pesho").UserName;

            Assert.AreNotEqual(expectedResult, actualResult);
        }
        [Test]
        public void DatabaseFindByUsernameMethodReturnActualPerson()
        {
            string expected = "Pesho";
            string actualResult = database.FindByUsername("Pesho").UserName;

            Assert.AreEqual(expected, actualResult);
        }
        [Test]

        public void FindByIDShouldThrowErrorWhenIDLessThan0()
        {
            
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(()
                => database.FindById(-2));

            Assert.AreEqual("Id should be a positive number!", ex.ParamName);
        }
        [Test]
        public void FindByIDShouldThrowErrorWhenIDNotFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.FindById(25));

            Assert.AreEqual("No user is present by this ID!", ex.Message);
        }
        [Test]
        public void FindByIDShouldWorkCorrectly()
        {
            string expectedResult = "Pesho";
            string actualResult = database.FindById(123).UserName;

            Assert.AreEqual(expectedResult, actualResult);
        }
        


        public Database FullDatabaseCreator(Database database)
        {
            List<Person> persons = new List<Person>();
            Person person;
            for (int i = 0; i < 16; i++)
            {
                person = new Person(i, "Gosho" + i);
                persons.Add(person);
            }
            foreach (Person element in persons)
            {
                database.Add(element);
            }
            return database;
        }
    }
}