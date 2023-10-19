using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treelayersc : MonoBehaviour {

    public bool analayer, hardmode;
    public float speedmain;

    [SerializeField] GameObject canobj, parcalarparent;
    [SerializeField] GameObject[] branches;
    [SerializeField] float[] speeds, cooldowns;

    bool onetime;
    float rdm, rdmtimer, canvalue = 100f;
    float timer;
    gamemanager managersc;

    private void Start()
    {
        managersc = gamemanager.managersc;
        rdm = Random.Range(0f, speeds.Length);
        rdmtimer = Random.Range(0f, cooldowns.Length);
        branchrandom();
    }

    public void branchrandom()
    {
        for (int i = 0; i < branches.Length; i++)
        {
            int randombranch = Random.Range(0, 11);

            if (randombranch < 4)
                branches[i].gameObject.SetActive(true);
            else
                branches[i].gameObject.SetActive(false);
        }
    }

    public void oldu()
    {
        if(parcalarparent != null)
        {
            parcalarparent.gameObject.SetActive(true);
            parcalarparent.transform.SetParent(null);
            Destroy(parcalarparent, 2f);
            managersc.loglayerdecerease(this.gameObject);
        }
    }

    void Update()
    {
        if (canvalue <= 0f)
        {
            managersc.bicakatabilir = false;

            if (!onetime)
            {
                oldu();
                onetime = true;
            }
        }

        if (analayer)
        {
            if (speedmain != speeds[(int)rdm])
            {
                speedmain = Mathf.MoveTowards(speedmain, speeds[(int)rdm], 0.2f);
            }

            if (hardmode)
            {
                timer += Time.deltaTime;

                if (timer > cooldowns[(int)rdmtimer])
                {
                    rdm = Random.Range(0f, speeds.Length);
                    timer = 0f;
                }

                if (speedmain != speeds[(int)rdm])
                {
                    speedmain = Mathf.MoveTowards(speedmain, speeds[(int)rdm], 0.2f);
                }
            }


            transform.Rotate(Vector3.up * speedmain * Time.deltaTime);
        }
    }

    public void damageal(float value)
    {
        canvalue -= value;
        canobj.transform.localScale -= new Vector3(0.36f, 0.36f, 0f);
    }
}
