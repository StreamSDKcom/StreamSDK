using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StreamSDKSpace;

public class StreamSDKMouseCursor : MonoBehaviour {

	public int index;
	public Camera streamCamera;
	public GraphicRaycaster graphicRaycaster;
	public EventSystem eventSystem;

	private Vector2 mousePos;
	private PointerEventData pointerEventData;
	List<RaycastResult> raycastResults = new List<RaycastResult>();

	// Use this for initialization
	void Start () {
		pointerEventData = new PointerEventData( eventSystem );
	}

	private void Update()
	{
		graphicRaycaster.Raycast( pointerEventData, raycastResults );
	}

	void FixedUpdate () {
		if( StreamSDK.instance == null )
			return;

		if( StreamSDK.GetMouseRemote( index ) == null ) {
			//Debug.Log( "GetMouseRemote == null" );
			return;
		}

		float scaleX = StreamSDK.instance.width < streamCamera.pixelRect.width ? ( StreamSDK.instance.width / streamCamera.pixelRect.width ) : ( streamCamera.pixelRect.width / StreamSDK.instance.width );
		float scaleY = StreamSDK.instance.height < streamCamera.pixelRect.height ? ( StreamSDK.instance.height / streamCamera.pixelRect.height ) : ( streamCamera.pixelRect.height / StreamSDK.instance.height );
		
		mousePos = new Vector2( ( StreamSDK.GetMouseRemote( index ).x ) * scaleX, ( StreamSDK.GetMouseRemote( index ).y ) * scaleY );
		
		transform.position = streamCamera.ViewportToWorldPoint( new Vector3( mousePos.x, mousePos.y, 1 ) );
		pointerEventData.position = new Vector2( mousePos.x * streamCamera.pixelWidth, mousePos.y * streamCamera.pixelHeight );
		
		if( StreamSDK.GetMouseRemote( index ).lmb ) {
			GetComponent<SpriteRenderer>().color = Color.white;
			foreach( RaycastResult raycastResult in raycastResults ) {
				if( raycastResult.gameObject != null ) {
					//Debug.Log( "RaycastHit = " + raycastResults[ 0 ].gameObject.name );
					raycastResult.gameObject.SendMessage( "OnClick", SendMessageOptions.DontRequireReceiver );
				}
			}
		}

		if( StreamSDK.GetMouseRemote( index ).rmb )
			GetComponent<SpriteRenderer>().color = Color.green;
		if( StreamSDK.GetMouseRemote( index ).mmb )
			GetComponent<SpriteRenderer>().color = Color.blue;

		raycastResults.Clear();
	}
}
