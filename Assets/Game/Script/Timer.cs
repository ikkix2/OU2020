using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timeTexts;
    public Text countText;
    float totalTime = 180;
    int retime;
    float countdown = 4f;
    int count;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            count = (int)countdown;
            countText.text = count.ToString();
        }

        if (count == 0)
        {
            countText.text = "START";
        }

        if (countdown < 0)
        {
            countText.text = "";
            totalTime -= Time.deltaTime;
            retime = (int)totalTime;
            timeTexts.text = retime.ToString();
            if (retime == 0)
            {
                SceneManager.LoadScene("result");
            }
        }
    }
}
