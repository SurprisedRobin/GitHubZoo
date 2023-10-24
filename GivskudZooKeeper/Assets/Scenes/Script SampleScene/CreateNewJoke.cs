using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewResponse : MonoBehaviour
{
    public TextMeshProUGUI joketext;
    public TextMeshProUGUI question;


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            NewResponse();
        }
    }


    public void NewResponse()
    {
        Response j = APIHelper.GetNewResponse(question.text);
        joketext.text = j.answer;
    }
}
