using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifethrower : MonoBehaviour {

    [SerializeField] GameObject[] knifes;
    [SerializeField] Transform knifepos, knifeparent;
    [SerializeField] gamemanager managersc;
    [SerializeField] float zmn, cooldown;

    GameObject knife;


    void Start()
    {

    }

    public void bicakspawnla()
    {
        if (managersc.bicakatabilir && zmn > cooldown)
        {
            knife = Instantiate(knifes[(int)PlayerPrefs.GetFloat("knife")], knifepos.transform.position + new Vector3(0f, 0f, -4f), Quaternion.Euler(0f, 0f, 0f));
            knife.transform.SetParent(knifeparent);
            knife.GetComponent<knifesc>().knifespawnersc = this;
            knife.GetComponent<knifesc>().postr = knifepos;
            zmn = 0f;
        }
    }

    void Update()
    {
        zmn += Time.deltaTime;
    }

    public void bicakat()
    {
        if (knife != null)
            knife.GetComponent<knifesc>().at();
        else
            bicakspawnla();
    }
}
