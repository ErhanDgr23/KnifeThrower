using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treespawner : MonoBehaviour {

    [SerializeField] GameObject rootobj, layerobj;
    [SerializeField] Transform treeparent;

    [Range(4, 8)]
    [SerializeField] int treevalue;

    List<GameObject> woodlist = new List<GameObject>();

    private void Start()
    {
        treevalue = Random.Range(4, 9);
    }

    public void spawnet()
    {
        GameObject root = Instantiate(rootobj, treeparent.position, Quaternion.identity);
        root.transform.SetParent(treeparent);
        woodlist.Add(root);

        for (int i = 0; i < treevalue; i++)
        {
            GameObject layerwood = Instantiate(layerobj, woodlist[woodlist.Count - 1].transform.GetChild(0).transform.position, Quaternion.identity);
            layerwood.transform.SetParent(treeparent);
            woodlist.Add(layerwood);
        }

        gamemanager.managersc.loglist = woodlist;
        gamemanager.managersc.oyunbasladi = true;
    }
}
