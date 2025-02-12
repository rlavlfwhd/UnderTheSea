using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    public GameData gameData;

    

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        LoadGameData();        
    }

    public void StartNewGame()
    {
        gameData = new GameData();
        gameData.isStoryWatched = true;
        gameData.dPlayer = new DPlayer
        {
            pMHP = 100f,
            pHP = 100f,
            pPos = new Vector2(0, 0),
            playTime = 0f
        };
    }

    public void LoadGameData()
    {
        GameData loadedData = SaveSystem.Load();
        if(loadedData != null)
        {
            gameData = loadedData;
        }
        else
        {
            StartNewGame();
        }
    }

    public void SaveGameData()
    {
        SaveSystem.Save(gameData);
    }

    
}
