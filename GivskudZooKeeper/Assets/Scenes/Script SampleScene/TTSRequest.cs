using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using TMPro;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections.Generic;
using System;

public class TTSRequest : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public AudioSource audioSource;

    void Start()
    {
        textBox.text = "";
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ListenForTextChanges());
    }

    IEnumerator ListenForTextChanges()
    {
        string lastText = "";
        while (true)
        {
            if (!string.IsNullOrEmpty(textBox.text) && textBox.text != lastText)
            {
                lastText = textBox.text;
                yield return MakeRequest(textBox.text);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator MakeRequest(string text)
    {
        string url = "http://127.0.0.1:5001/tts";
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(url, ""))
        {
            // Create a JSON object
            var json = new Dictionary<string, string>()
        {
            { "text", text }
        };

            // Convert the JSON object to a string
            var jsonString = JsonConvert.SerializeObject(json);

            // Convert the string to a byte array
            var jsonBytes = Encoding.UTF8.GetBytes(jsonString);

            // Set the request headers
            www.SetRequestHeader("Content-Type", "application/json");

            // Set the upload handler
            www.uploadHandler = new UploadHandlerRaw(jsonBytes);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                UnityEngine.Debug.Log(www.error);
            }
            else
            {
                try
                {
                    audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
                    audioSource.Play();
                }
                catch (InvalidCastException e)
                {
                    UnityEngine.Debug.LogError("InvalidCastException: " + e.Message);
                }
            }
        }
    }

}