using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusScript : MonoBehaviour
{

    private bool isPlayerInRange = false;
    [SerializeField] private GameObject buyGUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            GetPlayerCamera(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerInRange = false;
    }

    private void GetPlayerCamera(GameObject _player)
    {

    }
}
