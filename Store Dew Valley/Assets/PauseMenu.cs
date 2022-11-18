using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject quitMenu;
    public GameObject optionsMenu;
    public GameObject saveMenu;
    PlayerUIController uIController;

    public void Start()
    {
        uIController = FindObjectOfType<PlayerUIController>();
    }

    public void ResumeGame()
    {
        AudioManager.instance.PlayClip("ButtonPress");
        uIController.PauseGame();
    }
    public void OptionsToggle()
    {
        AudioManager.instance.PlayClip("ButtonPress");
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }
    public void ToggleSaveMenu()
    {
        AudioManager.instance.PlayClip("ButtonPress");
        saveMenu.SetActive(!saveMenu.activeSelf);
    }
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }
    public void AreYouSure()
    {
        AudioManager.instance.PlayClip("ButtonPress");
        quitMenu.SetActive(!quitMenu.activeSelf);
    }
}
