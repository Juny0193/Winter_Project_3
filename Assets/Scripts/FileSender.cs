using Photon.Pun;
using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FileSender : MonoBehaviour
{
    public string path;

    private PhotonView photonView;

    private float maxWidth = 1180;
    private float maxHeight = 800;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void OnLoad(FileInfo file)
    {
        byte[] byteTexture = File.ReadAllBytes(file.FullName);

        Texture2D texture2D = new Texture2D(0, 0);
        if (byteTexture.Length > 0)
        {
            texture2D.LoadImage(byteTexture);
        }

        float aspectRatio = (float)texture2D.width / (float)texture2D.height;
        float quadWidth, quadHeight;

        if (texture2D.width > maxWidth)
        {
            quadWidth = maxWidth;
            quadHeight = maxWidth / aspectRatio;
        }
        else if (texture2D.height > maxHeight)
        {
            quadWidth = maxHeight * aspectRatio;
            quadHeight = maxHeight;
        }
        else
        {
            quadWidth = texture2D.width;
            quadHeight = texture2D.height;
        }

        Material material = new Material(Shader.Find("Unlit/Texture"));
        material.mainTexture = texture2D;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
    }

    [PunRPC]
    public void SendFile(FileInfo file)
    {
        byte[] fileBytes = File.ReadAllBytes(file.FullName);

        // Send file to other players
        photonView.RPC("ReceiveFile", RpcTarget.All, fileBytes);
    }

    [PunRPC]
    private void ReceiveFile(byte[] fileBytes)
    {
        Texture2D texture2D = new Texture2D(0, 0);
        if (fileBytes.Length > 0)
        {
            texture2D.LoadImage(fileBytes);
        }

        float aspectRatio = (float)texture2D.width / (float)texture2D.height;
        float quadWidth, quadHeight;

        if (texture2D.width > maxWidth)
        {
            quadWidth = maxWidth;
            quadHeight = maxWidth / aspectRatio;
        }
        else if (texture2D.height > maxHeight)
        {
            quadWidth = maxHeight * aspectRatio;
            quadHeight = maxHeight;
        }
        else
        {
            quadWidth = texture2D.width;
            quadHeight = texture2D.height;
        }

        Material material = new Material(Shader.Find("Unlit/Texture"));
        material.mainTexture = texture2D;

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
    }
}