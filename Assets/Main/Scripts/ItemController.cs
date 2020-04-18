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
/// </summary>

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]

public class ItemController : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////////////////
    // Field ////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////

    private PhotonView photonView;
    private MeshFilter meshFilter;

    [SerializeField] private Mesh cubeMesh;
    [SerializeField] private Mesh sphereMesh;
    [SerializeField] private Mesh capsuleMesh;
    [SerializeField] private Mesh cylinderMesh;


    /////////////////////////////////////////////////////////////////////////////////////
    // Start ////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        meshFilter = GetComponent<MeshFilter>();
    }


    /////////////////////////////////////////////////////////////////////////////////////
    // Update ///////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////

    // Update is called once per frame
    private void Update()
    {
        // オーナー以外は操作不可
        if (photonView.IsMine == false)
        {
            return;
        }

        // 上矢印キー
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(0, 1, 0);
        }

        // 下矢印キー
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0, -1, 0);
        }

        // 左矢印キー
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-1, 0, 0);
        }

        // 右矢印キー
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(1, 0, 0);
        }

        // Aキー
        if (Input.GetKeyDown(KeyCode.A))
        {
            //// キューブに変身するRPCを送信
            //photonView.RPC("RpcChangeMesh", RpcTarget.All, "Cube");
            transform.Translate(0, 0, 1);
        }

        // Sキー
        if (Input.GetKeyDown(KeyCode.S))
        {
            //// スフィアに変身するRPCを送信
            //photonView.RPC("RpcChangeMesh", RpcTarget.All, "Sphere");
            transform.Translate(0, 0, -1);
        }

        // Dキー
        if (Input.GetKeyDown(KeyCode.D))
        {
            // カプセルに変身するRPCを送信
            photonView.RPC("RpcChangeMesh", RpcTarget.All, "Capsule");
        }

        // Fキー
        if (Input.GetKeyDown(KeyCode.F))
        {
            // シリンダーに変身するRPCを送信
            photonView.RPC("RpcChangeMesh", RpcTarget.All, "Cylinder");
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////
    // PunRPCs //////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////

    // Meshを変更するRPCを受信
    [PunRPC]
    private void RpcChangeMesh(string type)
    {
        if (meshFilter == null)
        {
            Debug.Log("RpcChangeMesh Failed");
            return;
        }

        switch (type)
        {
            case "Cube":
                meshFilter.mesh = cubeMesh;
                break;
            case "Sphere":
                meshFilter.mesh = sphereMesh;
                break;
            case "Capsule":
                meshFilter.mesh = capsuleMesh;
                break;
            case "Cylinder":
                meshFilter.mesh = cylinderMesh;
                break;
        }
    }
}