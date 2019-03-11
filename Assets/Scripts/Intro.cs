using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Intro : MonoBehaviour
{
    public TextMeshProUGUI text, bg;
    bool typing = false;
    int count;
    string[] intros = new string[]{
        "You have been poinsed.  Your heart will stop if you don't stay excited.",
        "Through the door is an endless amount of excitement.",
        "Use WASD to move.  Press spacebar to punch.",
        "Once you go through the door there is no going back.",
        "Last as long as you can.",
        "You'll get a refill on your 10 seconds by killing someone."
    };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TypePhrase");
    }

    IEnumerator TypePhrase()
    {
        if (intros.Length > count)
        {
            typing = true;
            int pos = 0;
            text.text = "";
            bg.text = "";
            while (typing)
            {
                text.text += intros[count].ToCharArray()[pos];
                bg.text += intros[count].ToCharArray()[pos];
                pos++;
                if (pos >= intros[count].Length)
                {
                    text.text += "\n\nany key to continue";
                    bg.text += "\n\nany key to continue";
                    typing = false;
                    count++;
                }
                yield return null;
                yield return null;
            }
            yield return null;
        }
        else
        {
            text.gameObject.SetActive(false);
            bg.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (typing)
            {
                typing = false;
                text.text = intros[count];
                bg.text = intros[count];
                count++;
            }
            else
            {
                StartCoroutine("TypePhrase");
            }
        }
        if (Recorder.instance.started)
        {
            text.gameObject.SetActive(false);
        }
    }
}
