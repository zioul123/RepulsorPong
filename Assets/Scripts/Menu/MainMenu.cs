using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    // Play button; play the game
	public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quit button: quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
