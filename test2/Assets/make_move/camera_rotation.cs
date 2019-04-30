using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class camera_rotation : MonoBehaviour
{

    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean touch_Pad;
    public SteamVR_Action_Boolean touch_position;

    public GameObject Main_camera;


    public GameObject pouch_pad_object;
    private Transform asdasd;
    int asdasdasd = 1;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(asdasd == null)
        {
            asdasd = pouch_pad_object.transform.Find("trackpad_touch");
            Debug.Log("asd");
        }
        
        //pouch_pad_object

        if (touch_Pad.GetLastStateDown(handType))
        {
            if (asdasd.localPosition.x > 0)
                asdasdasd = 1;
            if (asdasd.localPosition.x < 0)
                asdasdasd = -1;
            Main_camera.transform.Rotate(0, 15* asdasdasd, 0);
        }
        //if(touch_position.GetLastState(handType))
        //{
        //    if (asdasd.localPosition.x > 0)
        //        asdasdasd = 1;
        //    if (asdasd.localPosition.x < 0)
        //        asdasdasd = -1;

        //}

    }
}
