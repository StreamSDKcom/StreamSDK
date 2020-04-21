using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StreamSDKSpace;

public class StreamSDKErrorDebug : MonoBehaviour
{
    IEnumerator Start()
    {
		yield return new WaitUntil( ()=>StreamSDK.instance != null );
				
		yield return new WaitForSeconds( 3 );
		
		//You can use these boolean values to control the flow of your User Experience (UX) in case there are any problems.
		Debug.Log( "Error Connections = "             + StreamSDK.GetErrorConnections() );
		Debug.Log( "Error Credentials = "             + StreamSDK.GetErrorCredentials() );
		Debug.Log( "Error No Internet = "             + StreamSDK.GetErrorNoInternet() );
		Debug.Log( "Error Platform = "                + StreamSDK.GetErrorPlatform() );
		Debug.Log( "Error Version Deprecated = "      + StreamSDK.GetErrorVersionDeprecated() );
		Debug.Log( "Error Version Obsolete = "        + StreamSDK.GetErrorVersionObsolete() );
    }
}
