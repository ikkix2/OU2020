using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;


public class getGameData : MonoBehaviour
{
    //キャラクターが増えたら、この項目も増やす（予めGameObjectを登録しておいて、Databaseに合わせて変更できるようにする必要がある）
    public GameObject chara_1;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        StartCoroutine(GetPlayerDat("https://forusagi.firebaseio.com/RoomNo/R1/PlayerNo.json"));
    }

    // Update is called once per frame
    [Obsolete]
    void Update()
    {
        StartCoroutine(UpdatePlayerDat("https://forusagi.firebaseio.com/RoomNo/R1/PlayerNo.json"));
    }

    [Obsolete]
    private IEnumerator GetPlayerDat(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if(request.isHttpError || request.isNetworkError)
        {
            //output error log
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            var jsonData = MiniJSON.Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
            for (int i = 1; i <= jsonData.Count; i++)
            {
                var results = jsonData["P"+i] as Dictionary<string, object>;

                int direction = Convert.ToInt32(results["Direction"]);
                int oni_flg = Convert.ToInt32(results["OniFlg"]);
                int point = Convert.ToInt32(results["Point"]);
                int speed = Convert.ToInt32(results["Speed"]);

                var position_dat = results["Transform"] as string;

                string[] position_tmp = position_dat.Split(',');
                float[] position = Array.ConvertAll<string, float>(position_tmp, Single.Parse);

                GameObject player = Instantiate(chara_1) as GameObject;
                player.transform.position = new Vector3(position[0], position[1], position[2]);
                string v = "P" + i;
                player.name = v;

                switch (i)
                {
                    case 1:
                        player.GetComponent<Renderer>().material.color = Color.red;
                        break;
                    case 2:
                        player.GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    case 3:
                        player.GetComponent<Renderer>().material.color = Color.green;
                        break;
                    case 4:
                        player.GetComponent<Renderer>().material.color = Color.yellow;
                        break;
                    default:
                        break;
                }
            }

        }
    }

    [Obsolete]
    private IEnumerator UpdatePlayerDat(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            //output error log
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            var jsonData = MiniJSON.Json.Deserialize(request.downloadHandler.text) as Dictionary<string, object>;
            for (int i = 1; i <= jsonData.Count; i++)
            {
                var results = jsonData["P" + i] as Dictionary<string, object>;

                int direction = Convert.ToInt32(results["Direction"]);
                int oni_flg = Convert.ToInt32(results["OniFlg"]);
                int point = Convert.ToInt32(results["Point"]);
                int speed = Convert.ToInt32(results["Speed"]);

                var position_dat = results["Transform"] as string;

                string[] position_tmp = position_dat.Split(',');
                float[] position = Array.ConvertAll<string, float>(position_tmp, Single.Parse);

                string v = "P" + i;
                GameObject player = GameObject.Find(v);
                player.transform.position = new Vector3(position[0], position[1], position[2]);

            }

        }
    }
}
