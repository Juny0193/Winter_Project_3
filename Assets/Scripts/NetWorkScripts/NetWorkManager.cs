using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetWorkManager : MonoBehaviourPunCallbacks 
{
    public GameObject chatUI;
    public GameObject loginUI;

    [SerializeField]
    private byte m_MaxPlayer = 8;

    private RoomOptions roomOptions = new RoomOptions();
    

    private string gamever = "1";

    private void Start()
    {
        roomOptions.MaxPlayers = m_MaxPlayer;
    }

    public override void OnConnectedToMaster() //포톤 네트워크에 접속하면 가장 먼저 콜백되는 함수
    {
        Debug.Log("Connected to MasterServer!");
    }

    public override void OnJoinedRoom() //룸에 입장시 콜백되는 함수
    {
        Debug.Log("Welcome this room!");
        chatUI.SetActive(true);
        loginUI.SetActive(false);
    }

    public void connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gamever;
            PhotonNetwork.ConnectUsingSettings();
        }
    }


    public override void OnJoinRandomFailed(short returnCode, string message) //랜덤룸 입장 실패시 콜백되는 함수
    {
        Debug.Log("You can't join random room. Create a room forcibly.");
        PhotonNetwork.CreateRoom("null", roomOptions);
    }
}
