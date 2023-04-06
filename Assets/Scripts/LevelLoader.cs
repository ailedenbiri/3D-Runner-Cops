using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    private int currentLevel;
    private int maxLevel;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        maxLevel = 2;
        DontDestroyOnLoad(this.gameObject);
        GetLevel();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void GetLevel()
    {

        currentLevel = PlayerPrefs.GetInt("keyLevel", 1);
        LoadLevel();
    }

    private void LoadLevel()
    {
        string levelName = "LevelScene" + currentLevel;
        SceneManager.LoadScene(levelName);
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel > maxLevel)
        {
            currentLevel = 1;
        }

        PlayerPrefs.SetInt("keyLevel", currentLevel);
        LoadLevel();
    }

}
