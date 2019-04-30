using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class grab_object : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;

    private GameObject CollidingObject;
    private GameObject objectInHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(grabAction.GetLastStateDown(handType))
            if (CollidingObject)
                GrabObject();
        
        if (grabAction.GetLastStateUp(handType))
            if (objectInHand)
                ReleaseObject();
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);

    }
    public void OnTriggerExit(Collider other)
    {
        if (!CollidingObject)
            return;
        CollidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if(CollidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        CollidingObject = col.gameObject;
    }

    private void GrabObject()
    {
        objectInHand = CollidingObject;
        CollidingObject = null;

        var joint = AddFixedjoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedjoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;

        return fx;
    }

    private void ReleaseObject()
    {
        if(GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity =
                controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity =
                controllerPose.GetVelocity();
        }
        objectInHand = null;
    }

}
