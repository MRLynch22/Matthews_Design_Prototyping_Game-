using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void Scene0()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Scene1()
    {
        SceneManager.LoadScene("Game");
    }
    public void exitgame()
    {
        Debug.Log("You Quit the game");
        Application.Quit();
    }
}
