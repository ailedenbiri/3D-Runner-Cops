using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject playerSpawnerGO;
    public ZombieSpawnerController zombieSpawnerScript;
    private bool isZombieAlive;
    void Start()
    {
        isZombieAlive = true;
    }

    void FixedUpdate()
    {
        if (zombieSpawnerScript.isZombieAttacking == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerSpawnerGO.transform.position, Time.fixedDeltaTime * 1.5f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isZombieAlive == true)
        {
            isZombieAlive = false;
            zombieSpawnerScript.ZombieAttackThisCop(collision.gameObject, this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Destroy(other.gameObject);
           zombieSpawnerScript.ZombieGotShoot(this.gameObject);
        }
    }
}
