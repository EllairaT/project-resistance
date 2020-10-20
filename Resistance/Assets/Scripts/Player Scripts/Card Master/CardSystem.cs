using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    public LayerMask layer;
    public Camera CMCam;
    public SpawnableMonster spawnable;
    public int numberToSpawn = 5;

    public GameObject spawnPoint;

    public ParticleSystem spawnParticle;

    public void MakeMob()
    {
        GameObject mob = new GameObject();
        GameObject _o;
        mob.name = spawnable.name + "_ " + mob.GetInstanceID().ToString();

        float spacing = 0f;

        for (int i = 0; i < numberToSpawn; i++)
        {
            spacing += spawnable.transform.localScale.z + 2f;

            if (i % 2 == 0)
            {
                _o = Instantiate(spawnable.gameObject, new Vector3(spawnPoint.transform.position.x, 0f, spawnPoint.transform.localScale.z + (spacing * numberToSpawn)), Quaternion.identity);
            }
            else
            {
                _o = Instantiate(spawnable.gameObject, new Vector3(spawnPoint.transform.position.x, 0f, spawnPoint.transform.localScale.z - (spacing * numberToSpawn)), Quaternion.identity);
            }
            _o.transform.parent = mob.transform;
        }
    }

    public void SpawnMonsters()
    {
        StartCoroutine(StartSpawnAnimation(spawnParticle));
    }

    IEnumerator StartSpawnAnimation(ParticleSystem p)
    {
        p.Play();
        yield return new WaitUntil(() => p.isStopped);
        MakeMob();
    }
}
