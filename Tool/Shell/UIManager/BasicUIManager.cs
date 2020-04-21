using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BasicUIManager : MonoBehaviour {

	public GameObject[] containers;
			
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
	
	public void Start() {
		ShowContainer( containers[ 0 ] );		
	}
}
