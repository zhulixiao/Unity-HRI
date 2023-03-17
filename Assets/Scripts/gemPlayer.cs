using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Unity.Robotics.ROSTCPConnector;
using PosRot = RosMessageTypes.UnityRoboticsDemo.PosRotMsg;

public class gemPlayer : MonoBehaviour
{


    private GameObject gem;
    private GameObject eagle;
    private Vector3 show;
    private Vector3 hide;

    private GameObject cherry;

    private bool lockGem = false;
    private float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        show = new Vector3(0,-1,0);
        hide = new Vector3(0,0,-10);
        gem = GameObject.Find("gem");
        gem.transform.position = show;
        
        eagle = GameObject.Find("gem-picker");

        // Assuming the robot is picking gem
        ROSConnection.GetOrCreateInstance().Subscribe<PosRot>("player2_pub", UpdatePos);


    }

    // Update is called once per frame
    void UpdatePos(PosRot hand)
    {
        // the gem player's turn
        if(gem.transform.position.z == 0){
            float x = hand.pos_x * 40;
            float y = hand.pos_y * 40;

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

            //gem.transform.position = eagle.transform.position;
            // If the player has picked Gem
            if(lockGem) gem.transform.position = eagle.transform.position;

            if(Mathf.Abs(gem.transform.position.x - eagle.transform.position.x) < 0.5 && Mathf.Abs(gem.transform.position.y - eagle.transform.position.y) < 0.5 && !lockGem){
                gem.transform.position = eagle.transform.position;
                lockGem = true;
            }

            
            if(gem.transform.position.x < -4.45){
                gem.transform.position = new Vector3((float)-4.45,gem.transform.position.y,0);
            }

            if(gem.transform.position.x > 4.45){
                gem.transform.position = new Vector3((float)4.45,gem.transform.position.y,0);
            }

            if(gem.transform.position.y < -4.5){
                gem.transform.position = new Vector3(gem.transform.position.x,(float)-4.5,0);
            }

            if(gem.transform.position.y > 4.5){
                gem.transform.position = new Vector3(gem.transform.position.x,(float)4.5,0);
            }

            if(gem.transform.position.x < -3.2 && gem.transform.position.y < -3.2){
               // cherry.transform.position = show;
                gem.transform.position = show;
                lockGem = false;
            }

        }
        
    }
}
