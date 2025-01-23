using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title2Manager : MonoBehaviour
{
    public void NewGameBtnClick()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void BackBtnClick()
    {
        SceneManager.LoadScene("Title");
    }
}
