using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerImageLoad : MonoBehaviour
{
    public string url;

    private Renderer rend;

    // Start is called before the first frame update
    void Awake()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(TextureLoad());
    }

    string ExtractFileId(string url)
    {
        url = url.Replace("https://drive.google.com/file/d/", "");
        url = url.Replace("/view?usp=sharing", "");

        return url;
    }

    IEnumerator TextureLoad()
    {
        var gdId = ExtractFileId(url);
        var prefix = "http://drive.google.com/uc?export=view&id=";
        url = prefix + gdId;

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            // ºó Material »ý¼º
            Material material = new Material(Shader.Find("Standard"));
            material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            rend.material = material;
        }
    }
}