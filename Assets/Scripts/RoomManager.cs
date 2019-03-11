using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject doorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 100; i++){
            GameObject dr = Instantiate(doorPrefab, new Vector3(-2.6f, 1.1f, i * 10), Quaternion.identity);
            GameObject dl = Instantiate(doorPrefab, new Vector3(2.6f, 1.1f, i * 10), Quaternion.identity);
            dl.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
