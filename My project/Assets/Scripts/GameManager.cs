using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // This script is for managing all the different stages and scenes of the game, for example losing scene.

    // Bool used to store the information of whether or not the game has ended
    bool gameHasEnded = false;

    // Float for controlling how much time will be delayed before restarting the game in seconds
    public float delayedRestartTime;

    public GameObject PauseMenu;
    public GameObject LoseMenu;

    bool gamePaused = false;

    // This public function will be called by the DuckCollider.cs script when the duck hits something and loses.
    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            print("You lost!");
            gameHasEnded = true;
            Invoke("Restart", delayedRestartTime);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                StartCoroutine(DelayedResume());
            }
            else
            {
                Pause();
            }
        }
    }
    public IEnumerator DelayedResume()
    {
        yield return new WaitForSecondsRealtime(1f);

        yield return new WaitForSecondsRealtime(1f);

        yield return new WaitForSecondsRealtime(1f);

        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
