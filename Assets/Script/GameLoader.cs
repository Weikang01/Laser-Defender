using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {

    [SerializeField] float waitForGameOver = 2f;
    [SerializeField] [Range(0,1)] float slowMoAfterDied = 0.1f;
    [SerializeField] float gameSpeed = 1f;

    private void Start()
    {
        Time.timeScale = gameSpeed;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void GameOver()
    {
        StartCoroutine(loadGameOver());
    }

    IEnumerator loadGameOver()
    {
        yield return new WaitForSeconds(waitForGameOver);
        Time.timeScale = slowMoAfterDied * gameSpeed;
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
