using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using TMPro;
using SimpleFileBrowser;

public class FileManager : MonoBehaviour
{
    [SerializeField]
    private FileSender fileSender;

    private string path;
    private float maxWidth = 1180;
    private float maxHeight = 800;

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static FileManager _instance;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static FileManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(FileManager)) as FileManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

        private void Start()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".jpg");
    }

    public void OpenExplorer()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
/*#if UNITY_EDITOR
        path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png,jpg");
                GetImage();
#else
                StartCoroutine(ShowLoadDialogCoroutine());
#endif*/
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            path = FileBrowser.Result[0];

            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            // Read the bytes of the first file via FileBrowserHelpers
            // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

            // Or, copy the first file to persistentDataPath
            string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);

            GetImage();
        }
    }

    private void GetImage()
    {
        if (path != null)
        {
            fileSender.OnLoad(new FileInfo(path));
            fileSender.path = path;
            fileSender.SendFile(new FileInfo(path));
        }
    }
}