using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
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
        SceneManager.LoadScene(2);
        GameParameter.ResetCurrentStage();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
        // To reset the music
        GameObject.FindGameObjectWithTag(GameManager.AUDIO_TAG).GetComponent<Audio>().KillMusic();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
