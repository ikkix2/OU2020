using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;

public class getItem : MonoBehaviour
{
    public Material transparent_mtl;
    public Material normal_mtl;
    Renderer rend;
    GameObject refObj;

    void Start()
    {
        refObj = GameObject.Find("CreateItem");
    }

    public void OnTriggerEnter(UnityEngine.Collider other)
    {
        Debug.Log("OnTriggerEnter:::" + other.gameObject.name);
        if (other.gameObject.CompareTag("Item"))
        {
            Debug.Log("OnTriggerEnter:::Item" + other.gameObject.name);
            rend = gameObject.GetComponent<Renderer>();
            rend.material = transparent_mtl;
            StartCoroutine(DelayMethod(20, () =>
            {
                //20秒後に元に戻す
                Debug.Log("Delay call material");
                CreateItem create_item = refObj.GetComponent<CreateItem>();
                rend.material = normal_mtl;
            }));
            Destroy(other.gameObject);
        }

        //15秒後に実行する
        Debug.Log("StartCoroutine:::Before");
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            StartCoroutine(DelayMethod(15, () =>
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
    private IEnumerator DelayMethod(int waitTime, Action action)
    {
        Debug.Log("DelayMethod");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("DelayMethod::GO::waitTime" + waitTime);
        action();
        Debug.Log("DelayMethod::After::waitTime" + waitTime);
    }
}
