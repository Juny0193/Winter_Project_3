using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerNameInputField : MonoBehaviour
{

    const string playerNamePrefKey = "PlayerName";
    // Start is called before the first frame update
    void Start()
    {
        string defaulName = string.Empty;
        TMP_InputField _inputField = this.GetComponent<TMP_InputField>();
        _inputField.onValueChanged.AddListener(SetPlayerName);

        if (PlayerPrefs.HasKey(playerNamePrefKey))
        {
           defaulName = PlayerPrefs.GetString(playerNamePrefKey);
           _inputField.text = defaulName;
        }
        PhotonNetwork.NickName = defaulName;
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
