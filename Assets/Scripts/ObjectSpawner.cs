using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObjectSpawner : MonoBehaviourPunCallbacks
{
    public GameObject objectPrefab; // 생성할 오브젝트의 프리팹
    public Transform spawnPoint; // 오브젝트 생성 위치

    // 오브젝트 생성 버튼을 누를 때 호출되는 메서드
    public void CreateObject()
    {
        // 프리팹으로부터 오브젝트 생성
        GameObject newObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);

        // 생성된 오브젝트의 정보를 추출하여 패킷으로 전송
        Vector3 position = newObject.transform.position;
        Quaternion rotation = newObject.transform.rotation;
        Vector3 scale = newObject.transform.localScale;
        Color color = newObject.GetComponent<MeshRenderer>().material.color;
        Texture2D texture = newObject.GetComponent<MeshRenderer>().material.mainTexture as Texture2D;

        object[] data = new object[] { position, rotation, scale, color, texture };

        photonView.RPC("CreateObjectRPC", RpcTarget.All, data);
    }

    // 패킷 수신 콜백
    [PunRPC]
    private void CreateObjectRPC(Vector3 position, Quaternion rotation, Vector3 scale, Color color, Texture2D texture)
    {
        // 패킷에서 전송된 정보를 사용하여 오브젝트 생성
        GameObject newObject = Instantiate(objectPrefab, position, rotation);
        newObject.transform.localScale = scale;
        newObject.GetComponent<MeshRenderer>().material.color = color;
        newObject.GetComponent<MeshRenderer>().material.mainTexture = texture;
    }
}