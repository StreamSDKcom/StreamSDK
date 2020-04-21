using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewportChange : MonoBehaviour {

	public Rect  newCameraViewportRect;

	public void DoChangeViewportRect() {
		GetComponent<Camera>().rect = newCameraViewportRect;
	}	
}
