using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerSpawnerController : MonoBehaviour
{
    public GameObject playerGO;
    public float playerSpeed;
    float xSpeed;
    float maxXPosition = 4.1f;
    public List<GameObject> playersList = new List<GameObject>();
    bool isPlayersMoving;


    public AudioSource playerSpawnerAudioSource;
    public AudioClip gateClip, congratsClip, failClip, shootClip;

    private void Start()
    {
        
        
    }


    void Update()
    {
        if (isPlayersMoving == false) 
        {
            return;
        
        
        }
        float touchX = 0;
        float newXValue = 0;
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved )
        {
            xSpeed = 300f;
            touchX = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        else if (Input.GetMouseButton(0))
        {
            xSpeed = 500f;
            touchX = Input.GetAxis("Mouse X");
        }
           
        newXValue = transform.position.x + xSpeed* touchX* Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, -maxXPosition, maxXPosition);
        Vector3 playerNewPosition = new Vector3( newXValue,transform.position.y,transform.position.z + playerSpeed* Time.deltaTime);
        transform.position = playerNewPosition;
    }

    public void SpawnPlayer (int gateValue, GateType gateType)
    {
        PlayAudio(gateClip);

        if(gateType == GateType.additionType)
        {
            for (int i = 0; i < gateValue; i++)
            {
                GameObject newPlayerGO = Instantiate(playerGO, GetPlayerPosition(), Quaternion.identity, transform);
                playersList.Add(newPlayerGO);
            }

        }
        else if (gateType == GateType.multiplyType)
        {
            int newPlayerCount = (playersList.Count * gateValue) - playersList.Count;
            for (int i = 0; i < newPlayerCount; i++)
            {
                GameObject newPlayerGO = Instantiate(playerGO, GetPlayerPosition(), Quaternion.identity, transform);
                playersList.Add(newPlayerGO);
            }
        }
        

        
    }
    public Vector3 GetPlayerPosition()
    {
        Vector3 position = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + position;
        newPos.y = 0.4f;
        return newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FinishLine")
        {
            Debug.Log("Finish Line Touched");
            StopBackroundMusic();
            PlayAudio(congratsClip);
            StartAllCopsIdleAnim();
            isPlayersMoving = false;
            GameManager.Instance.ShowSucessPanel();
            //Stop Moving
        }

    }
    public void PlayerGotKilled(GameObject playerGO)
    {
        playersList.Remove(playerGO);
        Destroy(playerGO);
        CheckPlayersCount();
    }

    private void CheckPlayersCount()
    {
        if(playersList.Count <=0)
        {
            StopBackroundMusic();
            PlayAudio(failClip);
            StopPlayer();
            GameManager.Instance.ShowFailPanel();
        }
    }
    public void ZombieDetected(GameObject target)
    {
        isPlayersMoving= false;
        LookAtZombies(target);
        StartAllCopsShooting();
        PlayAudio(shootClip);
    }
    
    private void LookAtZombies(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = lookRotation;


    }
    private void LookAtForward()
    {
        transform.rotation = Quaternion.identity;
    }
    public void AllZombiesKilles()
    {
        LookAtForward();
        MovePlayer();

    }

    public void MovePlayer()
    {
        StartAllCopsRunAnim();
        isPlayersMoving =true;
    }


    public void StopPlayer()
    {
        isPlayersMoving=false;
    }

    public void StartAllCopsShooting ()
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            PlayerController cop = playersList[i].GetComponent<PlayerController>();
            cop.StartShooting();
        }
    }

    public void StartAllCopsRunAnim()
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            PlayerController cop = playersList[i].GetComponent<PlayerController>();
            cop.StopShooting();
        }
    }


    public void StartAllCopsIdleAnim()
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            PlayerController cop = playersList[i].GetComponent<PlayerController>();
            cop.StartIdleAnim();
        }
    }


    private void PlayAudio (AudioClip clip)
    {
        if (playerSpawnerAudioSource !=null)
        {
            playerSpawnerAudioSource.PlayOneShot(clip, 0.4f);
        }
    }

    private void StopBackroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }

}
