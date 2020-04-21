using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class StandalonePUNManager : MonoBehaviourPunCallbacks {
    // Photon AppID: d3920c52-a47c-4664-94d7-90991832488f
    // Unity上でのAppIDの設定方法はこちら https://qiita.com/UpAllNight/items/43e1b24301eb6029f18b

    private string roomName = ""; // 部屋名

    void Start () {
        roomName = "room";
        PhotonNetwork.ConnectUsingSettings ();
    }

    void OnGUI () {
        GUILayout.Label (PhotonNetwork.NetworkClientState.ToString ()); // ルームに入室しているかどうかを画面上に表示
    }

    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster () {
        // "room"という名前のルームに参加
        PhotonNetwork.JoinOrCreateRoom (roomName, new RoomOptions (), TypedLobby.Default);
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom () {
        int x = Random.Range (-10, 10);
        int z = Random.Range (-10, 10);

        GameObject player = PhotonNetwork.Instantiate ("Player", new Vector3(x, 0, z), Quaternion.identity, 0);
        PlayerController2 playerController2 = player.GetComponent<PlayerController2> ();
    }
}
