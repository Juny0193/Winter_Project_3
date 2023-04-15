using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using UnityEngine.EventSystems;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public GameObject spawner;

    void Start()
    {
        GameObject PI = PhotonNetwork.Instantiate("Player", new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z), Quaternion.identity);
        Debug.Log("생성완료");
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("nono");
            return;
        }

        Debug.LogFormat("PhotonNetWork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
            //LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            //LoadArena();
        }
    }
}
