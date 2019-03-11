using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using TMPro;
class Recording
{
    float time;
    Vector3 pos;
}

public class Recorder : MonoBehaviour
{
    public static Recorder instance;
    public ThirdPersonCharacter tpc;
    public List<Vector3> moves = new List<Vector3>();
    public List<Vector3> pos = new List<Vector3>();
    public Follow camera;
    bool attacking = false;
    float lastShake = -10;
    public GameObject gameOver, blood;
    public TextMeshProUGUI score, scoreBG;
    public float startTime;
    public bool started = false;
    int attack = -1;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Time.timeScale = 0.25f;
        instance = this;
        tpc = GetComponent<ThirdPersonCharacter>();
        //tpc.onMove = AddMove;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //pos.Add(transform.position + new Vector3(0, attack, 0));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            attack = tpc.Attack();
            attacking = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            attack = -1;
            attacking = false;
        }
        if (lastShake + 0.1f > Time.time)
        {
            camera.shake = lastShake - Time.time + 0.1f;
        }
        if (transform.position.y > 1)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void AddMove(Vector3 move)
    {
        moves.Add(move);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Hit")
        {
            CameraShake();
            Countdown.instance.count++;
            Countdown.instance.SetTimer();
        }
    }

    public void CameraShake()
    {
        lastShake = Time.time;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        tpc.HitByEnemy();
        StartCoroutine("RestartLevel");
        blood.SetActive(true);
    }
    IEnumerator RestartLevel()
    {
        scoreBG.gameObject.SetActive(true);
        score.text = "Score: " + Mathf.Round((Time.time - startTime) / Time.timeScale).ToString() + "s";
        scoreBG.text = "Score: " + Mathf.Round((Time.time - startTime) / Time.timeScale).ToString() + "s";
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
