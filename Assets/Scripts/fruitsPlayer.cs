using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Unity.Robotics.ROSTCPConnector;
using PosRot = RosMessageTypes.UnityRoboticsDemo.PosRotMsg;

public class fruitsPlayer : MonoBehaviour
{


    public List<GameObject> fruits = new List<GameObject>();
    
    private string[] fruitsName = {"Apple","Avocado","Banana","Coconut","Grape1","Grape2","Kiwi","Lemon","Orange","Peach","Pear","Pineapple","Strawberry","Watermelon"};
    
    private GameObject eagle;
    private GameObject trail;
    private Vector3 show;
    private Vector3 hide;

    // private Vector3 hideParticles;

    public bool lockfruits = false;
    public bool newfruits = false;
    public static int random = 0;
    //rivate float speed = 10000;

    // Start is called before the first frame update
    public void Start()
    {

        Debug.Log("fruits are started");

        show = new Vector3(0,-1,0);
        hide = new Vector3(0,-1,-10);
        // hideParticles = new Vector3(-2,-2,-100);
        // fruits = GameObject.Find("fruits");
        // fruits.transform.position = show;

        for(int i = 0; i < fruitsName.Length; i++){
            fruits.Add(GameObject.Find(fruitsName[i]));
            //fruits[i].transform.position = hide;
        }
        
        eagle = GameObject.Find("fruits-picker");
        trail = GameObject.Find("Trail");

        // Assuming the robot is picking fruits
        ROSConnection.GetOrCreateInstance().Subscribe<PosRot>("player2_pub", UpdatePos);

        // eagle movement is controlled by the mouse


    }

    // a function to listen to a mouse movement
    // void Update()
    // {
    //     if(Input.GetMouseButtonDown(0)){
    //         Debug.Log("mouse is clicked");
    //         Debug.Log(Input.mousePosition);
    //     }
    //     Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     fruits.transform.position = mousePosition;
    //     if(fruits.transform.position.x < -3.9 && fruits.transform.position.y < -3.9 &&
    //     fruits.transform.position.x > -4.1 && fruits.transform.position.y > -4.1 ){
    //         // cherry.transform.position = show;
    //         fruits.transform.position = show;
    //         lockfruits = false;
    //     }
    // }


    // Update is called once per frame
    void UpdatePos(PosRot hand)
    {
        if(hand.pos_x <= 4.45 && hand.pos_x >= -4.45 && hand.pos_y <= 4.5 && hand.pos_y >= -4.5){
            // Debug.Log("hand is in the range");
            float x = hand.pos_x;
            float y = hand.pos_y;
            Vector3 movement =new Vector3(x,y,0);
            eagle.transform.position = movement;
            trail.transform.position = movement;
        } else {
            // Debug.Log("hand is out of the range");
            // out of the range
            // don't move the eagle
            return;
        }



        // Debug.Log(x);
        // Debug.Log(y);

        // particles.transform.position = movement;

        // random a fruit
        if(newfruits == true){
            random = Random.Range(0,fruitsName.Length);
            fruits[random].transform.position = show;
            newfruits = false;
        }   
        
        // Debug.Log(random);
        // Debug.Log(fruits[random].transform.position);
        // the fruits player's turn
        if(fruits[random].transform.position.z == 0){
            // float x = hand.pos_x;
            // float y = hand.pos_y;

            // Debug.Log(x);
            // Debug.Log(y);
            // Vector3 movement =new Vector3(x,y,0);

            //eagle.transform.position = Vector3.MoveTowards(eagle.transform.position, movement, speed * Time.deltaTime);
            //eagle.transform.position = movement;
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

            //fruits.transform.position = eagle.transform.position;
            // If the player has picked fruits
            if(lockfruits) fruits[random].transform.position = eagle.transform.position;

            if(Mathf.Abs(fruits[random].transform.position.x - eagle.transform.position.x) < 0.5 && Mathf.Abs(fruits[random].transform.position.y - eagle.transform.position.y) < 0.5 && !lockfruits){
                fruits[random].transform.position = eagle.transform.position;
                lockfruits = true;
            }

            
            if(fruits[random].transform.position.x < -4.45){
                fruits[random].transform.position = new Vector3((float)-4.45,fruits[random].transform.position.y,0);
            }

            if(fruits[random].transform.position.x > 4.45){
                fruits[random].transform.position = new Vector3((float)4.45,fruits[random].transform.position.y,0);
            }

            if(fruits[random].transform.position.y < -4.5){
                fruits[random].transform.position = new Vector3(fruits[random].transform.position.x,(float)-4.5,0);
            }

            if(fruits[random].transform.position.y > 4.5){
                fruits[random].transform.position = new Vector3(fruits[random].transform.position.x,(float)4.5,0);
            }

            if(fruits[random].transform.position.x < -3.8 && fruits[random].transform.position.y < -3.8 &&
            fruits[random].transform.position.x > -4.2 && fruits[random].transform.position.y > -4.2 ){
                // cherry.transform.position = show;
                for(int i = 0; i < fruitsName.Length; i++){
                    fruits[i].transform.position = hide;
                }
                lockfruits = false;
                newfruits = true;
            }
        } else {
            lockfruits = false;
            newfruits = false;
            random = 0;
            for(int i = 0; i < fruitsName.Length; i++){
                fruits[i].transform.position = hide;
            }
        }
        
    }

    // open a file and store the time
    // if the file is not exist, create a new file
    // if the file is exist, append the time to the bottom of the file

    // public void writeTime(){
    //     string path = "Assets/Scripts/fruits_time.txt";
    //     //Write some text to the test.txt file
    //     StreamWriter writer = new StreamWriter(path, true);
    //     writer.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    //     writer.Close();
    // }

}
