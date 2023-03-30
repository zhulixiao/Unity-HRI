using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


using Unity.Robotics.ROSTCPConnector;
using PosRot = RosMessageTypes.UnityRoboticsDemo.PosRotMsg;


public class game : MonoBehaviour
{
    public cherryPlayer cherryPlayer;
    public gemPlayer gemPlayer;

    ROSConnection rosPub;

    private GameObject gem;
    private GameObject cherry;

    private GameObject eagle;
    private Vector3 showCherry;
    private Vector3 showGem;

    private Vector3 hide;
    private Vector3 hideEagle;
    // Hide after the background
    // Start is called before the first frame update

    private bool lockGem = false;
    private bool lockCherry = false;

    private float speed = 100;
    void Start()
    {
        Application.targetFrameRate = 300;
        // cherryPlayer.Start();
        // gemPlayer.Start();
        showCherry = new Vector3(0,1,0);
        showGem = new Vector3(0,-1,0);
        hide = new Vector3(0,0,-10);
        gem = GameObject.Find("gem");
        gem.transform.position = hide;
        cherry = GameObject.Find("cherry");
        cherry.transform.position = hide;

        ROSConnection.GetOrCreateInstance().Subscribe<PosRot>("start_pub", UpdateStart);

    }

    // Update is called once per frame
    void UpdateStart(PosRot startFlag)
    {   
        if(startFlag.pos_z == 0){
            gem.transform.position = showGem;
            cherry.transform.position = showCherry;
        } else if (startFlag.pos_z == -10) {
            gem.transform.position = hide;
            gemPlayer.lockGem = false;
            cherry.transform.position = hide;
            cherryPlayer.lockCherry = false;
        }
    }

}
