using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Words : MonoBehaviour
{
    public List<string> words;
    public string chosenWord;
    public int chosenWordIndex;
    public char[] letters;

    public void RandomChoice()
    {
        //random index
        chosenWordIndex = Random.Range(0, words.Count);
        chosenWord = words[chosenWordIndex];
        //breaks string down into char array
        letters = chosenWord.ToCharArray();
        //just so you cant get the same word twice in a row
        words.RemoveAt(chosenWordIndex);
    }
}
