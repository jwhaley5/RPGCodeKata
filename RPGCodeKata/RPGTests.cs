using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGClasses;

namespace RPGCodeKata
{
    [TestClass]
    public class RPGTests
    {
        [TestMethod]
        public void CreateCharacter()
        {
            Character character = new Character(1, "Melee");
            Assert.IsTrue(1 == character.ID);
        }

        [TestMethod]
        public void C1AttackC2()
        {
            Character character1 = new Character(1, "Melee");
            Character character2 = new Character(2, "Ranged");
            int initialHealth = character2.Health;
            character1.Attack(character2, 1);
            Assert.IsTrue(initialHealth > character2.Health);
        }

        [TestMethod]
        public void C2HealsHimself()
        {
            Character character1 = new Character(1, "Melee");
            Character character2 = new Character(2, "Ranged");
            int initialHealth = character2.Health;
            character1.Attack(character2, 1);
            int healthAfterAttack = character2.Health;
            character2.HealCharacter(character2);
            int healthAfterHeal = character2.Health;
            Assert.IsTrue(initialHealth > healthAfterAttack && healthAfterAttack < healthAfterHeal);
        }

        [TestMethod]
        public void C1CantHealC2()
        {
            Character character1 = new Character(1, "Melee");
            Character character2 = new Character(2, "Ranged");
            int initialHealth = character2.Health;
            character1.Attack(character2, 1);
            int healthAfterAttack = character2.Health;
            character1.HealCharacter(character2);
            int healthAfterHeal = character2.Health;
            Assert.IsTrue(initialHealth > healthAfterAttack && healthAfterAttack == healthAfterHeal);
        }

        [TestMethod]
        public void HealingDoesntGoOver1000()
        {
            Character character1 = new Character(1, "Melee");
            character1.HealCharacter(character1);
            Assert.IsTrue(character1.Health <= 1000);
        }

        [TestMethod]
        public void C2DiesAfterReceivingAlotOfDamage()
        {
            Character character1 = new Character(1, "Melee");
            Character character2 = new Character(2, "Ranged");
            character1.Damage = 1001;
            character1.Attack(character2, 1);
            Assert.IsTrue(character2.Alive == false && character2.Health == 0);
        }

        [TestMethod]
        public void C2CantHealIfDead()
        {
            Character character1 = new Character(1, "Melee");
            Character character2 = new Character(2, "Ranged");
            character1.Damage = 1001;
            character1.Attack(character2, 1);
            character2.HealCharacter(character2);
            Assert.IsFalse(character2.Alive && character2.Health > 0);
        }

        [TestMethod]
        public void C1CantAttackHimself()
        {
            Character character1 = new Character(1, "Melee");
            int initialHealth = character1.Health;
            character1.Attack(character1, 1);
            Assert.IsTrue(initialHealth == character1.Health);
        }

        [TestMethod]
        public void CharactersOfDifferentLevelsAttack()
        {
            Character character1 = new Character(1, "Melee");
            Character character2 = new Character(2, "Ranged");
            character1.Level = 10;
            int C1InitialHealth = character1.Health;
            int C2InitialHealth = character2.Health;
            character1.Attack(character2, 1);
            character2.Attack(character1, 1);
            int C1Difference = C1InitialHealth - character1.Health;
            int C2Difference = C2InitialHealth - character2.Health;
            Assert.IsTrue(C1Difference == character2.Damage / 2 && C2Difference == character1.Damage + (character1.Damage / 2));
        }

        [TestMethod]
        public void MeleeFighterRangeTest()
        {
            Character character1 = new Character(1, "Melee");
            Character character2 = new Character(2, "Ranged");
            int initialHealth = character2.Health;
            //FirstAttack is Within range
            character1.Attack(character2, 1);
            int healthAfterFirst = character2.Health;
            //Second Attack is out of range
            character1.Attack(character2, 3);
            Assert.IsTrue(initialHealth > healthAfterFirst && healthAfterFirst == character2.Health);
        }

        [TestMethod]
        public void RangedFighterRangeTest()
        {
            Character character1 = new Character(1, "Melee");
            Character character2 = new Character(2, "Ranged");
            int initialHealth = character1.Health;
            //FirstAttack is Within range
            character2.Attack(character1, 1);
            int healthAfterFirst = character1.Health;
            //Second Attack is out of range
            character2.Attack(character1, 21);
            Assert.IsTrue(initialHealth > healthAfterFirst && healthAfterFirst == character1.Health);
        }

        [TestMethod]
        public void NewlyCreatedCharacterBelongsToNoFaction()
        {
            Character character1 = new Character(1, "Melee");
            Assert.IsTrue(character1.Factions.Count == 0);
        }

        [TestMethod]
        public void CharacterJoinsOneFaction()
        {
            Character character1 = new Character(1, "Melee");
            character1.JoinFaction("Hogwarts");
            Assert.IsTrue(character1.Factions.Count == 1);
        }

        [TestMethod]
        public void CharacterCanJoinOneOrMoreFactions()
        {
            Character character1 = new Character(1, "Melee");
            character1.JoinFaction("Hogwarts");
            character1.JoinFaction("Narnia");
            character1.JoinFaction("Jumanji");
            Assert.IsTrue(character1.Factions.Count == 3);
        }

        [TestMethod]
        public void CharacterCanLeaveOneOrMoreFactions()
        {
            Character character1 = new Character(1, "Melee");
            character1.JoinFaction("Hogwarts");
            character1.JoinFaction("Narnia");
            character1.JoinFaction("Jumanji");
            character1.LeaveFaction("Jumanji");
            Assert.IsTrue(character1.Factions.Count == 2);
        }

        [TestMethod]
        public void AlliesCantDamageEachOther()
        {
            Character character1 = new Character(1, "Melee");
            character1.JoinFaction("Hogwarts");
            Character character2 = new Character(2, "Ranged");
            character2.JoinFaction("Hogwarts");

            int healthBeforeAttack = character2.Health;
            character1.Attack(character2, 1); 

            Assert.IsTrue(character2.Health == healthBeforeAttack);
        }


        [TestMethod]
        public void AlliesCanHealEachOther()
        {
            Character character1 = new Character(1, "Melee");
            character1.JoinFaction("Hogwarts");
            Character character2 = new Character(2, "Ranged");
            character2.JoinFaction("Hogwarts");
            character2.Health = 900;
            int healthBeforeHeal = character2.Health;
            character1.HealCharacter(character2);

            Assert.IsTrue(character2.Health > healthBeforeHeal);
        }

        [TestMethod]
        public void PlayersCanDamageProps()
        {
            Character character1 = new Character(1, "Melee");
            Prop tree = new Prop("Tree", 2000);
            int healthBeforeAttack = tree.Health;
            character1.AttackProp(tree, 1);
            Assert.IsTrue(tree.Health < healthBeforeAttack);
        }

        [TestMethod]
        public void PropIsDestroyedWhenNoHealth()
        {
            Character character1 = new Character(1, "Melee");
            Prop tree = new Prop("Tree", 2000);
            character1.Damage = 2010;
            character1.AttackProp(tree, 1);
            Assert.IsTrue(tree.Destroyed);
        }



    }
}
