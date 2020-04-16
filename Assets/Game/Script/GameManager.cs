using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] character;
    int selectedOniFlg;
    int number;

    void Start()
    {
        number = Random.Range(0, character.Length);
        character[number].GetComponent<PointManager>().oniFlg = 1;

    }
}
