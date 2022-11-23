using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    #region Variables and references ----------------------------------------------------------------

    public List<GameObject> wordLetters;
    public GameObject[] bodyParts;
    public GameObject letterGO;
    public Words wordScript;
    public GameObject letterHolder;
    public InputField input;
    public List<string> guessedLetters;
    public List<GameObject> covers;
    public int correctGuesses;
    public int incorrectGuessedLetters;
    public MenuHandler menu;
    public int letterIndex;
    public string guessedLetter;
    public Text guessedText;
    public string guessedLettersText;

    #endregion


    private void Start()
    {
        menu.paused = false;
        bodyParts[incorrectGuessedLetters].SetActive(true);
        correctGuesses = 0;
        //Empties the letter array
        wordScript.letters = Array.Empty<char>();
        //Run to select the random letter
        wordScript.RandomChoice();
        for (var i = 0; i < wordScript.letters.Length; i++)
        {
            //creating a clone of the letter prefab
            var clone = Instantiate(letterGO, letterHolder.transform);
            wordLetters.Add(letterGO);
            //changing the text of the instantiated letterGO
            clone.GetComponentInChildren<Text>().text = wordScript.chosenWord[i].ToString();
        }
        foreach (var obj in GameObject.FindGameObjectsWithTag("Cover")) covers.Add(obj);
    }

    public void Update()
    {
        bodyParts[incorrectGuessedLetters].SetActive(true);
        //gets the enter key
        if (Input.GetKeyDown(KeyCode.Return))
        {
            InputLetter();
        }
        //if you press esc then you pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
        if (correctGuesses == wordScript.chosenWord.Length)
        {
            //win conditions
            input.interactable = false;
            menu.WinScreen();
        }
        if (incorrectGuessedLetters == bodyParts.Length)
        {
            //loose conditions
            input.interactable = false;
            foreach (var cover in covers)
            {
                cover.SetActive(false);
            }
            menu.LoseScreen();
        }

        GuessedLetters();

    }

    private void InputLetter()
    {
        //saves last guessed letter
        guessedLetter = input.text;
        if (!guessedLetters.Contains(guessedLetter))
        {
            guessedLetters.Add(guessedLetter);
            //go through the string to see if you have guessed correctly
            for (var i = 0; i < wordScript.chosenWord.Length; i++)
            {
                var letter = wordScript.chosenWord[i];
                if (letter.ToString() != guessedLetter) continue;
                correctGuesses++;
                var index = wordLetters[i];
                input.text = "";
                letterIndex = i;
                RemoveCover();
            }
            //if you haven't already guessed you will lose another point
            if (wordScript.chosenWord.Contains(guessedLetter)) return;
            incorrectGuessedLetters++;
            input.text = "";
        }

        else if (guessedLetters.Contains(guessedLetter))
        {
            input.text = "";
        }
    }

    private void RemoveCover()
    {
        //sets the cover to false
        covers[letterIndex].SetActive(false);
    }

    private void PauseMenu()
    {
        //if your paused you cant interact with the game anymore
        switch (menu.paused)
        {
            case true:
                menu.paused = false;
                menu.pause.GameObject().SetActive(false);
                input.interactable = true;
                break;
            case false:
                menu.paused = true;
                menu.pause.GameObject().SetActive(true);
                input.interactable = false;
                break;
        }
        
    }
    public void Resume()
    {
        //unpauses the game and returns you into the game
        menu.paused = false;
        menu.pause.GameObject().SetActive(false);
        input.interactable = true;
    }
    
    public void GuessedLetters()
    {
        for (int i = 0; i < guessedLetters.Count; i++)
        {
            guessedLettersText = string.Join(", ", guessedLetters);
        }

        guessedText.text = guessedLettersText;
    }

}
