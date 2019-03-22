using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    DataManager dataManager;

    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitForLoadScene("GameOver"));
    }

    public void LoadFirstLevel()
    {
        
        SceneManager.LoadScene("Level_1");
    }

    public void LoadNextLevel(string level)
    {
        StartCoroutine(WaitForLoadScene(level));
    }

    IEnumerator WaitForLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevelFromMenu(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadStartMenu()
    {

        dataManager.ResetGame();
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void LoadUpgrade()
    {
        SceneManager.LoadScene("Upgrade");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public int GetLevelIndex()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        var intScene = sceneName.Remove(0, 6);
        return System.Convert.ToInt32(intScene);
    }


}
