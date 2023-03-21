using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Interface : MonoBehaviour
{
    public void OpenMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OpenMapScene()
    {
        SceneManager.LoadScene("Location-basedGame");
    }

    public void OpenGamecene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
