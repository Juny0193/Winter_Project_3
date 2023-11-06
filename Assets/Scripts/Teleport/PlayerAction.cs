using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject targetPoint;
    public Transform[] toPoint;
    public GameObject moveText;
    bool isCooldown = false;

    public void Start()
    {
        toPoint[0] = GameObject.Find("Teleport").transform.GetChild(0);
        toPoint[1] = GameObject.Find("Teleport").transform.GetChild(1);
        toPoint[2] = GameObject.Find("Teleport").transform.GetChild(2);
        toPoint[3] = GameObject.Find("Teleport").transform.GetChild(3);
        moveText = GameObject.Find("MoveText");
        moveText.SetActive(false);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Teleport") && GetComponent<PhotonView>().IsMine)
        {
            targetPoint = collision.gameObject;
            moveText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            moveText.SetActive(false);
            targetPoint = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isCooldown)
        {
            StartCoroutine(MoveSpawn());
        }
    }

    IEnumerator MoveSpawn()
    {
        isCooldown = true;
        switch (targetPoint.name) 
        {
            case "Outside":
                gameObject.GetComponent<CharacterController>().enabled = false;
                gameObject.transform.position = toPoint[1].position;
                gameObject.GetComponent<CharacterController>().enabled = true;
                break;
            case "Inside1":
                gameObject.GetComponent<CharacterController>().enabled = false;
                gameObject.transform.position = toPoint[0].position;
                gameObject.GetComponent<CharacterController>().enabled = true;
                break;
            case "Inside2":
                gameObject.GetComponent<CharacterController>().enabled = false;
                gameObject.transform.position = toPoint[3].position;
                gameObject.GetComponent<CharacterController>().enabled = true;
                break;
            case "Inside3":
                gameObject.GetComponent<CharacterController>().enabled = false;
                gameObject.transform.position = toPoint[2].position;
                gameObject.GetComponent<CharacterController>().enabled = true;
                break;
        }

        yield return new WaitForSeconds(0.1f); // 1 second delay

        isCooldown = false;
    }
}
