using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifesc : MonoBehaviour {

    [SerializeField] GameObject particeleffect;
    [SerializeField] Vector3 rot;
    [SerializeField] float rotspeed;
    [SerializeField] float speed;

    [HideInInspector] public knifethrower knifespawnersc;
    [HideInInspector] public Transform postr;

    Rigidbody rb;
    bool posesitle, temas, atildi, ontime;
    Vector3 pos;
    float zmn, destroycooldown = 5f;
    gamemanager managersc;

    void Start()
    {
        managersc = gamemanager.managersc;
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
        possifirla();
        managersc.bicak = this;
        posesitle = true;
    }

    public void possifirla()
    {
        if(managersc.loglist.Count > 1)
            pos = new Vector3(postr.position.x, managersc.loglist[managersc.loglist.Count - 1].transform.position.y, postr.position.z);
        else
        {
            pos = new Vector3(postr.position.x, postr.transform.position.y, postr.position.z);
            managersc.bicakatabilir = false;
        }
    }

    public void at()
    {
        if (managersc.bicakatabilir && posesitle == false)
        {
            atildi = true;
            rb.velocity = new Vector3(0f, 0f, speed);
            Invoke("knifespawnet", 0.2f);
        }
    }

    public void knifespawnet()
    {
        knifespawnersc.bicakspawnla();
    }

    void Update()
    {
        if (atildi)
        {
            if (!temas)
                zmn += Time.deltaTime;

            if (zmn > destroycooldown && !temas)
            {
                Destroy(this.gameObject);
                zmn = 0f;
            }
        }

        if (transform.position != pos && posesitle)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 10f * Time.deltaTime);
        }
        else
        {
            posesitle = false;
            managersc.bicakatabilir = true;
        }
    }

    public void particlekapa()
    {
        particeleffect.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "branch")
        {
            if (other.transform.parent.parent.GetComponent<treelayersc>() != null)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                transform.SetParent(other.transform);
                other.transform.parent.parent.GetComponent<treelayersc>().speedmain = 0f;
                other.transform.parent.parent.GetComponent<treelayersc>().analayer = false;
                managersc.gamefailed();
            }
        }
        else if (other.gameObject.tag == "knife")
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            transform.SetParent(other.transform);
            managersc.loglist[managersc.loglist.Count - 1].GetComponent<treelayersc>().analayer = false;
            managersc.loglist[managersc.loglist.Count - 1].GetComponent<treelayersc>().speedmain = 0f;
            managersc.gamefailed();
        }
        else if (other.gameObject.tag == "tree" && !ontime)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            transform.SetParent(other.transform);
            Invoke("particlekapa", 0.2f);
            PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") + 20f);
            managersc.skor += 50f;
            managersc.money += 20f;
            temas = true;
            ontime = true;

            if (other != null)
                other.transform.parent.GetComponent<treelayersc>().damageal(20);
        }
    }
}
