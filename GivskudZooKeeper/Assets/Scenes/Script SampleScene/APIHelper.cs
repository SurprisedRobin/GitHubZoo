using UnityEngine;
using System.Net;
using System.IO;

public static class APIHelper
{
    public static Response GetNewResponse(string question)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/get-questions/" + question);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader= new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();

        response.Close();

        return JsonUtility.FromJson<Response>(json);
    }
}
