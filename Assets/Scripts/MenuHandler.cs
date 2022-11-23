using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public Canvas pause;
    public Canvas endScreen;

    public Text screenText;
    
    public bool paused;

    public void Play()
    {
        //Enters you into the game from the main menu
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        //from either within or outside of the game state you can quit application
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void BackToMain()
    {
        //Goes back to main menu from game scene
        SceneManager.LoadScene(0);
    }

    public void LoseScreen()
    {
        //Shows a loose screen on the screen
        paused = true;
        endScreen.GameObject().SetActive(true);
        screenText.text = "Better Luck Next Time Looser!!!";
    }

    public void WinScreen()
    {
        //Shows a win screen on the screen
        paused = true;
        endScreen.GameObject().SetActive(true);
        screenText.text = "Congratualtions!!!";
    }

    public void PlayAgain()
    {
        //Play again from the main menu
        SceneManager.LoadScene(1);
    }
}
