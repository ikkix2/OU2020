using Photon.Pun;
using UnityEngine;

/// <summary>
/// 
/// Unity 2019.1.11f1
/// 
/// Pun: 2.4
/// 
/// Photon lib: 4.1.2.4
/// 
/// Prefab: Assets/Resources/Prefabs/Item.prefab
/// 
/// </summary>

public class SpawnItem : MonoBehaviourPunCallbacks
{
    // 部屋に入室した時
    public override void OnJoinedRoom()
    {
        // ランダムな位置にアイテムを生成
        int x = Random.Range(-5, 6);
        int y = Random.Range(-4, 3);

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate("Prefabs/Item", new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}