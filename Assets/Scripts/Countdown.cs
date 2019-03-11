using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public static Countdown instance;
    float lastRefil;
    public float count;
    TextMeshProUGUI text;
    AudioSource audioSource;

    void Start()
    {
        instance = this;
        text = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
        Refill();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastRefil + count * Time.timeScale < Time.time && Recorder.instance.started)
        {
            count++;
            SetTimer();
        }
    }

    public void SetTimer()
    {
        if (Recorder.instance && !Recorder.instance.tpc.isDead)
        {
            audioSource.Play();
            text.text = Mathf.Round(10 - count).ToString();
            if (Mathf.Round(10 - count) <= 0)
            {
                Recorder.instance.GameOver();
            }
        }
    }

    public void Refill()
    {
        lastRefil = Time.time;
        count = 0;
        SetTimer();
    }
}
