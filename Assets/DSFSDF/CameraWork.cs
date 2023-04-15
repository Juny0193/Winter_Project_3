using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{

    public class CameraWork : MonoBehaviour
    {
        #region Private Fields
        [Tooltip("이 거리는 타겟의 로컬 x-z영역을 설정한다.")]
        [SerializeField]
        private float distance = 7.0f;

        [Tooltip("타겟 위에 카메라의 높이 설정")]
        [SerializeField]
        private float height = 3.0f;

        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        [Tooltip("포톤 네트워크에 의해 인스턴스화되는 프리팹의 구성요소인 경우 이를 false로 설정하고 필요할때 함수 OnStartFollowing()을 호출합니다.")]
        [SerializeField]
        private bool followOnStart = false;

        [Tooltip("카메라가 대상을 따라가는 스무딩")]
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
