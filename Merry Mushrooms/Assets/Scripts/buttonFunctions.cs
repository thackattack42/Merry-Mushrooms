using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    public void resume()
    {
        gameManager.instance.UnpausedState();
    }
    public void restart()
    {
        gameManager.instance.UnpausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void quit()
    {
        Application.Quit(); 
    }
    public void respawn()
    {
        gameManager.instance.UnpausedState();
        gameManager.instance.playerScript.Spawn();
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void options()
    {

    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
