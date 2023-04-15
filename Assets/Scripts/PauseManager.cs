using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject optionCanvas;
    public GameObject quitCanvas;
    public GameObject screenPanel;
    public GameObject soundPanel;
    public GameObject controlPanel;

    public bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
            ResumeGame();
        else if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
            PauseGame();
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        optionCanvas.SetActive(false);
        quitCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        isPaused = true;
        optionCanvas.SetActive(false);
        quitCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void OpenOption()
    {
        pauseCanvas.SetActive(false);
        OpenScreenPanel();
        optionCanvas.SetActive(true);
    }

    public void OpenQuit()
    {
        pauseCanvas.SetActive(false);
        quitCanvas.SetActive(true);
    }

    public void OpenScreenPanel()
    {
        screenPanel.SetActive(true);
        soundPanel.SetActive(false);
        controlPanel.SetActive(false);
    }

    public void OpenSoundPanel()
    {
        screenPanel.SetActive(false);
        soundPanel.SetActive(true);
        controlPanel.SetActive(false);
    }

    public void OpenControlPanel()
    {
        screenPanel.SetActive(false);
        soundPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void GoMain()
    {
        // 서버와 연결 해제
        PhotonNetwork.Disconnect();

        // 메인화면으로 이동
        SceneManager.LoadScene("StartScene");
    }
}
