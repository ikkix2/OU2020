using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class StandalonePUNManager : MonoBehaviourPunCallbacks {
    // Photon AppID: d3920c52-a47c-4664-94d7-90991832488f
    // Unity上でのAppIDの設定方法はこちら https://qiita.com/UpAllNight/items/43e1b24301eb6029f18b

    private string roomName = ""; // 部屋名

    void Start () {
        if (PhotonNetwork.IsConnectedAndReady == false) {
            // サーバー未接続時の処理
            roomName = "room";
            PhotonNetwork.ConnectUsingSettings ();
        } else {
            Init ();
        }
    }

    // プレイヤーを配置する
    private void Init () {
        // 出現位置
        List<Vector3> list = new List<Vector3> ();
        list.Add(new Vector3 (-7, 3, -4));
        list.Add(new Vector3 (-8, 3, 0));
        list.Add(new Vector3 (-8, 3, 6));
        list.Add(new Vector3 (-1, 3, 7));
        list.Add(new Vector3 (-1, 3, -4));
        list.Add(new Vector3 (5, 3, 3));
        list.Add(new Vector3 (8, 3, 0));
        list.Add(new Vector3 (8, 3, -4));

        // 向き
        List<Quaternion> qs = new List<Quaternion>();
        qs.Add(Quaternion.Euler(0, 0, 0));
        qs.Add(Quaternion.Euler(0, 90, 0));
        qs.Add(Quaternion.Euler(0, 180, 0));
        qs.Add(Quaternion.Euler(0, 270, 0));


        // ランダムで出現する位置と向きを選ぶ
        int n1 = Random.Range (0, (list.Count - 1));
        int n2 = Random.Range (0, (qs.Count - 1));

        // 観戦者の場合は何もしない
        if (PhotonNetwork.LocalPlayer.NickName == "guest") {
            return;
        }

        GameObject player = PhotonNetwork.Instantiate ("Player", list[n1], qs[n2], 0);
        PlayerController2 playerController2 = player.GetComponent<PlayerController2> ();
        playerController2.ownerId = PhotonNetwork.LocalPlayer.NickName;
    }

    void OnGUI () {
        GUILayout.Label (PhotonNetwork.NetworkClientState.ToString ()); // ルームに入室しているかどうかを画面上に表示
    }

    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster () {
        if (PhotonNetwork.CurrentRoom == null) {
            PhotonNetwork.JoinOrCreateRoom (roomName, new RoomOptions (), TypedLobby.Default);
        }
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom () {
        if (PhotonNetwork.CurrentRoom != null) {
            Init ();
        }
    }
}
