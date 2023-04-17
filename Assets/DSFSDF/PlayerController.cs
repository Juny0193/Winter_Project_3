using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    PhotonView PV;
    public float MoveSpeed;
    private float OriginSpeed;
    public float SprintSpeed;
    public Transform camform;
    public GameObject cinemachine;
    public CinemachineFreeLook cf;

    private float jumpforce = 3.0f;
    private float gravity = -9.8f;
    Vector3 dir;
    Vector3 dir_xz;
    public Vector3 camoffset;
    private CharacterController characterController;
    private ChatManager chatManager;

    private Animator animator;
    private float rotationSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        PV = this.GetComponent<PhotonView>();
        characterController = GetComponentInChildren<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cinemachine = GameObject.Find("CM FreeLook1");
        cf = cinemachine.GetComponent<CinemachineFreeLook>();

        if (PV.IsMine)
        {
            cf.Follow = this.transform;
            cf.LookAt = this.transform;
        }

        OriginSpeed = MoveSpeed;

        //camoffset.z = -3;
        //camoffset.y = 2;

        chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine && !chatManager.isChatting)
        {
            float xput = Input.GetAxis("Horizontal");
            float zput = Input.GetAxis("Vertical");


            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (!characterController.isGrounded)
            {
                dir.y += gravity * Time.deltaTime;
                animator.SetBool("isJumping", false);
            }
            dir = new Vector3(xput, dir.y, zput);
            dir_xz = new Vector3(dir.x, 0, dir.z);

            characterController.Move(dir * Time.deltaTime * MoveSpeed);
            //MoveAnim(xput,zput);
            transform.LookAt(Vector3.Lerp(transform.position, transform.position + dir_xz, rotationSpeed * Time.deltaTime));

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                MoveSpeed += SprintSpeed;

            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("h", xput);
                animator.SetFloat("v", zput);
            }
            else if (!Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("h", xput * 0.5f);
                animator.SetFloat("v", zput * 0.5f);

                MoveSpeed = OriginSpeed;
            }

        }

    }

    void LateUpdate()
    {
        if (PV.IsMine)
        {
            //camform.position = this.transform.position + camoffset;
            //camform.LookAt(this.transform.position);
            //camform.rotation = Quaternion.Euler(camform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, camform.rotation.eulerAngles.z);
            //camform.position = this.transform.position + camoffset;
        }
    }

    void Jump()
    {
        if (characterController.isGrounded == true)
        {
            dir.y = jumpforce;
            animator.SetBool("isJumping", true);
        }
    }

    void MoveAnim(float x, float z)
    {
        if (x != 0 || z != 0)
        {
            animator.SetFloat("isRunning", x * x + z * z);
        }
    }
}
