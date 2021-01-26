using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerScript2 : MonoBehaviour
{
    public GameObject zemin;
    public GameObject interactable;
    public GameObject canvas;
    public GameObject camRig;
    public Text txt;

    private void OnTriggerEnter(Collider other)
    {
        zemin.GetComponent<BoxCollider>().enabled = true;
        canvas.transform.position = new Vector3(interactable.transform.position.x, interactable.transform.position.y+4, interactable.transform.position.z);
        canvas.transform.LookAt(camRig.transform);
        txt.fontSize = 15;
        txt.text = "This is an interactable object \n get player close to it and wait for few seconds. \n Interactables will be in different shape or forms \n but to logic is same for all of them.";
    }
}
