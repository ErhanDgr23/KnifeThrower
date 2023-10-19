using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plaprefgroup : MonoBehaviour {

    void Start()
    {
        PlayerPrefs.GetFloat("knife");
        PlayerPrefs.GetFloat("skor");
        PlayerPrefs.GetFloat("para");
    }
}
