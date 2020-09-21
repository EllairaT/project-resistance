using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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
    }
}
