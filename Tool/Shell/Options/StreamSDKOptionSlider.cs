using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StreamSDKSpace;


[ RequireComponent( typeof( Slider ) ) ]
public class StreamSDKOptionSlider : MonoBehaviour
{
	public Behaviour component;
	public string    message;
	
	private Slider   option;
	
	public IEnumerator Start() {
		
		yield return new WaitUntil( ()=>StreamSDK.instance != null );
		yield return new WaitUntil( ()=>StreamSDKTransporter.instance != null );
		
		if( option == null )
			option = GetComponent<Slider>();
		
		if( component == null )
			component = FindObjectOfType<StreamSDK>();
			
		if( message == "" )
			message = "Set" + gameObject.name;	
			
		if( option !=null ) {
			
			option.onValueChanged.AddListener( OnValueChanged );
			
			if( PlayerPrefs.GetInt( "StreamSDKOption_" + option.name ) == 1 ) {
				if( option != null ) {
					option.value = System.Convert.ToSingle( PlayerPrefs.GetString( option.name ) );
				}
			} else {
				PlayerPrefs.SetString( option.name, option.value.ToString() );
				PlayerPrefs.SetInt( "StreamSDKOption_" + option.name, 1 );
			}
			SetValue();
		} else {
			Debug.Log( "This component requires a Slider sister component!" );	
		}
	}

	
    public void OnEnable() {
		StartCoroutine( Start() );
	}

	public void OnDisable() {
		if( StreamSDK.instance == null )
			return;
		if( StreamSDKTransporter.instance == null )
			return;
			
		if( option == null )
			option = GetComponent<Slider>();	
		if( option != null ) {
							
			PlayerPrefs.SetString( option.name, option.value.ToString() );
			SetValue();
		} else
			Debug.Log( "This component requires a Slider sister component!" );
		
		
	}
	
	public void OnValueChanged( float value ) {
		if( StreamSDK.instance == null )
			return;
		if( StreamSDKTransporter.instance == null )
			return;
			
		if( option == null )
			option = GetComponent<Slider>();	
		
		if( option != null ) {
			PlayerPrefs.SetString( option.name, option.value.ToString() );
			SetValue();
		} else
			Debug.Log( "This component requires a Slider sister component!" );
	}
	
	public void SetValue() {
		if( StreamSDK.instance == null )
			return;
		if( StreamSDKTransporter.instance == null )
			return;
		if( component == null )
			component = FindObjectOfType<StreamSDK>();
		if( component == null )
			return;
				
		component.SendMessage( message, option.value );	
	}
}
