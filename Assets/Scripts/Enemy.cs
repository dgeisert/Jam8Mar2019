using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    Transform target;
    ThirdPersonCharacter tpc;
    public bool ranged;
    public float attackDistance = 1f;
    public float attackCooldown = 1f;
    float lastAttack;
    bool attacking = false;
    float DeathTime = -1;
    public GameObject projectile;
    public GameObject blood;
    Rigidbody rb;
    Collider collider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        tpc = GetComponent<ThirdPersonCharacter>();
    }

    void FixedUpdate()
    {
        if (!Recorder.instance.started)
        {
            return;
        }
        if (target == null)
        {
            target = Recorder.instance.transform;
        }
        if (!tpc.isDead && Vector3.Distance(transform.position, target.position) < attackDistance && !attacking)
        {
            attacking = true;
            lastAttack = Time.time;
            if (!ranged)
            {
                tpc.Attack();
            }
            else
            {
                Shoot();
            }
        }
        else if (!attacking && !tpc.isDead && Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            tpc.Move(target.position - transform.position, false, false);
        }
        else
        {
            tpc.Move(Vector3.zero, false, false);
        }
        if (attackCooldown + lastAttack < Time.time)
        {
            attacking = false;
        }
        if (false && DeathTime + 2 < Time.time && tpc.isDead)
        {
            collider.enabled = true;
            rb.useGravity = true;
            rb.isKinematic = false;
            tpc.GetUp();
        }
        else if (DeathTime + 2 < Time.time && tpc.isDead)
        {
            Destroy(gameObject);
        }
        if (transform.position.z - Recorder.instance.transform.position.z < -20)
        {
            transform.position += Vector3.forward * 40;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Hit")
        {
            Invoke("ActivateBlood", 0.5f);
            collider.enabled = false;
            rb.useGravity = false;
            rb.isKinematic = true;
            Recorder.instance.CameraShake();
            DeathTime = Time.time;
            Countdown.instance.Refill();
            tpc.HitByEnemy();
        }
    }
    void ActivateBlood()
    {
        blood.SetActive(false);
        blood.SetActive(true);
    }

    void Shoot()
    {
        GameObject go = Instantiate(projectile, transform.position + transform.forward * 0.5f, transform.rotation);
        go.transform.RotateAround(transform.position, Vector3.up, (Random.value - 0.5f) * 10);
    }
}
