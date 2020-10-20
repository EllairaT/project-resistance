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

    public Transform spawnPoint;
    public bool isPlacing = false;

    //make new gameobject to contain the spawnables
    private void Update()
    {
        if (isPlacing)
        {
            MakeRay();
        }
    }

    public bool IsPlacing()
    {
        return isPlacing;
    }

    public void MakeMob()
    {
        GameObject mob = new GameObject();
        GameObject _o;
        mob.name = spawnable.name + "_ " + mob.GetInstanceID().ToString();

        isPlacing = true;

        float spacing = 0f;

        for (int i = 0; i < numberToSpawn; i++)
        {
            spacing += spawnable.transform.localScale.z + 2f;
            
            if(i % 2 == 0)
            {
                _o = Instantiate(spawnable.gameObject, new Vector3(spawnPoint.position.x, 0f, spawnPoint.localScale.z + (spacing * numberToSpawn)), Quaternion.identity);
            }
            else
            {
                _o = Instantiate(spawnable.gameObject, new Vector3(spawnPoint.position.x, 0f, spawnPoint.localScale.z - (spacing * numberToSpawn)), Quaternion.identity);
            }
            _o.transform.parent = mob.transform;
        }
    }

    public void MakeRay()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = CMCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 70f, layer))
        {
            spawnable.gameObject.transform.position = hit.point;
        }
    }
}
