using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


using Unity.Robotics.ROSTCPConnector;
using PosRot = RosMessageTypes.UnityRoboticsDemo.PosRotMsg;


public class game : MonoBehaviour
{
    private cherryPlayer cherryplayer;
    private fruitsPlayer fruitsplayer;

    ROSConnection rosPub;

    private List<GameObject> fruits = new List<GameObject>();

    private string[] fruitsName = {"Apple","Avocado","Banana","Coconut","Grape1","Grape2","Kiwi","Lemon","Orange","Peach","Pear","Pineapple","Strawberry","Watermelon"};
    

    private GameObject cherry;

    private GameObject eagle;
    private Vector3 showCherry;
    private Vector3 showfruits;

    private Vector3 hide;
    private Vector3 hideEagle;
    // Hide after the background
    // Start is called before the first frame update

    private float speed = 100;
    void Start()
    {
        Application.targetFrameRate = 120;

        cherryplayer = FindObjectOfType<cherryPlayer>();
        fruitsplayer = FindObjectOfType<fruitsPlayer>();
        fruitsplayer.newfruits = false;
        fruitsplayer.lockfruits = false;
        cherryplayer.lockCherry = false;
        // cherryPlayer.Start();
        // fruitsPlayer.Start();
        showCherry = new Vector3(0,1,0);
        showfruits = new Vector3(0,-1,0);
        hide = new Vector3(0,0,-10);
        //fruits = GameObject.Find("fruits");
        //fruits.transform.position = hide;

        for(int i = 0; i < fruitsName.Length; i++){
            fruits.Add(GameObject.Find(fruitsName[i]));
            fruits[i].transform.position = hide;
        }
        cherry = GameObject.Find("cherry");
        cherry.transform.position = hide;

        ROSConnection.GetOrCreateInstance().Subscribe<PosRot>("start_pub", UpdateStart);

    }

    // Update is called once per frame
    void UpdateStart(PosRot startFlag)
    {   
        if(startFlag.pos_z == 0){
            //int random = Random.Range(0,fruitsName.Length);
            fruits[0].transform.position = showfruits;
            
            cherry.transform.position = showCherry;
        } else if (startFlag.pos_z == -10) {

            cherry.transform.position = hide;

            for(int i = 0; i < fruitsName.Length; i++){
                fruits[i].transform.position = hide;
            }
            //fruits[0].transform.position = hide;
            fruitsplayer.lockfruits = false;
            fruitsplayer.newfruits = false;
            cherryplayer.lockCherry = false;
        }
    }
    void OnDestroy() {
        var ros = ROSConnection.GetOrCreateInstance();
        if (ros != null) {
            ros.Unsubscribe("start_pub");
        }
    }

    void OnApplicationQuit() {
        var ros = FindObjectOfType<ROSConnection>(); // Don't create a new instance
        if (ros != null) {
            ros.Disconnect(); // Ensure it properly disconnects
        }
    }


}
