using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Unity.Robotics.ROSTCPConnector;
using PosRot = RosMessageTypes.UnityRoboticsDemo.PosRotMsg;

public class cherryPlayer : MonoBehaviour
{

    private GameObject gem;
    private GameObject eagle;
    private Vector3 show;
    private Vector3 hide;

    private GameObject cherry;

    private bool lockCherry = false;
    private float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        show = new Vector3(0,0,0);
        hide = new Vector3(0,0,-10);
        gem = GameObject.Find("gem");
        cherry = GameObject.Find("cherry");
        eagle = GameObject.Find("cherry-picker");

        // For cherry player
        ROSConnection.GetOrCreateInstance().Subscribe<PosRot>("cherry_pos", UpdatePos);


    }

    // Update is called once per frame
    void UpdatePos(PosRot hand)
    {
        // the cherry player's turn
        if(cherry.transform.position.z == 0){
            float x = hand.pos_x;
            float y = hand.pos_y;

            Debug.Log(x);
            Debug.Log(y);
            Vector3 movement =new Vector3(x,y,0);

            eagle.transform.position = Vector3.MoveTowards(eagle.transform.position, movement, speed * Time.deltaTime);
            if(eagle.transform.position.x < -4.45){
                eagle.transform.position = new Vector3((float)-4.45,eagle.transform.position.y,0);
            }

            if(eagle.transform.position.x > 4.45){
                eagle.transform.position = new Vector3((float)4.45,eagle.transform.position.y,0);
            }

            if(eagle.transform.position.y < -4.5){
                eagle.transform.position = new Vector3(eagle.transform.position.x,(float)-4.5,0);
            }

            if(eagle.transform.position.y > 4.5){
                eagle.transform.position = new Vector3(eagle.transform.position.x,(float)4.5,0);
            }

            if(lockCherry) cherry.transform.position = eagle.transform.position;
            
            if(Mathf.Abs(cherry.transform.position.x - eagle.transform.position.x) < 0.5 && Mathf.Abs(cherry.transform.position.y - eagle.transform.position.y) < 0.5 && !lockCherry){
                cherry.transform.position = eagle.transform.position;
                lockCherry = true;
            }

            if(cherry.transform.position.x < -4.1){
                cherry.transform.position = new Vector3((float)-4.1,cherry.transform.position.y,0);
            }

            if(cherry.transform.position.x > 4.3){
                cherry.transform.position = new Vector3((float)4.3,cherry.transform.position.y,0);
            }

            if(cherry.transform.position.y < -4.3){
                cherry.transform.position = new Vector3(cherry.transform.position.x,(float)-4.3,0);
            }

            if(cherry.transform.position.y > 4.24){
                cherry.transform.position = new Vector3(cherry.transform.position.x,(float)4.24,0);
            }


            if(cherry.transform.position.x > 3.2 && cherry.transform.position.y > 3.2){
                cherry.transform.position = hide;
                gem.transform.position = show;
                lockCherry = false;
            }            

        
        
        }        
    }
}
