using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    static Score singleton;
    TMP_Text text;
    int score;
    void Awake()
    {
        text = GetComponent<TMP_Text>();
        if (singleton == null)
        {
            singleton = this;
        }
    }

    private void SelfIncrement()
    {
        score++;
        text.text = "" + score;
    }

    static public void Increment()
    {
        singleton.SelfIncrement();
    }
}
