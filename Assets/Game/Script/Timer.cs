using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text timeTexts;
    public Text countText;
    float totalTime = 120;
    float countdown = 4f;
    int count;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (countdown > 0) {
            countdown -= Time.deltaTime;
            count = (int) countdown;
            countText.text = count.ToString ();
        }

        if (count == 0) {
            countText.text = "START";
        }

        if (countdown < 0) {
            totalTime -= Time.deltaTime;

            if (totalTime > 0f) {
                int retime = (int) totalTime;
                countText.text = "";
                timeTexts.text = "残り時間: " + retime.ToString() + "秒";
            } else {
                countText.text = "Finished!";
            }

            if (totalTime < -5f) {
                SceneManager.LoadScene ("Start");
            }
        }
    }
}
