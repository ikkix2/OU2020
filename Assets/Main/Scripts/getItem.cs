using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;

public class getItem : MonoBehaviour
{
    public Material transparent_mtl;
    Renderer rend;
    GameObject refObj;

    void Start()
    {
        refObj = GameObject.Find("CreateItem");
    }

    public void OnTriggerEnter(UnityEngine.Collider other)
    {
        Debug.Log("OnTriggerEnter:::" + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnTriggerEnter:::Player" + other.gameObject.name);
            rend = other.gameObject.GetComponent<Renderer>();
            rend.material = transparent_mtl;
            Destroy(gameObject);
        }

        //10秒後に実行する
        Debug.Log("StartCoroutine:::Before");
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            Debug.Log("Not Delay call");
            CreateItem create_item2 = refObj.GetComponent<CreateItem>();
            create_item2.RandomCreateItem();

            StartCoroutine(DelayMethod(10.0f, () =>
            {
                Debug.Log("Delay call");
                CreateItem create_item = refObj.GetComponent<CreateItem>();
                create_item.RandomCreateItem();
            }));
        }
    }



    /// <summary>
    /// 渡された処理を指定時間後に実行する
    /// </summary>
    /// <param name="waitTime">遅延時間[ミリ秒]</param>
    /// <param name="action">実行したい処理</param>
    /// <returns></returns>
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        Debug.Log("DelayMethod");
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
