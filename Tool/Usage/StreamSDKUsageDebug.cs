using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreamSDKSpace;

public class StreamSDKUsageDebug : MonoBehaviour
{
    IEnumerator Start()
    {
		yield return new WaitUntil( ()=>StreamSDK.instance != null );
				
		yield return new WaitForSeconds( 5 );
		
		//You may want to use these statistics to inform your own backend processes and/or maintain the appropriate StreamSDK license.
		Debug.Log( "Current Connection Cap = " + StreamSDK.GetUserConnectionsCap() );
		Debug.Log( "Current Connections = " + StreamSDK.GetUserConnectionsCurrent() );
		Debug.Log( "Peak Connections = " + StreamSDK.GetUserConnectionsPeak() );
    }
}