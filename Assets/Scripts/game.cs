using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Unity.Robotics.ROSTCPConnector;
using PosRot = RosMessageTypes.UnityRoboticsDemo.PosRotMsg;


public class game : MonoBehaviour
{


    private GameObject gem;
    private GameObject cherry;

    private GameObject eagle;
    private Vector3 show;

    private Vector3 hide;
    // Hide after the background
    // Start is called before the first frame update

    private bool lockGem = false;
    private bool lockCherry = false;

    private float speed = 100;
    void Start()
    {
        show = new Vector3(0,0,0);
        hide = new Vector3(0,0,-10);
        gem = GameObject.Find("gem");
        gem.transform.position = show;

        cherry = GameObject.Find("cherry");
        cherry.transform.position = hide;

        eagle = GameObject.Find("eagle");

        ROSConnection.GetOrCreateInstance().Subscribe<PosRot>("chatter_xy", UpdatePos);     
    }

    // Update is called once per frame
    void UpdatePos(PosRot hand)
    {

        // float x = Input.GetAxis("Horizontal");
        // float y = Input.GetAxis("Vertical");

        float x = hand.pos_x;
        float y = hand.pos_y;
        Vector3 movement =new Vector3(x,y,0);
        //eagle.transform.Translate(movement * speed * Time.deltaTime);
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
        if(lockGem) gem.transform.position = eagle.transform.position;


        
        if(cherry.transform.position.z == 0 ){
            //Debug.Log("cherry");


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
        } else if(gem.transform.position.z == 0){
            //Debug.Log("gem");
            // float x = Input.GetAxis("Horizontal");
            // float y = Input.GetAxis("Vertical");
            // Vector3 movement =new Vector3(x,y,0);
            // gem.transform.Translate(movement * speed * Time.deltaTime);

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
                cherry.transform.position = show;
                gem.transform.position = hide;
                lockGem = false;
            }
        } 
    }
}
