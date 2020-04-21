using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StreamSDKSpace;


[ RequireComponent( typeof( Dropdown ) ) ]
public class StreamSDKOptionDropdown : MonoBehaviour
{
	public Behaviour component;
	public string    message;
	public bool      sendOptionIndex;
	
	private Dropdown option;
	
	public IEnumerator Start() {
		
		yield return new WaitUntil( ()=>StreamSDK.instance != null );
		yield return new WaitUntil( ()=>StreamSDKTransporter.instance != null );
		
		if( option == null )
			option = GetComponent<Dropdown>();	
			
		if( component == null )
			component = FindObjectOfType<StreamSDK>();
			
		if( message == "" )
			message = "Set" + gameObject.name;
			
		if( option !=null ) {
			
			option.onValueChanged.AddListener( OnValueChanged );
			
			if( PlayerPrefs.GetInt( "StreamSDKOption_" + option.name ) == 1 ) {
				if( option != null ) {
					for( int i = 0; i < option.options.Count; i++ ) {
						if( option.options[ i ].text == PlayerPrefs.GetString( option.name ) )
							option.value = i;
					}
					option.RefreshShownValue();
				}
			} else {
				PlayerPrefs.SetString( option.name, option.options[ option.value ].text );
				PlayerPrefs.SetInt( "StreamSDKOption_" + option.name, 1 );
			}
			SetValue();
		} else {
			Debug.Log( "This component requires a Dropdown sister component!" );	
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
			option = GetComponent<Dropdown>();	
		if( option != null ) {
						
			PlayerPrefs.SetString( option.name, option.options[ option.value ].text );
			SetValue();
		} else
			Debug.Log( "This component requires a Dropdown sister component!" );
	}
	
	public void OnValueChanged( int index ) {
		if( StreamSDK.instance == null )
			return;
		if( StreamSDKTransporter.instance == null )
			return;
		
		if( option == null )
			option = GetComponent<Dropdown>();	
		
		if( option != null ) {
			PlayerPrefs.SetString( option.name, option.options[ option.value ].text );
			SetValue();
		} else
			Debug.Log( "This component requires a Dropdown sister component!" );	
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
				
		if( sendOptionIndex )
			component.SendMessage( message, option.value );
		else
			component.SendMessage( message, option.options[ option.value ].text );	
	}
}
