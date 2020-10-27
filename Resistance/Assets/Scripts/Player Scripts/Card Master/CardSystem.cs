using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Mirror;
public class CardSystem : NetworkBehaviour
{
    public Camera CMCam; //card master camera
    private GameObject spawnable; //monster prefab
    public int numberToSpawn;
    public int goldToSpend;
    public GameObject spawnPoint; //point where monsters should spawn
    public ParticleSystem spawnParticle;
    private MonsterSpawnerCM monsterSpawnerCM;

    public GameObject Spawnable { get => spawnable; set => spawnable = value; }

    private void Awake()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("WaveSpawner");
        spawnParticle = GameObject.FindGameObjectWithTag("SpawnParticle").GetComponent<ParticleSystem>();
    }

    public void MakeMob()
    {
        //make new mob object everytime this function is called. 
        //this is so that we can keep track of each mob that the card master spawns 
        //for future features such as taking control of the mob 
        GameObject mob = new GameObject();
        GameObject _o;
        mob.name = spawnable.name + "_ " + mob.GetInstanceID().ToString();

        float spacing = 0f;

        for (int i = 0; i < numberToSpawn; i++)
        {
            spacing += Spawnable.transform.localScale.z + 1f;
            Vector3 pos;

            //Debug.LogError("INDEX: " + Spawnable.GetComponent<MonsterController>().monsterIndex);

            //spawn monsters to the left/right side of the first monster
            if (i % 2 == 0)
            {
                pos = new Vector3(spawnPoint.transform.position.x, 0f, spawnPoint.transform.localScale.z + (spacing * numberToSpawn));
                //_o = Instantiate(Spawnable.gameObject);
                CmdSpawnMonster(Spawnable.GetComponent<MonsterController>().monsterIndex, pos);
            }
            else
            {
                pos = new Vector3(spawnPoint.transform.position.x, 0f, spawnPoint.transform.localScale.z - (spacing * numberToSpawn));

                //_o = Instantiate(Spawnable.gameObject);
                CmdSpawnMonster(Spawnable.GetComponent<MonsterController>().monsterIndex, pos);
            }
            Spawnable.GetComponent<NavMeshAgent>().Warp(pos);

            //Spawnable.GetComponent<MonsterController>().PlaySpawnAnim();

            //set the mob gameobject as the monster's parent. 
            //_o.transform.parent = mob.transform;

            //navmeshagents' position must be set using Warp() prior to setting destination.
            //_o.GetComponent<NavMeshAgent>().Warp(pos);
        }
    }

    [Command]
    public void CmdSpawnMonster(int index, Vector3 pos)
    {
        if (monsterSpawnerCM == null)
        {
            monsterSpawnerCM = GameObject.FindGameObjectWithTag("MonsterSpawnerCM").GetComponent<MonsterSpawnerCM>();
        }
        GameObject temp = Instantiate(monsterSpawnerCM.monsterPrefabs[index]);
        NetworkServer.Spawn(temp);
        temp.GetComponent<NavMeshAgent>().Warp(pos);
        temp.GetComponent<MonsterController>().PlaySpawnAnim();
    }

    public void SpawnMonsters()
    {
        //check if user selected a monster. Prevents the spawn particlesystem from firing unnecessarily.
        if (numberToSpawn > 0)
        {
            GetComponent<CardMaster>().gold -= goldToSpend;
            GetComponent<CardMaster>().gs.SetGold(GetComponent<CardMaster>().gold);
            StartCoroutine(StartSpawnAnimation(spawnParticle));
            Spawnable.GetComponent<Draggable>().card.maxNumber = Spawnable.GetComponent<Draggable>().stats.remainingAvailable;
        }
    }

    //coroutine to wait for the middle of the spawn animation before spawning the monsters. 
    IEnumerator StartSpawnAnimation(ParticleSystem p)
    {
        if (p != null)
        {
            p.Play();
            var main = p.main; // particle .duration is deprecated, so use main instead.
            yield return new WaitForSeconds(main.duration / 2);
            MakeMob();
        }
    }

    public int testBuy(int gold)
    {
        return gold -= goldToSpend;
    }
}
