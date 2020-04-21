using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDepthChange : MonoBehaviour {

	public void Do( int depth ) {
		GetComponent<Camera>().depth = depth;
	}
}
