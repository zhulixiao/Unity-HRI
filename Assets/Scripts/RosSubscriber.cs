using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using PosRot = RosMessageTypes.UnityRoboticsDemo.PosRotMsg;

public class RosSubscriber : MonoBehaviour
{

    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<PosRot>("chatter_xy", UpdatePos);
    }

    void UpdatePos(PosRot hand) {
        Debug.Log(hand.pos_x);
        Debug.Log(hand.pos_y);
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