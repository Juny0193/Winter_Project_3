using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject targetPoint;
    public Transform[] toPoint;

    public void Start()
    {
        toPoint[0] = GameObject.Find("Teleport").transform.GetChild(0);
        toPoint[1] = GameObject.Find("Teleport").transform.GetChild(1);
        toPoint[2] = GameObject.Find("Teleport").transform.GetChild(2);
        toPoint[3] = GameObject.Find("Teleport").transform.GetChild(3);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Teleport"))
        {
            targetPoint = collision.gameObject;
            Debug.Log(targetPoint.name);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        targetPoint = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(MoveSpawn());
            
        }
    }

    IEnumerator MoveSpawn()
    {        
        switch (targetPoint.name) 
        {
            case "Outside":
                gameObject.transform.position = toPoint[1].position;
                break;
            case "Inside1":
                gameObject.transform.position = toPoint[0].position;
                break;
            case "Inside2":
                gameObject.transform.position = toPoint[3].position;
                break;
            case "Inside3":
                gameObject.transform.position = toPoint[2].position;
                break;
        }

        yield return null;

        
    }
}
