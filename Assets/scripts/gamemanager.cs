using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gamemanager : MonoBehaviour {

    public static gamemanager managersc;

    public List<GameObject> loglist = new List<GameObject>();
    public bool bicakatabilir, oyunbasladi;
    public float skor, money;

    [SerializeField] GameObject failedpanel, succespanel;
    [SerializeField] TextMeshProUGUI stage1, stage2, moneymenu, moneyt, moneyenvi, skort;

    [HideInInspector] public knifesc bicak;

    GameObject objknife;
    public bool failedb, onetime;

    private void Awake()
    {
        managersc = this;
    }

    private void Start()
    {
        //Application.targetFrameRate = 20;
    }

    public void stageguncelle(float value)
    {
        PlayerPrefs.SetFloat("stage", PlayerPrefs.GetFloat("stage") + value);
    }

    public void gamefailed()
    {
        failedb = true;
        failedpanel.gameObject.SetActive(true);
        winfailpanelcounteiner sc = failedpanel.GetComponent<winfailpanelcounteiner>();
        sc.skor.text = skor.ToString();
        sc.money.text = money.ToString();
        bicakatabilir = false;
    }

    public void gamesuccess()
    {
        stageguncelle(1);
        failedb = true;
        succespanel.gameObject.SetActive(true);
        winfailpanelcounteiner sc = succespanel.GetComponent<winfailpanelcounteiner>();
        sc.skor.text = skor.ToString();
        sc.money.text = money.ToString();
        bicakatabilir = false;
    }

    public void yenidenbasla()
    {
        Application.LoadLevel(0);
    }

    void Update()
    {
        stage1.text = PlayerPrefs.GetFloat("stage").ToString();
        stage2.text = PlayerPrefs.GetFloat("stage").ToString();
        moneymenu.text = PlayerPrefs.GetFloat("money").ToString();
        moneyenvi.text = PlayerPrefs.GetFloat("money").ToString();
        moneyt.text = PlayerPrefs.GetFloat("money").ToString();
        skort.text = skor.ToString();

        if (!oyunbasladi)
            return;

        if (loglist.Count <= 1f && !onetime)
        {
            gamesuccess();
            onetime = true;
        }
    }

    public void loglayerdecerease(GameObject obj)
    {
        if(!failedb)
        {
            bicakatabilir = false;

            if (loglist.Count > 1)
            {
                if (loglist.Count > 2)
                    bicak.possifirla();

                objknife = obj;
                Invoke("objyeyoket", 0.1f);
                loglist.Remove(obj);
                logdondur();
            }
            else
                bicakatabilir = false;
        }
    }

    public void objyeyoket()
    {
        bicakatabilir = true;
        Destroy(objknife);
    }

    public void logdondur()
    {
        if (loglist.Count > 1)
        {
            int hardmoderdm = Random.Range(0, 11);
            loglist[loglist.Count - 1].GetComponent<treelayersc>().analayer = true;

            if (hardmoderdm > 6)
            {
                loglist[loglist.Count - 1].GetComponent<treelayersc>().hardmode = true;
            }
        }
        else
            bicakatabilir = false;
    }
}
