using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menumanager : MonoBehaviour {

    public GameObject[] guns;
    public GameObject[] enviromentparents;

    [SerializeField] int envivalue;

    void Start()
    {
        if (!PlayerPrefs.HasKey(enviromentparents[0].GetComponent<enviromentcountainer>().id))
        {
            PlayerPrefs.SetFloat(enviromentparents[0].GetComponent<enviromentcountainer>().id, 1f);
        }

        if (!PlayerPrefs.HasKey("enviroment"))
        {
            PlayerPrefs.SetFloat("enviroment", 0f);
        }

        if (!PlayerPrefs.HasKey("selectknife"))
        {
            PlayerPrefs.SetString("selectknife", "gun0");
        }

        if (!PlayerPrefs.HasKey(guns[0].GetComponent<guncountainer>().id))
        {
            PlayerPrefs.SetFloat(guns[0].GetComponent<guncountainer>().id, 1f);
        }

        gunbuttonayarla();
        envivalue = (int)PlayerPrefs.GetFloat("enviroment");
        enviromentaktifet();
    }

    public void enviromentsagok()
    {
        if(envivalue < enviromentparents.Length - 1)
        {
            envivalue++;
            enviromentaktifet();
            enviromentbuttonayarla(enviromentparents[envivalue].GetComponent<enviromentcountainer>());
        }
    }

    public void enviromentsolok()
    {
        if (envivalue > 0) 
        {
            envivalue--;
            enviromentaktifet();
            enviromentbuttonayarla(enviromentparents[envivalue].GetComponent<enviromentcountainer>());
        }
    }

    public void enviromentaktifet()
    {
        foreach (var item in enviromentparents)
        {
            item.gameObject.SetActive(false);
        }

        enviromentparents[envivalue].gameObject.SetActive(true);
        enviromentbuttonayarla(enviromentparents[envivalue].GetComponent<enviromentcountainer>());
    }

    public void enviromentbuttonayarla(enviromentcountainer envisc)
    {
        if (PlayerPrefs.GetFloat(envisc.id) == 0f)
        {
            envisc.butt.text = envisc.buyvalue.ToString();
            envisc.but.onClick.AddListener(() => enviromnetsatinal(envisc));

        }
        else if (PlayerPrefs.GetFloat("enviroment") == envisc.idvalue)
        {
            envisc.butt.text = "Selected";
            envisc.but.onClick.RemoveAllListeners();
        }
        else
        {
            envisc.butt.text = "Select";
            envisc.but.onClick.AddListener(() => enviromnetselect(envisc));
        }
    }

    public void enviromnetselect(enviromentcountainer envisc)
    {
        PlayerPrefs.SetFloat("enviroment", envisc.idvalue);
        enviromentbuttonayarla(envisc);
    }

    public void enviromnetsatinal(enviromentcountainer envisc)
    {
        if(PlayerPrefs.GetFloat("money") >= envisc.buyvalue)
        {
            PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") - envisc.buyvalue);
            PlayerPrefs.SetFloat(envisc.id, 1f);
            PlayerPrefs.SetFloat("enviroment", envisc.idvalue);
            enviromentbuttonayarla(envisc);
        }
    }

    public void enviromentsifirla()
    {
        envivalue = (int)PlayerPrefs.GetFloat("enviroment");
        enviromentaktifet();
    }

    public void gunbuttonayarla()
    {
        foreach (var item in guns)
        {
            guncountainer itemcountainer = item.GetComponent<guncountainer>();

            if(PlayerPrefs.GetFloat(itemcountainer.id) == 0f)
            {
                itemcountainer.buttext.text = itemcountainer.buyvalue + "";
                itemcountainer.but.onClick.RemoveAllListeners();
                itemcountainer.but.onClick.AddListener(() => buy(itemcountainer));
            }
            else if(PlayerPrefs.GetString("selectknife") == itemcountainer.id)
            {
                itemcountainer.buttext.text = "Selected";
                PlayerPrefs.SetFloat("knife", itemcountainer.arrayvalue);
                itemcountainer.but.onClick.RemoveAllListeners();
            }
            else
            {
                itemcountainer.buttext.text = "Select";
                itemcountainer.but.onClick.RemoveAllListeners();
                itemcountainer.but.onClick.AddListener(() => selectgun(itemcountainer));
            }
        }
    }

    public void selectgun(guncountainer item)
    {
        PlayerPrefs.SetString("selectknife", item.id);
        print(PlayerPrefs.GetString("selectknife"));
        PlayerPrefs.SetFloat("knife", item.arrayvalue);
        gunbuttonayarla();
    }

    public void buy(guncountainer item)
    {
        if(PlayerPrefs.GetFloat("money") >= item.buyvalue)
        {
            PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") - item.buyvalue);
            PlayerPrefs.SetFloat(item.id, 1);
            PlayerPrefs.SetString("selectknife", item.id);
            gunbuttonayarla();
        }
    }

    public void hile()
    {
        PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") + 1000f);
    }
}
