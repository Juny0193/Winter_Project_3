using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [SerializeField]
    private byte maxPlayer = 5;

    [SerializeField]
    private GameObject controlPanel;

    [SerializeField]
    private GameObject progressLabel;

    [SerializeField]
    private GameObject SelectImage;

    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private GameObject loadingColor;

    [SerializeField]
    private GameObject loadingBlack;

    #endregion

    #region Priavate Fields
    string gameVersion = "1";

    #endregion

    private bool isConnecting;

    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        //Screen.SetResolution(1920, 1080, false);

        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    private void Update()
    {
        // 씬 로딩 상태에 따라 로딩 바를 업데이트
        if (PhotonNetwork.LevelLoadingProgress < 1)
        {
            loadingColor.GetComponent<Image>().fillAmount = PhotonNetwork.LevelLoadingProgress;
        }
    }

    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        SelectImage.SetActive(false);
        loadingBlack.SetActive(true);
        loadingColor.SetActive(true);
        loadingPanel.SetActive(true);

        isConnecting = true;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayer});
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("Load the 'Room for 1' ");

            PhotonNetwork.LoadLevel("Room1");
        }
    }

    
}
