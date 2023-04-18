using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static int progression1 = 0;
    public static int progression2 = 0;

    public GameObject level2;
    public GameObject level3;

    void Update()
    {
        if(progression1 > 0) {
            level2.SetActive(true);
        }
        if(progression2 > 0) {
            level3.SetActive(true);
        }
    }
}
