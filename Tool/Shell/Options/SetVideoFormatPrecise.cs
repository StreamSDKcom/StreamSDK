using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreamSDKSpace;

public class SetVideoFormatPrecise : MonoBehaviour
{
    public int width;
	public int height;
	
	public void Do() {
		StreamSDK.instance.SetVideoFormatPrecise( width, height );
	}
}
