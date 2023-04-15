using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScript : MonoBehaviour
{
    public Transform[] transforms = new Transform[4];


    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        transforms[1].transform.position = new Vector3(Random.Range(-20, 0), 0, transforms[1].transform.position.z);
        transforms[2].transform.position = new Vector3(Random.Range(0, 20), 0, transforms[2].transform.position.z);
        transforms[3].transform.position = new Vector3(Random.Range(-20, 0), 0, transforms[3].transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        //startPos = BazierLine(startPos,Movepos,Departure); // a = 시작 지점, b = 곡선 접점(?), c = 도착 지점
        time += Time.deltaTime;

        transform.position = BazierLine(Mathf.Lerp(0, 1, time / 1.0f));
        
        
    }
    
    public Vector3 BazierLine(float t){
        Vector3 bazier = new Vector3(transforms[0].position.x * Mathf.Pow(1 - t, 3) +
                                    transforms[1].position.x * 3 * t * Mathf.Pow(1 - t, 2) +
                                    transforms[2].position.x * 3 * Mathf.Pow(t, 2) * (1 - t) +
                                    transforms[3].position.x * Mathf.Pow(t, 3),
                                     0,
                                     transforms[0].position.z * Mathf.Pow(1 - t, 3) + 
                                     transforms[1].position.z * 3 * t * Mathf.Pow(1 - t, 2) + 
                                     transforms[2].position.z * 3 * Mathf.Pow(t, 2) * (1 - t) +
                                     transforms[3].position.z * Mathf.Pow(t, 3));


        return bazier;
    }
    
}
