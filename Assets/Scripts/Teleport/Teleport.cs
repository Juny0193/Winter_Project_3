using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetPoint;
    public GameObject[] toPoint;

    public bool isOutside = true;
    public bool isInside = false;

    new Collider[] collider;

    private void OnTriggerStay(Collider collision)
    {
        collider = GetComponentsInChildren<Collider>();

        if (collision.CompareTag("Player"))
        {
            targetPoint = collision.gameObject;
            //Debug.Log(collider.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        targetPoint = null;
        collider = null;
    }

    public void MoveSpawn()
    {
        StartCoroutine("TeleportSpace");
    }

    IEnumerator TeleportSpace()
    {
        if (targetPoint != null)
        {
            foreach (Collider collider in collider)
            {
                if (collider.gameObject.name.Equals("Outside") && targetPoint != null)
                {
                    targetPoint.transform.position = toPoint[1].transform.position;
                    yield return new WaitForSeconds(0.1f);
                    break;
                }

                else if (collider.gameObject.name.Equals("Inside1") && targetPoint != null)
                {
                    targetPoint.transform.position = toPoint[0].transform.position;
                    yield return new WaitForSeconds(0.1f);
                    break;
                }

                else if (collider.gameObject.name.Equals("Inside2") && targetPoint != null)
                {
                    targetPoint.transform.position = toPoint[3].transform.position;
                    yield return new WaitForSeconds(0.1f);
                    break;
                }

                else if (collider.gameObject.name.Equals("Inside3") && targetPoint != null)
                {
                    targetPoint.transform.position = toPoint[2].transform.position;
                    yield return new WaitForSeconds(0.1f);
                    break;
                }
            }
        }       
    }

}

