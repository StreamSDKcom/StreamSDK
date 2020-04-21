using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicPlay : MonoBehaviour
{

	private AudioSource mic;

    // Start is called before the first frame update
    public void Start()
    {
		#if !UNITY_WEBGL
		mic = gameObject.AddComponent<AudioSource>();
        if( Microphone.devices.Length >= 1 ) {
			mic.clip = Microphone.Start( "", true, 1, 44100 );
			mic.loop = true;
			while ( !( Microphone.GetPosition( "" ) > 44100 * Time.fixedDeltaTime ) ){}
			mic.Play();
		}
		#endif
    }
}
