using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject optionCanvas;
    public GameObject quitCanvas;
    public GameObject nameCanvas;
    public GameObject guildCanvas;
    public GameObject screenPanel;
    public GameObject soundPanel;
    public GameObject controlPanel;

    public void GameStart()
    {
        mainCanvas.SetActive(false);
        nameCanvas.SetActive(true);
    }

    public void OpenOption()
    {
        mainCanvas.SetActive(false);
        OpenScreenPanel();
        optionCanvas.SetActive(true);
    }

    public void GoMain()
    {
        optionCanvas.SetActive(false);
        quitCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void OpenQuit()
    {
        mainCanvas.SetActive(false);
        quitCanvas.SetActive(true);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenGuild()
    {
        nameCanvas.SetActive(false);
        guildCanvas.SetActive(true);
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
}
