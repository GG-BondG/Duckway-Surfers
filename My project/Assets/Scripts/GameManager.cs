using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // This script is for managing all the different stages and scenes of the game, for example losing scene.

    // Bool used to store the information of whether or not the game has ended
    bool gameHasEnded = false;

    public GameObject PauseMenu;
    public GameObject LoseMenu;

    public Text resumeText;

    bool gamePaused = false;

    public Animator animator;

    // This public function will be called by the DuckCollider.cs script when the duck hits something and loses.
    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            print("You lost!");
            gameHasEnded = true;
            animator.SetFloat("Run", 0f);
            Invoke("Lost", 2f);
        }
    }

    public void Lost()
    {
        LoseMenu.SetActive(true);
    }

    public void Restart()
    {
        LoseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "GameScene")
        {
            if (gamePaused)
            {
                StartCoroutine(Resume());
            }
            else
            {
                Pause();
            }
        }
    }
    public IEnumerator Resume()
    {
        resumeText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        resumeText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        resumeText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        resumeText.text = "Go!";
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        yield return new WaitForSecondsRealtime(1.5f);
        resumeText.text = "";
    }
    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("IntroScene");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
