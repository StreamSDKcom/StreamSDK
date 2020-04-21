using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamSDKCursor : MonoBehaviour
{
	public void DoLock( bool locked ) {
		if( locked )
			Cursor.lockState = CursorLockMode.Locked;
		else
			Cursor.lockState = CursorLockMode.None;
	}

	public void DoVisible( bool visible ) {
		Cursor.visible = visible;
	}
}
