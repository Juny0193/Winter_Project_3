using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ChatManager : MonoBehaviourPunCallbacks
{
    enum ChatType
    {
        World,
        Whipser
    }

    enum Guild
    {
        Art2D,
        Art3D,
        Programming,
        Plan,
        QA,
        Youtube,
        Novel
    }

    public GameObject m_Content;
    public GameObject worldButton;
    public GameObject whisperButton;
    public TMP_Text typeText;
    public TMP_InputField m_inputField;
    public TMP_InputField targetInputField;

    public bool isChatting;

    ChatType chatType = ChatType.World;
    Guild guildType = Guild.Plan;

    PhotonView photonview;
    GameObject m_ContentText;
 
    string userName;
    string targetPlayerName;
    Dictionary<int, string> playerNames;
 
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        m_ContentText = m_Content.transform.GetChild(0).gameObject;
        photonview = GetComponent<PhotonView>();

        playerNames = new Dictionary<int, string>();

        string selectedOption = GameManager.Instance.selectedOption;

        if (selectedOption == "<color=#00FF00>메이커</color> ( 기획 )")
            guildType = Guild.Plan;
        else if (selectedOption == "<color=#FF0000>페인터</color> ( 2D / 원화 )")
            guildType = Guild.Art2D;
        else if (selectedOption == "<color=#0000FF>조각가</color> ( 3D / 모델링 )")
            guildType = Guild.Art3D;
        else if (selectedOption == "<color=#FFA500>스파이럴</color> ( QA / 분석 )")
            guildType = Guild.QA;
        else if (selectedOption == "<color=#FF007F>수트리밍</color> ( 유튜브 / 컨텐츠 )")
            guildType = Guild.Youtube;
        else if (selectedOption == "<color=#8A2BE2>크래프터</color> ( 프로그래밍 )")
            guildType = Guild.Programming;
        else if (selectedOption == "<color=#A52A2A>스토리텔러</color> ( 소설 / 시나리오 )")
            guildType = Guild.Novel;

        userName = PhotonNetwork.LocalPlayer.NickName;
        AddChatMessage(userName + " 님이 입장했습니다.");

        // 모든 플레이어의 닉네임을 기록
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            string playerName = player.NickName;
            int playerActorNumber = player.ActorNumber;
            playerNames.Add(playerActorNumber, playerName);
        }
    }
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // 엔터를 누르면 채팅창 활성화/비활성화
        {
            if (isChatting)
            {
                m_inputField.DeactivateInputField();

                // Reset the selected object when the chat field is deactivated
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                m_inputField.ActivateInputField();

                // Set the selected object to the chat input field when it is activated
                EventSystem.current.SetSelectedGameObject(m_inputField.gameObject);
            }
        }

        if(m_inputField.isFocused == true)
            isChatting = true;
        else
            isChatting = false;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string newPlayerName = newPlayer.NickName;
        AddChatMessage(newPlayerName + " 님이 입장했습니다.");
        playerNames.Add(newPlayer.ActorNumber, newPlayer.NickName);
    }

    public void OnEndEditEvent() // 채팅 입력 함수 (엔터)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (chatType == ChatType.World && m_inputField.text != "")
            {
                string strMessage = getGuild() + " " + userName + " : " + m_inputField.text;
                photonview.RPC("RPC_Chat", RpcTarget.All, strMessage);
                m_inputField.text = "";
            }

            else if (chatType == ChatType.Whipser && m_inputField.text != "")
            {
                // 대상 플레이어의 ActorNumber를 가져오기
                int targetActorNumber = -1;
                string message = m_inputField.text;

                foreach (KeyValuePair<int, string> pair in playerNames)
                {
                    if (pair.Value == targetPlayerName)
                    {
                        targetActorNumber = pair.Key;
                        break;
                    }
                }

                if (targetActorNumber != -1)
                {
                    Photon.Realtime.Player targetPlayer = null;

                    foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                    {
                        if (player.ActorNumber == targetActorNumber)
                        {
                            targetPlayer = player;
                            break;
                        }
                    }

                    if (targetPlayer != null)
                    {
                        if(targetPlayer.NickName == userName)
                        {
                            GameObject _goText = Instantiate(m_ContentText, m_Content.transform);
                            _goText.GetComponent<TextMeshProUGUI>().text = "<color=red>자기 자신에게는 귓속말을 보낼 수 없습니다.</color>";
                            m_inputField.text = "";
                        }
                        else
                        {
                            // 귓속말 보내기
                            photonView.RPC("ReceivePrivateMessage", targetPlayer, message);
                            GameObject goText = Instantiate(m_ContentText, m_Content.transform);
                            goText.GetComponent<TextMeshProUGUI>().text = $"<color=#FFC0CB>{targetPlayerName}님에게: {message}";
                            m_inputField.text = "";
                        }
                    }
                }
                else
                {
                    GameObject goText = Instantiate(m_ContentText, m_Content.transform);
                    goText.GetComponent<TextMeshProUGUI>().text = "<color=red>접속 중이 아니거나 존재하지 않는 유저입니다.</color>";
                    m_inputField.text = "";
                }
            }
        }
    }

    public void OnEndEditEventButton() // 채팅 입력 함수 (버튼)
    {
        if (chatType == ChatType.World && m_inputField.text != "")
            {
                string strMessage = getGuild() + " " + userName + " : " + m_inputField.text;
                photonview.RPC("RPC_Chat", RpcTarget.All, strMessage);
                m_inputField.text = "";
            }

            else if (chatType == ChatType.Whipser && m_inputField.text != "")
            {
                // 대상 플레이어의 ActorNumber를 가져오기
                int targetActorNumber = -1;
                string message = m_inputField.text;

                foreach (KeyValuePair<int, string> pair in playerNames)
                {
                    if (pair.Value == targetPlayerName)
                    {
                        targetActorNumber = pair.Key;
                        break;
                    }
                }

                if (targetActorNumber != -1)
                {
                    Photon.Realtime.Player targetPlayer = null;

                    foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                    {
                        if (player.ActorNumber == targetActorNumber)
                        {
                            targetPlayer = player;
                            break;
                        }
                    }

                    if (targetPlayer != null)
                    {
                        if(targetPlayer.NickName == userName)
                        {
                            GameObject _goText = Instantiate(m_ContentText, m_Content.transform);
                            _goText.GetComponent<TextMeshProUGUI>().text = "<color=red>자기 자신에게는 귓속말을 보낼 수 없습니다.</color>";
                            m_inputField.text = "";
                        }
                        else
                        {
                            // 귓속말 보내기
                            photonView.RPC("ReceivePrivateMessage", targetPlayer, message);
                            GameObject goText = Instantiate(m_ContentText, m_Content.transform);
                            goText.GetComponent<TextMeshProUGUI>().text = $"<color=#FFC0CB>{targetPlayerName}님에게: {message}";
                            m_inputField.text = "";
                        }
                    }
                }
                else
                {
                    GameObject goText = Instantiate(m_ContentText, m_Content.transform);
                    goText.GetComponent<TextMeshProUGUI>().text = "<color=red>접속 중이 아니거나 존재하지 않는 유저입니다.</color>";
                    m_inputField.text = "";
                }
            }

        m_inputField.DeactivateInputField();
        // Reset the selected object when the chat field is deactivated
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SelectType()
    {
        worldButton.SetActive(true);
        whisperButton.SetActive(true);
    }

    public void SelectWorld()
    {
        worldButton.SetActive(false);
        whisperButton.SetActive(false);
        typeText.text = "월드";
        chatType = ChatType.World;
    }

    public void SelectWhisper()
    {
        worldButton.SetActive(false);
        whisperButton.SetActive(false);
        targetInputField.gameObject.SetActive(true);
        typeText.text = "";
        chatType = ChatType.Whipser;
    }

    public void EnterTarget()
    {
        if(targetInputField.text == "")
        {
            targetInputField.gameObject.SetActive(false);
            SelectWorld();
            return;
        }

        typeText.text = targetInputField.text;
        targetPlayerName = targetInputField.text;
        targetInputField.text = "";
        targetInputField.gameObject.SetActive(false);
    }

    public string getGuild()
    {
        if (guildType == Guild.Plan)
            return "<color=#00FF00>[메이커]</color>";
        else if (guildType == Guild.Art2D)
            return "<color=#FF0000>[페인터]</color>";
        else if (guildType == Guild.Art3D)
            return "<color=#0000FF>[조각가]</color>";
        else if (guildType == Guild.QA)
            return "<color=#FFA500>[스파이럴]</color>";
        else if (guildType == Guild.Youtube)
            return "<color=#FF007F>[수트리밍]</color>";
        else if (guildType == Guild.Programming)
            return "<color=#8A2BE2>[크래프터]</color>";
        else
            return "<color=#A52A2A>[스토리텔러]</color>";
    }
 
    void AddChatMessage(string message) // 채팅 출력 함수
    {
        GameObject goText = Instantiate(m_ContentText, m_Content.transform);
        goText.GetComponent<TextMeshProUGUI>().text = message;
    }

    void AddPrivateChatMessage(string message) // 귓속말 출력 함수
    {
        GameObject goText = Instantiate(m_ContentText, m_Content.transform);
        goText.GetComponent<TextMeshProUGUI>().text = message;
    }
 
    [PunRPC]
    void RPC_Chat(string message)
    {
        AddChatMessage(message);
    }

    [PunRPC]
    void ReceivePrivateMessage(string message, PhotonMessageInfo info)
    {
        string senderName = info.Sender.NickName;
        string formattedMessage = $"<color=#FFC0CB>{senderName}님의 귓속말: {message}</color>";
        AddChatMessage(formattedMessage);

        // 상대방에게도 메시지 전송
        photonView.RPC("ReceivePrivateMessage", info.Sender, message, info);
    }

    /*[PunRPC]
    void ReceivePrivateMessage(string message)
    {
        AddPrivateChatMessage(message);
    }*/
}