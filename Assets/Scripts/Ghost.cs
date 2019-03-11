using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Ghost : MonoBehaviour
{
    int playPoint = 0;
    public float delay = 1;

    ThirdPersonCharacter tpc;
    int attack = -1;
    bool attacking = false;
    void Start()
    {
        tpc = GetComponent<ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > delay)
        {
            if (Recorder.instance.moves.Count > playPoint)
            {
                tpc.Move(Recorder.instance.moves[playPoint], false, false);
                playPoint++;
            }
            Vector3 pos = Recorder.instance.pos[Recorder.instance.pos.Count - Mathf.RoundToInt(1/Time.fixedDeltaTime * delay)];
            transform.position = new Vector3(pos.x, 0, pos.z);
            if(Mathf.RoundToInt(pos.y) >= 0 && !attacking){
                attack = Mathf.RoundToInt(pos.y);
                tpc.Attack(attack);
                attacking = true;
            }
            if(Mathf.RoundToInt(pos.y) == -1){
                attacking = false;
                attack = Mathf.RoundToInt(pos.y);
            }
        }
    }
}
