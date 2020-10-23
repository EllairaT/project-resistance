using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    public Camera CMCam;
    private GameObject spawnable;
    public int numberToSpawn;

    public GameObject spawnPoint;
    public ParticleSystem spawnParticle;

    public GameObject Spawnable { get => spawnable; set => spawnable = value; }


    public void MakeMob()
    {
        GameObject mob = new GameObject();
        GameObject _o;
        mob.name = spawnable.name + "_ " + mob.GetInstanceID().ToString();

        float spacing = 0f;

        for (int i = 0; i < numberToSpawn; i++)
        {
            spacing += Spawnable.transform.localScale.z + 1f;

            if (i % 2 == 0)
            {
                _o = Instantiate(Spawnable.gameObject, new Vector3(spawnPoint.transform.position.x, 0f, spawnPoint.transform.localScale.z + (spacing * numberToSpawn)), Quaternion.identity);
            }
            else
            {
                _o = Instantiate(Spawnable.gameObject, new Vector3(spawnPoint.transform.position.x, 0f, spawnPoint.transform.localScale.z - (spacing * numberToSpawn)), Quaternion.identity);
            }
            _o.transform.parent = mob.transform;
        }
    }


    public void SpawnMonsters()
    {
        if (numberToSpawn > 0)
        {
            MakeMob();
            StartCoroutine(StartSpawnAnimation(spawnParticle));
        }
    }

    IEnumerator StartSpawnAnimation(ParticleSystem p)
    {
        p.Play();
        yield return new WaitForSeconds(3f);
        MakeMob();
    }
}
