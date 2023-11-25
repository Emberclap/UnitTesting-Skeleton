namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private string make;

        private string model;

        private double fuelConsumption;


        private double fuelCapacity;

        private Car car;
        [SetUp]
        public void Setup()
        {
            this.make = "Opel";
            this.model = "Corsa";
            this.fuelConsumption = 2.5;
            this.fuelCapacity = 100;
            this.car = new Car(make, model, fuelConsumption, fuelCapacity);
        }
        [Test]
        public void FuelAmountMustBe0()
        {
            Assert.AreEqual(0, car.FuelAmount);
        }
        [Test]
        public void ConstructorShouldWorkCorrectly()
        {
            string make = "Opel";
            string model = "Corsa";
            double fuelConsumption = 2.5;
            double fuelCapacity = 100;
            Assert.AreEqual(make,car.Make);
            Assert.AreEqual(model,car.Model);
            Assert.AreEqual(fuelConsumption, car.FuelConsumption);
            Assert.AreEqual(fuelCapacity, car.FuelCapacity);
        }
        [Test]
        [TestCase("")]
        public void MakeCannotBeNullOrEmpty(string make)
        {

            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => new Car(make,model, fuelConsumption, fuelCapacity));

            Assert.AreEqual("Make cannot be null or empty!", ex.Message);
        }
        [TestCase("Subaru")]
        [TestCase("Ford")]
        public void MakeSetterShouldWorkCorrectly(string make)
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            string expected = make;
            string actual = car.Make;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        [TestCase("")]
        public void ModelCannotBeNullOrEmpty(string model)
        {

            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => new Car(make, model, fuelConsumption, fuelCapacity));

            Assert.AreEqual("Model cannot be null or empty!", ex.Message);
        }
        [TestCase("Subaru")]
        [TestCase("Ford")]
        public void ModelSetterShouldWorkCorrectly(string model)
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            string expected = model;
            string actual = car.Model;
            Assert.AreEqual(expected, actual);
        }
        [TestCase(0)]
        [TestCase(-15)]
        public void FuelConsumptionSetterShouldThrowErrorWhenNegativeNumber(double fuelConsumption)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => new Car(make, model, fuelConsumption, fuelCapacity));

            Assert.AreEqual("Fuel consumption cannot be zero or negative!", ex.Message);
            
        }
        [Test]
        public void FuelConsumptionSetterShouldWorkCorrectly()
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            double expected = 2.5;
            double actual = fuelConsumption;
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void FuelAmountSetterShouldWorkCorrectly()
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            double expected = 0;
            double actual = car.FuelAmount;
            Assert.AreEqual(expected, actual);
        }
        
        [TestCase(0)]
        [TestCase(-15)]
        public void FuelCapacitySetterShouldThrowErrorWhenNegativeNumber(double fuelCapacity)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => new Car(make, model, fuelConsumption, fuelCapacity));

            Assert.AreEqual("Fuel capacity cannot be zero or negative!", ex.Message);

        }
        [Test]
        public void FuelCapacitySetterShouldWorkCorrectly()
        {
            Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            double expected = 100;
            double actual = fuelCapacity;
            Assert.AreEqual(expected, actual);
        }
        [TestCase(50)]
        [TestCase(75.5)]
        public void CarRefuelShouldIncreaseFuelAmount(double fuelToRefuel)
        {
            double expected = fuelToRefuel + car.FuelAmount;
            car.Refuel(fuelToRefuel);
            double actual = car.FuelAmount;
            Assert.AreEqual(expected, actual);
        }
        [TestCase(0)]
        [TestCase(-10)]
        public void CarRefuelShouldThrowExceptionIfFuelIsNegativeOrZero(double fuelToRefuel)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(()
                => car.Refuel(fuelToRefuel));

            Assert.AreEqual("Fuel amount cannot be zero or negative!", exception.Message);
        }
        [Test] 
        public void CarFuelAmountShouldNotBeMoreThanFuelCapacity()
        {
            double expected = car.FuelCapacity;
            car.Refuel(120);
            double actual = car.FuelAmount;
            Assert.AreEqual(expected, actual);
        }
        [TestCase(150)]
        [TestCase(200)]
        public void CarDriveMustThrowErrorWhenFuelAmountIsNotEnough(double distance)
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(()
                => car.Drive(distance));

            Assert.AreEqual("You don't have enough fuel to drive!", exception.Message);
        }
        [TestCase(50)]
        [TestCase(70)]

        public void CarDriveShouldRemoveFuelAmount(double distance)
        {
            car.Refuel(120);
            double fuelNeeded = (distance / 100) * this.fuelConsumption;
            double expected = car.FuelAmount - fuelNeeded;
            car.Drive(distance);

            double actual = car.FuelAmount;
            Assert.AreEqual(expected, actual);
        }

    }
}