using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    // 새로운 캐릭터 이동 변수
    private Rigidbody rigidbody;
    private Vector3 newDir = Vector3.zero;

    public float rotateSpeed = 3f;
    public float speed = 10f;

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        newDir.x = Input.GetAxis("Horizontal");
        newDir.y = Input.GetAxis("Vertical");

        if (newDir != Vector3.zero)
        {
            transform.forward = newDir;
        }
    }

    private void FixedUpdate()
    {
        if (newDir != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, newDir, rotateSpeed * Time.deltaTime);
        }

        rigidbody.MovePosition(this.gameObject.transform.position + newDir * speed * Time.deltaTime);
    }
}
