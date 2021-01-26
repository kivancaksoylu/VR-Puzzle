
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public GameObject player;
    float distance;
    bool hasInteracted = false;
    public float timeStart = 4; //Saniyeyi gösteren sayı

    public GameObject zemin;
    public Animator anim;
    public GameObject kapi;
    public GameObject canvas;
    public Text txt;
    public GameObject camRig;
    public GameObject trigger;

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        //Timer kodu
        if (distance <= radius)
        {
            timeStart -= Time.deltaTime;
        }
        else { 
            timeStart = 4;
        }

        //Interact kodu
        if (!hasInteracted && timeStart < 1 )
        {
            Interact();
            hasInteracted = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Interact() 
    {
        anim = kapi.GetComponent<Animator>();
        transform.GetComponent<Renderer>().material.color = Color.green;
        anim.Play("CubeDoorAnimation");
        zemin.GetComponent<MeshCollider>().enabled = true;
        canvas.transform.position = new Vector3(trigger.transform.position.x, trigger.transform.position.y, trigger.transform.position.z);
        canvas.transform.LookAt(camRig.transform);
        txt.fontSize = 20;
        txt.text = "Move Player Here \n To Finish Tutorial.";
    }
}
