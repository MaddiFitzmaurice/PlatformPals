using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Transform P1RespawnPoint;
    public Transform P2RespawnPoint;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player1"))
        {
            player1.transform.position = P1RespawnPoint.position;
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            player2.transform.position = P2RespawnPoint.position;
        }

    }
}
