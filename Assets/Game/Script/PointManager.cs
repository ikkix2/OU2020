using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    Text scoreText;
    GameObject oniTextSet;
    float getPoint = 0;
    int repoint;
    public int oniFlg;
    float countdown = 4f;


    // Start is called before the first frame update
    void Start()
    {
        // ScoreTextを取得
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        Debug.Log("スコアテキスト" + scoreText);

        // Canvasを取得
        GameObject canvas = GameObject.Find("Canvas");

        // OniTestSetを取得
        Transform oniTextSetTr = canvas.transform.Find("OniTextSet");
        oniTextSet = oniTextSetTr.gameObject;
        Debug.Log("オニテキストセット = " + oniTextSet);

    }

    // Update is called once per frame
    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            if (oniFlg == 0)
            {
                oniTextSet.SetActive(false);
                scoreText.text = "";
                getPoint += Time.deltaTime;
                repoint = (int)getPoint;
                scoreText.text = repoint.ToString() + "点";
            }

            if (oniFlg == 1)
            {
                oniTextSet.SetActive(true);
            }
        }

    }
}
