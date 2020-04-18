using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject[] player;
    int selectedOniFlg;
    int number;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(player.Length);
        number = Random.Range(0, player.Length);
        player[number].GetComponent<PointManager>().oniFlg = 1;

    }
}
