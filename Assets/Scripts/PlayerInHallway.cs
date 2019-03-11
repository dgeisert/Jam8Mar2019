using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInHallway : MonoBehaviour
{
    public Door door;
    
    void OnTriggerEnter(){
        door.StartCoroutine("OGOpen");
        Countdown.instance.Refill();
    }
}
