using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool open = false;
    public Transform hinge;
    Collider collider;
    public MeshRenderer[] yellows;
    public Collider blockcollider;
    public Enemy[] enemies;
    public bool OG;
    public Material red;
    float lastSpawn;
    float spawnTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        if (!OG)
        {
            blockcollider.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (open && hinge.localEulerAngles.y < 90)
        {
            hinge.localEulerAngles += Vector3.up;
            if (hinge.localEulerAngles.y >= 90 && !OG)
            {
                foreach (MeshRenderer mr in yellows)
                {
                    mr.material = red;
                }
            }
        }
        if (Vector3.Distance(Recorder.instance.transform.position, transform.position) < 10
        && !OG
        && Recorder.instance.started)
        {
            open = true;
        }
        if (open && !OG)
        {
            SpawnEnemy();
        }
    }
    IEnumerator OGOpen()
    {
        Recorder.instance.startTime = Time.time;
        Recorder.instance.started = true;
        blockcollider.enabled = true;
        foreach (MeshRenderer mr in yellows)
        {
            mr.material = red;
        }
        yield return new WaitForSeconds(1);
        open = false;
        while (hinge.localEulerAngles.y > 0 && hinge.localEulerAngles.y < 180)
        {
            hinge.localEulerAngles -= Vector3.up * 5;
            yield return null;
        }
        collider.enabled = true;
        foreach (MeshRenderer mr in yellows)
        {
            mr.material = red;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Hit" && yellows[0].material != red)
        {
            collider.enabled = false;
            open = true;
        }
    }

    void SpawnEnemy()
    {
        if (spawnTime + lastSpawn < Time.time && Recorder.instance.started)
        {
            lastSpawn = Time.time;
            Enemy e = Instantiate(enemies[Random.Range(0, 3)], transform.position - transform.right * 0.5f - Vector3.up * 1.1f, Quaternion.identity);
        }
    }
}
