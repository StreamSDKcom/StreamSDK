using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureScreen : MonoBehaviour
{
	private RawImage rawImage;
	private Texture2D texture2D;

	public static CaptureScreen instance;

    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
		StartCoroutine( DoCapture() );
    }

    // Update is called once per frame
    public IEnumerator DoCapture()
    {
		yield return new WaitForEndOfFrame();
		Destroy( rawImage.texture );
        texture2D = ScreenCapture.CaptureScreenshotAsTexture();
		texture2D.filterMode = FilterMode.Point;
		rawImage.texture = texture2D;
		texture2D.Apply( false, false );
		StartCoroutine( DoCapture() );
    }
}
