using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayMode
    {
        [UnityTest]
        public IEnumerator MoveX_Axis() //Unit Test for moving along the X axis
        {
            var gameObj = new GameObject();
            var movement = gameObj.AddComponent<PlayerMovementController>();
            Assert.AreEqual(5, movement.CalculateMovement(1, 1, 1).x);

            yield return null;
        }

        [UnityTest]
        public IEnumerator MoveZ_Axis() //Unit Test for moving along the Y axis
        {
            var gameObj = new GameObject();
            var movement = gameObj.AddComponent<PlayerMovementController>();
            Assert.AreEqual(5, movement.CalculateMovement(1, 1, 1).z);

            yield return null;
        }

        [UnityTest]
        public IEnumerator MouseMoveX() //Unit Test for looking along the X axis
        {
            var gameObj = new GameObject();
            var mouse = gameObj.AddComponent<MouseLook>();
            mouse.playerBody = gameObj.transform;
            Assert.AreEqual(-20, mouse.TestMouseLookX(1, 20, 1));

            yield return null;
        }

        [UnityTest]
        public IEnumerator MouseMoveY() //Unit Test for looking along the Y axis
        {
            var gameObj = new GameObject();
            var mouse = gameObj.AddComponent<MouseLook>();
            mouse.playerBody = gameObj.transform;
            Assert.AreEqual(20, mouse.TestMouseLookY(1, 20, 1));

            yield return null;
        }

        [UnityTest]
        public IEnumerator Shoot() //Unit Test for shooting a bullet
        {
            var gameObj = new GameObject();

            var shoot = new GunScript();
            shoot.ammoScript = gameObj.AddComponent<AmmoScript>();
            shoot.healthScript = gameObj.AddComponent<HealthBarScript>();
            shoot.goldScript = gameObj.AddComponent<GoldScript>();

            Assert.AreEqual(19, shoot.TestShoot(20));

            yield return null;
        }

        [UnityTest]
        public IEnumerator EarnGold() //Unit Test for earning gold
        {
            var gameObj = new GameObject();

            var shoot = new GunScript();
            shoot.ammoScript = gameObj.AddComponent<AmmoScript>();
            shoot.healthScript = gameObj.AddComponent<HealthBarScript>();
            shoot.goldScript = gameObj.AddComponent<GoldScript>();

            Assert.AreEqual(1010, shoot.TestEarnGold(100, 10));

            yield return null;
        }

        
        //[UnityTest]
        //public IEnumerator CalculateDamageTest() //Unit test to calculate damage taken by structure (depending on material hardness)
        //{
        //    var testStruct = Structure.CreateInstance<Structure>();
        //    testStruct.hardness = 2;
        //    testStruct.baseHealth = 100;
        //    var baseDamage = 50;
        //    var expectedDamage = baseDamage / testStruct.hardness;

        //    Assert.AreEqual(expectedDamage, testStruct.CalculateDamageTaken(baseDamage));
        //    yield return null;
        //}

        //[UnityTest]
        //public IEnumerator AssignMaterialTest() //Unit test for assigning material to structure
        //{
        //    var testStruct = Structure.CreateInstance<Structure>();
        //    Materials testMat = (Materials)AssetDatabase.LoadAssetAtPath("Assets/Materials/concrete.asset", typeof(Materials));
        //    var gameObj = new GameObject();
        //    var ps = new PlaceableStructure();
        //    ps.mesh = ps.r = gameObj.AddComponent<MeshRenderer>();
        //    ps.stats = testStruct;
        //    ps.AssignMaterial(testMat);

        //    Assert.NotNull(ps.mat);
        //    yield return null;
        //}

        [UnityTest]
        public IEnumerator MonsterTakeDamageTest() //unit test to check that damage reduction for different classes of monsters work
        {
            //in the context of this game, a card can mean monster. 
            var testMonster = Card.CreateInstance<Card>();
            testMonster.Health = 100;
            testMonster.type = Card.ClassType.TANK; //tanks should take 15% less damage
            var expected = 50 - (0.15f * 50);
            Debug.Log(expected);
            testMonster.CalculateDamageTaken(50);

            Assert.AreEqual(expected, testMonster.CalculateDamageTaken(50)); 
            yield return null;
        }

        [UnityTest]
        public IEnumerator AwardGoldTest()
        {
            var testCardMaster = new PlayerScript();
            testCardMaster.role = "Card Master";
            var testMonster = new Card();
            testMonster.goldPerDmg = 10; //award 10 gold per dmg output
            var expected = 10 * 10;

            for (int i = 0; i < 10; i++)
            {
                testMonster.AwardGold(testCardMaster);
            }

            Assert.AreEqual(expected, testCardMaster.gold);
            yield return null;
        }
    }
}
