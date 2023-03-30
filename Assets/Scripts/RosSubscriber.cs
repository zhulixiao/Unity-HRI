using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosColor = RosMessageTypes.UnityRoboticsDemo.UnityColorMsg;

public class RosSubscriber : MonoBehaviour
{
    private GameObject light;
    void Start()
    {	
    	light = GameObject.Find("light");
        ROSConnection.GetOrCreateInstance().Subscribe<RosColor>("color_changer", ChangeColor);
    }

    void ChangeColor(RosColor newColor) {
        light.GetComponent<Renderer>().material.color = new Color32((byte)newColor.r, (byte)newColor.g, (byte)newColor.b, (byte)newColor.a);
    }
}



// using UnityEngine;
// using Unity.Robotics.ROSTCPConnector;
// using RosMessageTypes.UnityRoboticsDemo;
// //using RosColor = RosMessageTypes.UnityRoboticsDemo.UnityColorMsg;

// public class RosSubscriber : MonoBehaviour
// {
//     public GameObject cube;

//     void Start()
//     {
//         ROSConnection.GetOrCreateInstance().RegisterRosService<PositionServiceRequest, PositionServiceResponse>("chatter_xy");
//     }
    
//     void Update() {
        
//     }

// }
