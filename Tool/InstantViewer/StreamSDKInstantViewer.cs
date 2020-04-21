using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreamSDKSpace;

public class StreamSDKInstantViewer : MonoBehaviour {

	void OnEnable() {
		StartCoroutine( Init() );	
	}

	// Use this for initialization
	IEnumerator Init () {		
		yield return new WaitUntil( ()=>StreamSDK.instance != null );
		yield return new WaitUntil( ()=>StreamSDKTransporter.instance != null );
		yield return new WaitUntil( ()=>PhotonWrapper.instance != null );

		yield return new WaitForSeconds( 1 );
		StreamSDKTransporter.instance.JoinServer( false );
	}

}
