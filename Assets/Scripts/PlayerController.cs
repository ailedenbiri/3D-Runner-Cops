using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletGO;
    public Transform bulletSpawnTransform;
    private float bulletSpeed = 15f;
    bool isShootingOn;
    Animator playerAnimator;
    Transform PlayerSpawnerCenter;
    float goingToCenterSpeed = 4f;


    void Start()
    {
        PlayerSpawnerCenter = transform.parent.gameObject.transform;
        playerAnimator = GetComponent<Animator>();

    }

    
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerSpawnerCenter.position, Time.fixedDeltaTime * goingToCenterSpeed);
    }
    public void StartShooting()
    {
        StartShootingAnim();
        isShootingOn = true;
        StartCoroutine(Shooting());
        
    }

    public void StopShooting()
    {
        isShootingOn = false;
        StartRunAnim();
    }

    IEnumerator Shooting() 
    { 
        while (isShootingOn)
        {
            yield return new WaitForSeconds(0.5f);
            Shoot();
            yield return new WaitForSeconds(1.5f);
        }    
    
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletGO, bulletSpawnTransform.position, Quaternion.identity);
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.velocity = transform.forward * bulletSpeed;


    }
    
   private void StartShootingAnim()
    {
        playerAnimator.SetBool("isShootingOn", true);
        playerAnimator.SetBool("isRunning", false);

    }
    
    private void StartRunAnim()
    {
        playerAnimator.SetBool("isShootingOn", false);
        playerAnimator.SetBool("isRunning", true);
    }
    
   public void StartIdleAnim()
    {
        playerAnimator.SetBool("isRunning", false);
        playerAnimator.SetBool("isLevelFinish", true);

    }


}
