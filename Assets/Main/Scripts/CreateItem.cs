using UnityEngine;
using Photon.Pun;

public class CreateItem : MonoBehaviour
{
    public GameObject randomItem_1;
    public GameObject randomItem_2;
    public GameObject randomItem_3;

    public Vector3[] randItemP1;
    public Vector3[] randItemP2;
    public Vector3[] randItemP3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CreateItemStart:::PhotonNetwork.LocalPlayer.ActorNumber:::" + PhotonNetwork.LocalPlayer.ActorNumber);

        // Itemは誰かが作ってくれればいいはずなので、決め打ちで最初にアクセスした人に作ってもらってしまう
        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            RandomCreateItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomCreateItem()
    {
        Debug.Log("CreateItemStart:::RandomCreateItem");
        int i_number = Random.Range(1, 4);

        switch (i_number)
        {
            case 1:
                int i1p_number = Random.Range(0, randItemP1.Length);
                Debug.Log("Prefabs/" + randomItem_1.name);
                PhotonNetwork.Instantiate("Prefabs/" + randomItem_1.name, randItemP1[i1p_number], Quaternion.identity);
                break;
            case 2:
                int i2p_number = Random.Range(0, randItemP2.Length);
                Debug.Log("Prefabs/" + randomItem_2.name);
                PhotonNetwork.Instantiate("Prefabs/" + randomItem_2.name, randItemP1[i2p_number], Quaternion.identity);
                break;
            case 3:
                int i3p_number = Random.Range(0, randItemP3.Length);
                Debug.Log("Prefabs/" + randomItem_3.name);
                PhotonNetwork.Instantiate("Prefabs/" + randomItem_3.name, randItemP1[i3p_number], Quaternion.identity);
                break;
        }
    }
}
