using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneRawImageWebCamTexture : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds( 1 );
		yield return new WaitUntil( ()=>RawImageWebCamTexture.webCamTexture != null );
		RawImage rawImage;
		rawImage = gameObject.GetComponent<RawImage>();
		if( rawImage == null )
			rawImage = gameObject.AddComponent<RawImage>();
		rawImage.texture = RawImageWebCamTexture.webCamTexture;
	}
	
	void LateUpdate() {
		if( RawImageWebCamTexture.webCamTexture != null ) {
			float physical = (float)RawImageWebCamTexture.webCamTexture.width / (float)RawImageWebCamTexture.webCamTexture.height;
			GetComponent<AspectRatioFitter>().aspectRatio = physical;
			
			float scaleY = RawImageWebCamTexture.webCamTexture.videoVerticallyMirrored ? -1.0f : 1.0f;
			GetComponent<RectTransform>().localScale = new Vector3( 1, scaleY, 1 );
	
			int angle = -RawImageWebCamTexture.webCamTexture.videoRotationAngle;
			GetComponent<RectTransform>().localEulerAngles = new Vector3( 0, 0, angle );	
		}
	}
}
