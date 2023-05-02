using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PhotoFrame : MonoBehaviour
{
    public GameObject text;
    private bool ontrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PhotonView>().IsMine)
                text.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
                text.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    FileManager.Instance.OpenExplorer();
                }
            }
        }
    }
}
