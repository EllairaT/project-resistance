using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    public LayerMask layer;
    public Camera CMCam;
    public SpawnableMonster spawnable;
    public int numberToSpawn = 5;

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

    public void MakePreview()
    {
        isPlacing = true;

        float spacing = 0f;

        for (int i = 0; i < numberToSpawn; i++)
        {
            spacing += spawnable.transform.localScale.z + 2f;
            Instantiate(spawnable.gameObject, new Vector3(0f,0f, spacing * numberToSpawn), Quaternion.identity);
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
