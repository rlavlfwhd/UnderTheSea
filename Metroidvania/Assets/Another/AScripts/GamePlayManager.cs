using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject selectQuitPanel;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
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
