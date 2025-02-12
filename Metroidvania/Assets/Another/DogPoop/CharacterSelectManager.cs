using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    public Button button;
    public Button button2;
    
    public void buttonClick()
    {
        button.interactable = false;
    }

    public void button2Click()
    {
        button2.interactable = false;
    }

    public void CancelBtnClick()
    {
        SceneManager.LoadScene("Title2");
    }

    public void ConfirmBtnClick()
    {
        if (!button.interactable && !button2.interactable)
        {
            SceneManager.LoadScene("GamePlay");
            Debug.Log("yyyy");
        }
    }
}
