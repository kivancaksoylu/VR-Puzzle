using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMotor))]
public class VRGaze : MonoBehaviour
{
    public GameObject oyuncu; //Önemli: oyuncu objesindeki PlayerMotor scriptine ulaşmak için objeyi bu şekilde declare etmelisin.

    PlayerMotor motor;

    public Image imgGaze;
    public Image imgGaze2;

    private GameObject[] all_Objs;
    private GameObject canvas;
    private GameObject canvasForTexts;
    int currentSceneId;


    public float totalTime = 2;
    bool gvrStatus,gvrStatus2;
    float gvrTimer;

    public int distanceOfRay = 50;
    private RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        imgGaze.fillAmount = 0;
        imgGaze2.fillAmount = 0;
        gvrStatus = false;
        gvrStatus2 = false;
        all_Objs = GameObject.FindGameObjectsWithTag("Teleport");
        canvas = GameObject.Find("Canvas");
        canvasForTexts = GameObject.Find("CanvasForTexts");
        motor = oyuncu.GetComponent<PlayerMotor>();  //Oyuncu objesindeki PlayerMotor Scriptini al

        currentSceneId = SceneManager.GetActiveScene().buildIndex;  //Şuanki sahnenin build index numarasını kontrol et
        Debug.Log("This is current scene id = " + currentSceneId);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);

        switch (currentSceneId) 
        {
            case 1:
                break;
            case 2:
                oyuncu.transform.localScale = new Vector3(3, 3, 3);
                break;
            case 3:
                oyuncu.transform.localScale = new Vector3(3, 3, 3);
                break;
        }

    }
  
    // Update is called once per frame
    void Update()
    {
        switch (currentSceneId) //Sahneye özel kodların geleceği yer
        {
            case 1:
                canvasForTexts.transform.position = new Vector3(oyuncu.transform.position.x, oyuncu.transform.position.y + 4, oyuncu.transform.position.z);
                canvasForTexts.transform.LookAt(gameObject.transform); 
                break;
            case 2:
                canvasForTexts.transform.LookAt(gameObject.transform);
                break;
            case 3:
                canvasForTexts.transform.LookAt(gameObject.transform);
                break;
        }

        if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
        }
        else if (gvrStatus2) {
            gvrTimer += Time.deltaTime;
            imgGaze2.fillAmount = gvrTimer / totalTime;
        }
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out _hit, distanceOfRay)) 
        {
            //Interactable bir objeye vurarsa interactable a ata
      
            if (_hit.transform.CompareTag("Teleport"))
            {

                canvas.transform.position = new Vector3(_hit.transform.position.x, _hit.transform.position.y, _hit.transform.position.z);
                canvas.transform.LookAt(gameObject.transform);
              
                if (imgGaze.fillAmount == 1) 
                {
                    //Teleport tagına sahip bütün objeleri listeye koy, box collider ve mesh renderarlarını aktifleştir
                    foreach (GameObject g in all_Objs)
                    {
                        g.GetComponent<MeshRenderer>().enabled = true;
                        g.GetComponent<BoxCollider>().enabled = true;
                    }

                    //Ray ile vurulan objedeki ışınlanma fonksiyonunu çağır 
                    _hit.transform.gameObject.GetComponent<Teleport>().TeleportPlayer();
                }
                
            }
            else if (_hit.transform.CompareTag("SceneWriting"))
            {
                canvas.transform.position = new Vector3(_hit.transform.position.x, _hit.transform.position.y-.6f, _hit.transform.position.z);
                canvas.transform.LookAt(gameObject.transform);
                if (imgGaze2.fillAmount == 1)
                {
                    _hit.transform.gameObject.GetComponent<SceneSwitch>().ChangeScene();   //Sahne değiştirme fonksiyonunu çağır
                }
            }
            else if (_hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground")) 
            {
                motor.MoveToPoint(_hit.point);
            }

        }
    }

    public void GVROn()
    {
        gvrStatus = true;
    }
    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
    }
    public void GVRWritingOn() {
        gvrStatus2 = true;
    }
    public void GVRWritingOff(){
        gvrStatus2 = false;
        gvrTimer = 0;
        imgGaze2.fillAmount = 0;
    }
}
