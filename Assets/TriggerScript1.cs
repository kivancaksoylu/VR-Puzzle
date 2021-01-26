using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerScript1 : MonoBehaviour
{
    public GameObject aktiflestir1;
    public GameObject aktiflestir2;
    public GameObject[] teleporters;
    public GameObject canvas;
    public Text txt;
    public GameObject camRig;

    private void OnTriggerEnter(Collider other)
    {
        aktiflestir1.transform.GetComponent<MeshRenderer>().enabled = true;
        aktiflestir1.transform.GetComponent<MeshCollider>().enabled = true;
        aktiflestir2.transform.GetComponent<MeshRenderer>().enabled = true;

        foreach(GameObject g in teleporters)
        {
            g.GetComponent<MeshRenderer>().enabled = true;
            g.GetComponent<BoxCollider>().enabled = true;
        }

        canvas.transform.position = new Vector3(teleporters[5].transform.position.x, teleporters[5].transform.position.y-1, teleporters[5].transform.position.z);
        canvas.transform.LookAt(camRig.transform);
        txt.text = "Teleport Here";
        txt.fontSize = 30;
    }

}
