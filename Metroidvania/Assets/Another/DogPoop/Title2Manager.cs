using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title2Manager : MonoBehaviour
{
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

    public void BackBtnClick()
    {
        SceneManager.LoadScene("Title");
    }
}
