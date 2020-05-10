using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PUNManager : MonoBehaviourPunCallbacks {
    // Photon AppID: d3920c52-a47c-4664-94d7-90991832488f
    // Unity上でのAppIDの設定方法はこちら https://qiita.com/UpAllNight/items/43e1b24301eb6029f18b
    private int testFlg = 0;
    private string roomName = "room";

    void Start () {
        // PhotonNetwork.ConnectUsingSettings ();

        int x = Random.Range (-5, 2);
        int z = Random.Range (-5, 2);

        GameObject player = PhotonNetwork.Instantiate ("Player", new Vector3(x, 3, z), Quaternion.identity, 0);
        PlayerController2 playerController2 = player.GetComponent<PlayerController2> ();
        playerController2.ownerId = PhotonNetwork.LocalPlayer.NickName;
    }

    // void OnGUI () {
    //     GUILayout.Label (PhotonNetwork.NetworkClientState.ToString ()); // ルームに入室しているかどうかを画面上に表示
    // }

    // void PUNInit () {
    //     PhotonNetwork.ConnectUsingSettings ();
    // }

    // // ルームに入室前に呼び出される
    // public override void OnConnectedToMaster () {
    //     PhotonNetwork.JoinOrCreateRoom (roomName, new RoomOptions (), TypedLobby.Default);
    // }

    // // ルームに入室後に呼び出される
    // public override void OnJoinedRoom () {
    //     int x = Random.Range (-2, 2);
    //     int z = Random.Range (-2, 2);

    //     GameObject player = PhotonNetwork.Instantiate ("Player", new Vector3(x, 3, z), Quaternion.identity, 0);
    //     PlayerController2 playerController2 = player.GetComponent<PlayerController2> ();
    // }
}
