using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject optionPanel;


    public void NewGameBtnClick()
    {
        SaveSystem.DeleteSave();
        GameMaster.Instance.StartNewGame();
        SceneManager.LoadScene("GamePlay");
    }

    public void LoadGameBtnClick()
    {
        GameMaster.Instance.LoadGameData();
        SceneManager.LoadScene("GamePlay");
    }

    public void OptionBtnClick()
    {
        optionPanel.SetActive(true);
    }

    public void ExitBtnClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;       
        #else
            Application.Quit();
        #endif
    }

    public void OptionBackBtnClick()
    {
        optionPanel.SetActive(false);
    }
}
