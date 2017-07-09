using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class MotionControllers : MonoBehaviour
{ 
    public VRNode NodeType;

	// Update is called once per frame
	void Update ()
	{
	    this.transform.localPosition = InputTracking.GetLocalPosition(NodeType);
	    this.transform.localRotation = InputTracking.GetLocalRotation(NodeType);
	}
}
