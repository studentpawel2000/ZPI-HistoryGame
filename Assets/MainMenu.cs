using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadMainGame", 7f);
    }

    void LoadMainGame()
    { 
        SceneManager.LoadScene("LoadGameScreen");
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GPS()
    {
        SceneManager.LoadScene("GPSLocation");
    }

    public void QuitGame()
    {
        Application.Quit();
        //Debug.Log("Apllication Quit");
    }
}
