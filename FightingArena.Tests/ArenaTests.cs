namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System.Threading;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        [Test]
        public void ArenaConstructroShouldWorkCorrectly()
        {
            Arena arena = new Arena();
           
            Assert.AreEqual(0, arena.Warriors.Count);
        }
        [Test]
        public void EnrollShouldThrowAnErrorIfWarriorAlreadyExist()
        {
            Warrior warrior = new Warrior("Gosho", 25, 40);
            Warrior warrior2 = new Warrior("Gosho", 33, 40);
            Arena arena = new Arena();
            arena.Enroll(warrior);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => arena.Enroll(warrior2));
            
            Assert.AreEqual("Warrior is already enrolled for the fights!", ex.Message);
        }

        [Test]
        public void EnrollShouldAddWarriorToArenaCorrectly()
        {
            Warrior warrior = new Warrior("Gosho", 25, 40);
            Warrior warrior2 = new Warrior("Pesho", 33, 40);
            Warrior warrior3 = new Warrior("Dimo", 40, 40);
            Arena arena = new Arena();
            arena.Enroll(warrior);
            arena.Enroll(warrior2);
            arena.Enroll(warrior3);
            Assert.AreEqual(3, arena.Warriors.Count);

        }

        [Test]
        public void ThereCannotBeFight_IfAttackerWarriorIsNotEnrolled()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("Gosho", 25, 40);
            Warrior warrior2 = new Warrior("Pesho", 33, 40);
            arena.Enroll(warrior2);

            string missingName = warrior.Name;

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => arena.Fight(warrior.Name,warrior2.Name));

            Assert.AreEqual($"There is no fighter with name {missingName} enrolled for the fights!", ex.Message);
        }
        [Test]
        public void ThereCannotBeFight_IfDefenderWarriorIsNotEnrolled()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("Gosho", 25, 40);
            Warrior warrior2 = new Warrior("Pesho", 33, 40);
            arena.Enroll(warrior);

            string missingName = warrior2.Name;

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => arena.Fight(warrior.Name, warrior2.Name));

            Assert.AreEqual($"There is no fighter with name {missingName} enrolled for the fights!", ex.Message);
        }
        [Test]
        public void FightShouldWorkCorrectly()
        {
            Arena arena = new Arena();
            Warrior warrior = new Warrior("Gosho", 25, 40);
            Warrior warrior2 = new Warrior("Pesho", 33, 40);
            arena.Enroll(warrior);
            arena.Enroll(warrior2);
            int expectedHP = 15;
            arena.Fight(warrior.Name, warrior2.Name);
            Assert.AreEqual(expectedHP, warrior2.HP);
        }
    }
}
