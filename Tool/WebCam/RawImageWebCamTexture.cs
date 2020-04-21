using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImageWebCamTexture : MonoBehaviour {
	
	public int width = 1280;
	public int height = 720;

	public static WebCamTexture webCamTexture;

	IEnumerator Start () {	
	
		yield return new WaitForSeconds( 1.0f );
				
		webCamTexture = new WebCamTexture( width, height );
		
		GetComponent<RawImage>().texture = webCamTexture;

		webCamTexture.Play();		
	}

	void OnDestroy() {
		if( webCamTexture != null )
			webCamTexture.Stop();
	}

	void LateUpdate() {
		if( webCamTexture != null ) {
			float physical = (float)webCamTexture.width / (float)webCamTexture.height;
			GetComponent<AspectRatioFitter>().aspectRatio = physical;
			
			float scaleY = webCamTexture.videoVerticallyMirrored ? -1.0f : 1.0f;
			GetComponent<RectTransform>().localScale = new Vector3( 1, scaleY, 1 );
	
			int angle = -webCamTexture.videoRotationAngle;
			GetComponent<RectTransform>().localEulerAngles = new Vector3( 0, 0, angle );	
		}
	}
}
