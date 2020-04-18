using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniChange : MonoBehaviour
{
    int myOniFlg;
    int colOniFlg;

    // void start()
    // {
    //     myOniFlg = this.gameObject.GetComponent<PointManager>().oniFlg;
    //     Debug.Log(myOniFlg);
    // }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            myOniFlg = this.gameObject.GetComponent<PointManager>().oniFlg;
            Debug.Log(myOniFlg);
            colOniFlg = collision.gameObject.GetComponent<PointManager>().oniFlg;
            Debug.Log(colOniFlg);

            if (myOniFlg == 1)
            {
                if (colOniFlg == 0)
                {
                    myOniFlg = 0;
                    Debug.Log(myOniFlg);
                    colOniFlg = 0;
                    Debug.Log(colOniFlg);
                }
            }
        }
    }
}