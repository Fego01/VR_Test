using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using UnityEngine;


public class Move_Player : MonoBehaviour
{


    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;

    private MoveCtrl controller;




  

    void Start()
    {
        controller = GameObject.Find("[CameraRig]").GetComponent<MoveCtrl>();
    }

    void Update()
    {
        if (grabAction.GetLastStateDown(handType))
        {
            controller.moveType = MoveCtrl.MoveType.LOOK_AT;
        }

        if (grabAction.GetLastStateUp(handType))
        {
            controller.moveType = MoveCtrl.MoveType.DAYDREAM;
        }


      
    }
}
