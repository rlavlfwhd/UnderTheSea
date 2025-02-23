using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public GameObject[] gameObjects;
    public GameObject pausePanel;
    public GameObject selectQuitPanel;
    public Transform player;


    public Image backgroundImage; // 배경을 담당하는 UI Image
    public Sprite[] backgroundSprites; // 사용할 배경 이미지 목록


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        foreach (GameObject obj in gameObjects)
        {
            DontDestroyOnLoad(obj);            
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
    }

    void Start()
    {        
        Time.timeScale = 1;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float playerPositionX = player.position.x;

        if (playerPositionX >= 8.6f)
        {
            player.position = new Vector2(-8.6f, player.position.y);
            SceneManager.LoadScene("Stage2");
        }
                

        if (!pausePanel.activeSelf && !selectQuitPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            }
        }

        if(selectQuitPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                selectQuitPanel.SetActive(false);
            }
        }

        string CS = SceneManager.GetActiveScene().name;

        if (CS == "Title")
        {
            Destroy(this.gameObject);
        }
    }

    // 씬이 변경될 때 호출되는 함수
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title")
        {
            DestroyAllPersistObjects();
        }
        else
        {
            UpdateBackground(scene.name);
        }
    }

    void DestroyAllPersistObjects()
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        Destroy(this.gameObject); // GamePlayManager도 삭제
    }

    // 배경을 변경하는 함수
    void UpdateBackground(string sceneName)
    {
        int index = 0;

        switch (sceneName)
        {
            case "GamePlay":
                index = 0;
                break;
            case "Stage2":
                index = 1;
                break;
            case "Stage3":
                index = 2;
                break;
            case "Stage4":
                index = 3;
                break;
            case "Stage5":
                index = 4;
                break;
            case "Stage6":
                index = 5;
                break;
            default:
                index = 0;
                break;
        }

        if (backgroundSprites != null && index < backgroundSprites.Length)
        {
            backgroundImage.sprite = backgroundSprites[index];
        }
    }


    public void ResumeBtnClick()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitBtnClick()
    {
        selectQuitPanel.SetActive(true);
    }

    public void SQLeftBtnClick()
    {
        SceneManager.LoadScene("Title");
    }

    public void SQRightBtnClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;        
        #else
            Application.Quit();
        #endif
    }
}
