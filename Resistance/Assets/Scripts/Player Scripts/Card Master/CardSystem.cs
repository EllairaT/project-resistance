using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    public LayerMask layer;
    public Camera CMCam;
    public GameObject monsterToSpawn;

    public SpawnableMonster spawnable;

    void MakeRay()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = CMCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 70f, layer))
        {
            monsterToSpawn.transform.position = hit.point;
        }
    }
}
