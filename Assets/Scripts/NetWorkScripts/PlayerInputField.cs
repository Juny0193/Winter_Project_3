using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInputField : MonoBehaviour
{
    public TMP_InputField _inputField;

    const string playerNamePrefKey = "PlayerName";
    // Start is called before the first frame update
    void Start()
    {
        string defaultName = string.Empty;

        _inputField = GetComponent<TMP_InputField>();
        _inputField.onValueChanged.AddListener(SetPlayerName);

        if (PlayerPrefs.HasKey(playerNamePrefKey))                  //저장된 닉네임이 있다면 실행
        {
            defaultName = PlayerPrefs.GetString(playerNamePrefKey);
            _inputField.text = defaultName;
        }
     

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        
        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}
