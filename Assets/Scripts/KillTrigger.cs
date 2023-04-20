using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Transform P1RespawnPoint;
    public Transform P2RespawnPoint;

    private void Start()
    {
        player1 = GameObject.Find("Player");
        player2 = GameObject.Find("Player2");
        P1RespawnPoint = GameObject.Find("P1Respawn").transform;
        P2RespawnPoint = GameObject.Find("P2Respawn").transform;
    }
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
