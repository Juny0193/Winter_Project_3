using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{

    public class CameraWork : MonoBehaviour
    {
        #region Private Fields
        [Tooltip("�� �Ÿ��� Ÿ���� ���� x-z������ �����Ѵ�.")]
        [SerializeField]
        private float distance = 7.0f;

        [Tooltip("Ÿ�� ���� ī�޶��� ���� ����")]
        [SerializeField]
        private float height = 3.0f;

        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        [Tooltip("���� ��Ʈ��ũ�� ���� �ν��Ͻ�ȭ�Ǵ� �������� ��������� ��� �̸� false�� �����ϰ� �ʿ��Ҷ� �Լ� OnStartFollowing()�� ȣ���մϴ�.")]
        [SerializeField]
        private bool followOnStart = false;

        [Tooltip("ī�޶� ����� ���󰡴� ������")]
        [SerializeField]
        private float smoothSpeed = 0.125f;

        public Transform cameraTransform;
        public Vector3 Camoffset;

        bool isFollowing;

        Vector3 cameraOffset = Vector3.zero;

        #endregion

        #region MonoBehaviour Callback
        // Start is called before the first frame update
        void Start()
        {
            if (followOnStart)
                OnStartFollowing();

            cameraTransform = Camera.main.transform;
            
        }

        void Update()
        {
            Camoffset = cameraTransform.transform.forward;
            Camoffset.y = 0;
            transform.LookAt(transform.position + Camoffset);
            
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if(cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }

            if (isFollowing){
                Follow();
            }
        }
        #endregion

        #region Public Methods
        public void OnStartFollowing()
        {
            cameraTransform = Camera.main.transform;
            isFollowing = true;
            Cut();
        }

        #endregion

        #region priavte Metohods
        public void Follow()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position, smoothSpeed);

            cameraTransform.LookAt(this.transform.position);
        }
        public void Cut()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;
        }

        #endregion
    }
}
