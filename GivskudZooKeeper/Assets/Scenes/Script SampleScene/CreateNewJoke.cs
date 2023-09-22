using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateNewJoke : MonoBehaviour
{
    public TextMeshProUGUI joketext;

    public void NewJoke()
    {
        Jokes j = APIHelper.GetNewJoke();
        joketext.text = j.value;
    }
}
