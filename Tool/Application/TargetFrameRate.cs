using UnityEngine;
using System.Collections;

public class TargetFrameRate : MonoBehaviour {

	public int frameRate = 60;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = frameRate;
	}
}
