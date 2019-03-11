using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    Vector3 distance;
    public Transform followThis;
    public float shake = 0;
    void Start()
    {
        distance = transform.position - followThis.position;
    }
    void Update()
    {
        Vector3 randShake = Vector3.zero;
        if(shake > 0.01f){
            randShake = new Vector3((Random.value - 0.5f), (Random.value - 0.5f), (Random.value - 0.5f)) * shake * 2;
        }
        transform.position = followThis.position + distance + randShake;
    }
}
