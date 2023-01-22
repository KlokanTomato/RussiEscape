using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void EndGame() 
    {
        Application.Quit();
    }
    public void ControllsScreen()
    {
        SceneManager.LoadScene(18);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(4);
    }
}
