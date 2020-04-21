using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreamSDKSpace;

public class StreamSDKCharacterController : MonoBehaviour {

	public float speed;
	public int   id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int x = (int)StreamSDK.GetAxis( "Horizontal", id );
		int y = (int)StreamSDK.GetAxis( "Vertical", id );
		transform.position = new Vector3( transform.position.x + x * speed * Time.deltaTime, transform.position.y + y * speed * Time.deltaTime, 0 );
	}
}
