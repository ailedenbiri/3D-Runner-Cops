using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private PlayerSpawnerController playerSpawnerScript;
    private GameObject playerSpawnerGO;
    
    void Start()
    {
        playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        playerSpawnerScript = playerSpawnerGO.GetComponent<PlayerSpawnerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerSpawnerScript.PlayerGotKilled(other.gameObject);
        }
    }



}
