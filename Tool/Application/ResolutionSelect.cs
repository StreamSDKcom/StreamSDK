using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionSelect : MonoBehaviour {

	public List<Vector2Int> resolutions;
	public List<bool>       fullscreen;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		for( int i = 1; i < 10; i++ ) {
			if( Input.GetKeyDown( i.ToString() ) ) {
				if( i <= resolutions.Count ) {
					Screen.SetResolution( resolutions[ i - 1 ].x, resolutions[ i - 1 ].y, fullscreen[ i - 1 ] );
				}
			}
		}
	}
}
