using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerScript : MonoBehaviour
{
    public GameObject teleporter;
    public GameObject textCanvas;
    public GameObject playerCanvas;
    public GameObject cameraRig;
    public Text txt;
    private void OnTriggerEnter(Collider other)
    {
        teleporter.GetComponent<BoxCollider>().enabled = true;
        teleporter.GetComponent<MeshRenderer>().enabled = true;
        playerCanvas.GetComponent<Canvas>().enabled = false;
        txt.text = "To Teleport \n Look At Yellow Cubes";
        txt.fontSize = 25;
        textCanvas.transform.position = new Vector3(teleporter.transform.position.x, teleporter.transform.position.y-1, teleporter.transform.position.z);
        textCanvas.transform.LookAt(cameraRig.transform);

        gameObject.active = false;
    }
}
