using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TTSClient : MonoBehaviour
{
    public string serverURL = "http://localhost:5000/tts";

    public void Speak(string text)
    {
        StartCoroutine(SendRequest(text));
    }

    IEnumerator SendRequest(string text)
    {
        UnityWebRequest request = UnityWebRequest.Post(serverURL, "");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(text));
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Save the audio to a file
            File.WriteAllBytes("audio.wav", request.downloadHandler.data);

            // Load the audio file as an AudioClip
            AudioClip audioClip = LoadAudioClip("audio.wav");

            // Play the audio clip
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    AudioClip LoadAudioClip(string path)
    {
        // Load the audio file into a byte array
        byte[] audioData = File.ReadAllBytes(path);

        // Create an AudioClip from the byte array
        AudioClip audioClip = WavUtility.ToAudioClip(audioData, 0, audioData.Length);

        return audioClip;
    }
}
