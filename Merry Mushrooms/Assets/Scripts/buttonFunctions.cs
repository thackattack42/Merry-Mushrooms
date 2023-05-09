using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    void resume()
    {
        gameManager.instance.UnpausedState();
    }
    void restart()
    {
        gameManager.instance.UnpausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void quit()
    {
        Application.Quit(); 
    }

    void respawn()
    {
        
    }
}
