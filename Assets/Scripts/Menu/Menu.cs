using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayStage(int stage)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + stage);
        GameParameter.SetCurrentStage(stage);
    }

    public void ReloadStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayNextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameParameter.SetCurrentStage(GameParameter.GetCurrentStage() + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void BackToStageSelectionMenu()
    {
        SceneManager.LoadScene(1);
        GameParameter.ResetCurrentStage();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
