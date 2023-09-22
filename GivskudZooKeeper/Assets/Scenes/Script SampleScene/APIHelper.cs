using UnityEngine;
using System.Net;
using System.IO;

public static class APIHelper
{
    public static Jokes GetNewJoke()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.chucknorris.io/jokes/random");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader= new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();

        return JsonUtility.FromJson<Jokes>(json);
    }
}
