namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Runtime.CompilerServices;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void WarriorConstructroShouldWorkCorrectly()
        {
            string expectedName = "Gosho";
            int expectedDMG = 15;
            int expectedHP = 150;

            Warrior warrior = new Warrior(expectedName, expectedDMG, expectedHP);

            Assert.AreEqual(expectedHP, warrior.HP);
            Assert.AreEqual(expectedDMG, warrior.Damage);
            Assert.AreEqual(expectedName, warrior.Name);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("     ")]
        public void WarriorConstructorMustThrowError_WhenNameIsNullOrEmpty(string name)
        {
            
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Warrior(name, 25, 50));

            Assert.AreEqual("Name should not be empty or whitespace!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-20)]
        public void WhenDMGIsNegativ_ShouldThrowExeption(int dmg)
        {

            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Warrior("Gosho", dmg, 50));

            Assert.AreEqual("Damage value should be positive!", ex.Message);
        }

        
        [TestCase(-1)]
        [TestCase(-20)]
        public void WhenHPIsNegativ_ShouldThrowExeption(int hp)
        {

            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Warrior("Gosho", 25, hp));

            Assert.AreEqual("HP should not be negative!", ex.Message);
        }

        [TestCase(29)]
        [TestCase(25)]
        [TestCase(2)]
        public void WhenAttackerHPIsSmaller_ThanMinAttackHP_ShouldThrowExeption(int AttackHp)
        {
            Warrior warrior = new Warrior("Gosho", 25, AttackHp);
            Warrior warrior2 = new Warrior("Pesho", 30, 40);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => warrior.Attack(warrior2));

            Assert.AreEqual("Your HP is too low in order to attack other warriors!", ex.Message);
        }
        [TestCase(30)]
        [TestCase(25)]
        [TestCase(2)]
        public void WhenEnemyAttackerHPIsSmallerOrEqual_ThanMinAttackHP_ShouldThrowExeption(int AttackHp)
        {
            int minAttackHp = 30;
            Warrior warrior = new Warrior("Gosho", 25, 35);
            Warrior warrior2 = new Warrior("Pesho", 30, AttackHp);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => warrior.Attack(warrior2));

            Assert.AreEqual($"Enemy HP must be greater than {minAttackHp} in order to attack him!", ex.Message);
        }

        [TestCase(50)]
        [TestCase(60)]
        [TestCase(80)]
        public void WhenAttackerHPIsSmallerOrEqual_ThanEnemyDMG_ShouldThrowExeption(int AttackDMG)
        {
            
            Warrior warrior = new Warrior("Gosho", 30, 45);
            Warrior warrior2 = new Warrior("Pesho", AttackDMG, 40);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => warrior.Attack(warrior2));

            Assert.AreEqual($"You are trying to attack too strong enemy", ex.Message);
        }
        [Test]
        public void WhenAttackIsSuccess_AttackedWarriorShouldLoseHP()
        {
            int expectedHP = 20;
            
            Warrior warrior = new Warrior("Gosho", 30, 40);
            Warrior warrior2 = new Warrior("Pesho", 20, 40);

            warrior.Attack(warrior2);
            Assert.AreEqual(expectedHP, warrior.HP);

        }

        [Test]
        public void WhenAttackerDMGIsBigger_ThanEnemyHP_ThenEnemyHPIsEqual0()
        {
            int expectedHP = 0;
            Warrior warrior = new Warrior("Gosho", 41, 40);
            Warrior warrior2 = new Warrior("Pesho", 20, 40);
            warrior.Attack(warrior2 );
            Assert.AreEqual(expectedHP, warrior2.HP);
        }

        [Test]
        public void WhenAttackerDMGIsSmaller_ThanEnemyHP_ThenEnemyHPShouldBeReduced()
        {
            int expectedHP = 15;
            Warrior warrior = new Warrior("Gosho", 25, 40);
            Warrior warrior2 = new Warrior("Pesho", 20, 40);
            warrior.Attack(warrior2);
            Assert.AreEqual(expectedHP, warrior2.HP);
        }
    }
}