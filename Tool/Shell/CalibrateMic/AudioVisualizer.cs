using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using StreamSDKSpace;

public class AudioVisualizer : MonoBehaviour {
	
	public float  scalar;
	
	private float audioAverage;
	
	// Update is called once per frame
	public void Update() {
		
		Rect cRect = GetComponent<RectTransform>().rect;
		
		if( StreamSDK.instance == null ) {
			GetComponent<RectTransform>().localScale = new Vector2( 1, 1 );
			return;
		}
		
		if( StreamSDK.instance.audioPacket == null ) {
			GetComponent<RectTransform>().localScale = new Vector2( 1, 1 );
			return;
		}
		
		if( StreamSDK.instance.audioPacket.data == null ) {
			GetComponent<RectTransform>().localScale = new Vector2( 1, 1 );
			return;
		}
		
		if( StreamSDK.instance.audioPacket.data.Length <= 4 ) {
			GetComponent<RectTransform>().localScale = new Vector2( 1, 1 );
			return;
		}
		
		audioAverage = Mathf.Abs( AudioArray.ToFloat( CLZF2.Decompress( StreamSDK.instance.audioPacket.data ) ).ToList().Average() ) * scalar;
		GetComponent<RectTransform>().rect.Set( cRect.x, cRect.y, cRect.width, 50 + audioAverage );
		GetComponent<RectTransform>().localScale = new Vector2( 1, audioAverage );

	}
}
