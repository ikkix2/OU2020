using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    public Text scoreText;
    // [SerializeField] public GameObject oniTextSet;
    float getPoint = 0;
    int repoint;
    public int oniFlg = 0;
    float countdown = 4f;


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
        }
        else
        {
            if (oniFlg == 0)
            {
                // oniTextSet.SetActive(false);
                scoreText.text = "";
                getPoint += Time.deltaTime;
                repoint = (int)getPoint;
                scoreText.text = repoint.ToString() + "点";
            }

            if (oniFlg == 1)
            {
                // oniTextSet.SetActive(true);
            }
        }

    }
}
