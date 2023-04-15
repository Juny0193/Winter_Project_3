using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObjectSpawner : MonoBehaviourPunCallbacks
{
    public GameObject objectPrefab; // ������ ������Ʈ�� ������
    public Transform spawnPoint; // ������Ʈ ���� ��ġ

    // ������Ʈ ���� ��ư�� ���� �� ȣ��Ǵ� �޼���
    public void CreateObject()
    {
        // ���������κ��� ������Ʈ ����
        GameObject newObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);

        // ������ ������Ʈ�� ������ �����Ͽ� ��Ŷ���� ����
        Vector3 position = newObject.transform.position;
        Quaternion rotation = newObject.transform.rotation;
        Vector3 scale = newObject.transform.localScale;
        Color color = newObject.GetComponent<MeshRenderer>().material.color;
        Texture2D texture = newObject.GetComponent<MeshRenderer>().material.mainTexture as Texture2D;

        object[] data = new object[] { position, rotation, scale, color, texture };

        photonView.RPC("CreateObjectRPC", RpcTarget.All, data);
    }

    // ��Ŷ ���� �ݹ�
    [PunRPC]
    private void CreateObjectRPC(Vector3 position, Quaternion rotation, Vector3 scale, Color color, Texture2D texture)
    {
        // ��Ŷ���� ���۵� ������ ����Ͽ� ������Ʈ ����
        GameObject newObject = Instantiate(objectPrefab, position, rotation);
        newObject.transform.localScale = scale;
        newObject.GetComponent<MeshRenderer>().material.color = color;
        newObject.GetComponent<MeshRenderer>().material.mainTexture = texture;
    }
}