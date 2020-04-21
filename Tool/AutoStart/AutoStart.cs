using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreamSDKSpace;

public class AutoStart : MonoBehaviour
{
	public int    audioFrequency;
	public int    framerate;
	public string videoFormat;
	public int    quality;
	
    // Start is called before the first frame update
    IEnumerator Start()
    {
		yield return new WaitUntil( ()=>StreamSDKTransporter.instance != null );
		yield return new WaitUntil( ()=>PhotonWrapper.instance != null );

        StreamSDK.instance.SetAudioFrequency( audioFrequency );
		StreamSDK.instance.SetFramerate( framerate );
		StreamSDK.instance.SetVideoFormat( videoFormat );
		StreamSDK.instance.SetVideoQuality( quality );

		StreamSDKTransporter.instance.StartServer( false );
    }
}
