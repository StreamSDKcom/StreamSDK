using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StreamSDKSpace;

public class StreamSDKUIManager : MonoBehaviour {

	public int          width      = 1280;
	public int          height     = 720;
	public bool         fullscreen = false;
	public GameObject[] containers;
	public EventSystem  eventSystem;
	public Text         message;

	public static StreamSDKUIManager instance;
			
	private void HideAllContainers() {
		for( int i = 0; i < containers.Length; i++ ) {
			if( containers[ i ] != null )
				containers[ i ].SetActive( false );
		}
	}
	
	public void ShowContainer( GameObject container ) {
		if( container != null ) {
			for( int i = 0; i < containers.Length; i++ ) {
				if( containers[ i ] != container ) {
					containers[ i ].SetActive( false );
				}
			}
	
			container.SetActive( true );
		}
	}

	// Use this for initialization
	IEnumerator Start () {
		
		if( eventSystem == null )
			eventSystem = EventSystem.current;
		
		eventSystem.enabled = false;
		
		Screen.SetResolution( width, height, fullscreen );
				
		yield return new WaitUntil( ()=>StreamSDK.instance != null );		
		yield return new WaitUntil( ()=>StreamSDKTransporter.instance != null );
		yield return new WaitForEndOfFrame();

		instance = this;
		
		ShowContainer( containers[ 0 ] );	
		
		eventSystem.enabled = true;
	}
}
