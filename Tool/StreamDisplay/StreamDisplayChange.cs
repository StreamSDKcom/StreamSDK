using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StreamSDKSpace;

public class StreamDisplayChange : MonoBehaviour {

	public void Do( RawImage newStreamDisplay ) {
		if( StreamSDK.instance.streamDisplays.Count > 0 )
			StreamSDK.instance.streamDisplays[ 0 ] = newStreamDisplay;
	}
}
