using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject FailPanel;
    public GameObject SucessPanel;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }


    void Start()
    {

    }


    void Update()
    {

    }

    public void StartButtonTapped()
    {
        MainPanel.gameObject.SetActive(false);
        GameObject playerSpawnerGO = GameObject.FindGameObjectWithTag("PlayerSpawner");
        PlayerSpawnerController playerSpawnerScript = playerSpawnerGO.GetComponent<PlayerSpawnerController>();
        playerSpawnerScript.MovePlayer();
    }

    public void ShowFailPanel ()
    {
        FailPanel.gameObject.SetActive(true);
    }

    public void RestartButtonTapped ()
    {
        LevelLoader.Instance.GetLevel();
    }

    public void ShowSucessPanel ()
    {
        SucessPanel.gameObject.SetActive(true);
    }

    public void NextLevelButtonTapped()
    {
        LevelLoader.Instance.NextLevel();

    }

}
