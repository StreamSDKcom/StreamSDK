using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StreamSDKSpace;

public class CalibrateMic : MonoBehaviour {
	
	private bool init;
	private StreamSDKSpace.AudioType tempAudioType;
	private bool tempAudioSet;
		
	public IEnumerator Start() {
		tempAudioSet = false;
		yield return new WaitUntil( ()=> StreamSDK.instance != null );
		yield return new WaitUntil( ()=> StreamSDKTransporter.instance != null );
		yield return new WaitForEndOfFrame();
		StreamSDK.AddClient( StreamSDK.instance.id );
		StreamSDK.InitMic( StreamSDKTransporter.instance.numberReceivers );
		init = true;
	}

	public void OnEnable() {
		init = false;
		StartCoroutine( Start() );
	}

	public void OnDisable() {
		if( tempAudioSet )
			StreamSDK.instance.streamSDKAdvancedOptions.audioType = tempAudioType;
		StreamSDK.RemoveClient( StreamSDK.instance.id );
		StreamSDK.StopAudio();
	}

	public void FixedUpdate() {		
		if( !init )
			return;
			
		if( !tempAudioSet ) {
			tempAudioType = StreamSDK.instance.streamSDKAdvancedOptions.audioType;
			StreamSDK.instance.streamSDKAdvancedOptions.audioType = StreamSDKSpace.AudioType.microphone;
			tempAudioSet = true;
		}

		StreamSDK.UpdateStreamRemoteAudio( StreamSDK.GetAudio() );	
	}
}
