using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using StreamSDKSpace;
using System;

[Serializable]
public class SendData {
	public bool audio = true;
	public bool video = true;
	public bool input = false;
	public bool mouse = false;
}

//Extend NetTech as needed, StreamSDK supports any networking technology!
//public enum NetTech{ photon, tnet, tcp, udp, unet, webrtc };

//Currently provided by Midnight Status
//public enum NetTech{ photon, tnet };
public enum NetTech{ photon }; //TNet support has been removed in version 3.0, will revisit at a later time. The structure of extending the StreamSDKTransporter is still visible though. Extend at your leisure

public class StreamSDKTransporter : MonoBehaviour {

	public GameObject contentContainer;
	public string     ipAddress;
	public NetTech    netTech;
	public int        numberReceivers;
	public bool       overridePhotonServerSettings;
	public bool       reloadScene;
	public string     roomName;
	public int        sendRate = 50;
	public bool       test;
	public SendData[] sendData;
	/*
	public bool       sendAudio = true;
	public bool       sendVideo = true;
	public bool       sendControl;
	public bool       sendMouse;
	*/
		
	public static     StreamSDKTransporter instance;

	private Coroutine  coroutine;

	private SendData[] defaultSendData;

	private int        sendDataIndex;

	/*
	private bool       defaultSendAudio;
	private bool       defaultSendVideo;
	private bool       defaultSendControl;
	private bool       defaultSendMouse;
	*/

	private bool       doStream;
	private byte[]     lastMouse;
	private string     ipAddressA;
	private string     ipAddressB;
	private string     ipAddressC;
	private string     ipAddressD;
	
	public void JoinServer( bool pTest ) {

		switch( netTech ) {

			case NetTech.photon:
					doStream = true;
					test = pTest;						
					if( coroutine != null )
						StopCoroutine( coroutine );
					coroutine = StartCoroutine( PhotonWrapper.instance.JoinRoom( roomName, test ) );
				break;
			/*
			case NetTech.tnet:
					doStream = true;
					test = pTest
					if( coroutine != null )
						StopCoroutine( coroutine );
					//coroutine = StartCoroutine( TNetWrapper.instance.JoinRoom( roomName, test ) );
				break;
			
			case NetTech.tcp:
				break;
	
			case NetTech.udp:
				break;

			case NetTech.unet:
				break;
			
			case NetTech.webrtc:
				break;
			*/

		}

	}

	public void QuitServer() {
		
		test = false;

		switch( netTech ) {

			case NetTech.photon:
					doStream    = false;
					for( int i = 0; i < numberReceivers; i++ ) {
						if( i >= sendData.Length )
							break;
						sendData[ i ].audio = defaultSendData[ i ].audio;
						sendData[ i ].video = defaultSendData[ i ].video;
						sendData[ i ].input = defaultSendData[ i ].input;
						sendData[ i ].mouse = defaultSendData[ i ].mouse;
					}
					StartCoroutine( PhotonWrapper.instance.Restart() );
				break;
			/*
			case NetTech.tnet:
					doStream = false;
					StartCoroutine( TNetWrapper.instance.Restart() );
				break;

			
			case NetTech.tcp:
				break;
		
			case NetTech.udp:
				break;

			case NetTech.unet:
				break;
			
			case NetTech.webrtc:
				break;
			*/
			
		}

	}

	IEnumerator Start() {
		
		doStream           = false;

		defaultSendData = new SendData[ sendData.Length ];

		for( int i = 0; i < defaultSendData.Length; i++ ) {
			defaultSendData[i] = new SendData {
				audio = sendData[i].audio,
				video = sendData[i].video,
				input = sendData[i].input,
				mouse = sendData[i].mouse
			};
		}
		
		yield return new WaitUntil( ()=> StreamSDK.instance != null );
		
		StreamSDK.instance.numberReceivers = numberReceivers;

		StreamSDK.InitInput( numberReceivers );
		StreamSDK.InitMouse( numberReceivers );
		
		StreamSDKTransporter.instance = this;
		
		switch( netTech ) {

			case NetTech.photon:
					PhotonWrapper photonWrapper = gameObject.AddComponent<PhotonWrapper>();
				break;
			/*
			case NetTech.tnet:
					//TNetWrapper tnetWrapper = gameObject.AddComponent<TNetWrapper>();
				break;
			
			
			case NetTech.tcp:
				break;
		
			case NetTech.udp:
				break;

			case NetTech.unet:
				break;
			
			case NetTech.webrtc:
				break;
			*/

		}
	}
	
	
	public void SetIPAddressA( string octet ) {
			ipAddressA = octet;
			SetIPAddressByOctets();
	}
	
	public void SetIPAddressB( string octet ) {
			ipAddressB = octet;
			SetIPAddressByOctets();
	}
	
	public void SetIPAddressByOctets() {
		ipAddress = ipAddressA + "." + ipAddressB + "." + ipAddressC + "." + ipAddressD;	
	}
	
	public void SetIPAddressC( string octet ) {
			ipAddressC = octet;
			SetIPAddressByOctets();
	}
	
	public void SetIPAddressD( string octet ) {
			ipAddressD = octet;
			SetIPAddressByOctets();
	}
	
	public void SetPhotonRegion( int pPhotonRegion ) {
		PlayerPrefs.SetInt( "StreamSDKPhotonRegion", pPhotonRegion );	
	}
	
	public void SetPhotonProtocol( int pPhotonProtocol ) {
		PlayerPrefs.SetInt( "StreamSDKPhotonProtocol", pPhotonProtocol );
	}
	
	public void SetSendAudio( bool flag, int receiver ) {
		sendData[ receiver ].audio = flag;	
	}
	
	public void SetSendVideo( bool flag, int receiver ) {
		sendData[ receiver ].video = flag;
	}
	
	public void SetSendInput( bool flag, int receiver ) {
		sendData[ receiver ].input = flag;	
	}
	
	public void SetSendMouse( bool flag, int receiver ) {
		sendData[ receiver ].mouse = flag;	
	}

	public void StartServer( bool pTest ) {

		switch( netTech ) {

			case NetTech.photon:
					doStream = true;
					test = pTest;
					if( coroutine != null )
						StopCoroutine( coroutine );
					coroutine = StartCoroutine( PhotonWrapper.instance.CreateRoom( roomName, test ) );
				break;
			/*
			case NetTech.tnet:
					doStream = true;
					test = pTest;
					if( coroutine != null )
						StopCoroutine( coroutine );
					//coroutine = StartCoroutine( TNetWrapper.instance.CreateRoom( roomName, test ) );
				break;

			case NetTech.tcp:
				break;
		
			case NetTech.udp:
				break;

			case NetTech.unet:
				break;
			
			case NetTech.webrtc:
				break;
			*/

		}
	}

	void FixedUpdate() {
		if( !doStream )
			return;

		for( int i = 0; i < sendData.Length; i++ ) {
			if( sendData[ i ].audio && sendData[ i ].video )
				SendStream( i );
			if( sendData[ i ].audio && !sendData[ i ].video )
				SendAudio( i );
			if( !sendData[ i ].audio && sendData[ i ].video )
				SendVideo( i );	
			if( sendData[ i ].input )
				SendInput( i );
			if( sendData[ i ].mouse )
				SendMouse( i );
		}

		StreamSDK.KillPackets();
	}

	void SendAudio( int receiver ) {
		byte[] audio = StreamSDK.GetAudio();
		if( audio != null ) {
			switch( netTech ) {
				case NetTech.photon:
					PhotonWrapper.instance.SendAudio( audio, receiver );
					break;
				/*
				case NetTech.tnet:
					TNetWrapper.instance.SendAudio( audio, receiver );
					break;
				*/
			}
		}
	}

	void SendInput( int receiver ) {
		byte[] input = StreamSDK.GetInput();
		if( input != null ) {
			switch( netTech ) {
				case NetTech.photon:
					PhotonWrapper.instance.SendInput( input, receiver );
					break;
					
				//case NetTech.tnet:
					//TNetWrapper.instance.SendInput( input, receiver );
					//break;
					
			}
		}
	}


	void SendMouse( int receiver ) {
		#if !UNITY_WSA
		byte[] mouse = StreamSDK.GetMouse();
		if( mouse != null ) {
			switch( netTech ) {
				case NetTech.photon:
					PhotonWrapper.instance.SendMouse( mouse, receiver );
					break;
				/*
				case NetTech.tnet:
					TNetWrapper.instance.SendMouse( mouse, receiver );
					break;
				*/
			}	
		}
		#endif
	}

	void SendStream( int receiver ) {
		byte[] stream = StreamSDK.GetStream();
		if( stream != null ) {
			switch( netTech ) {
				case NetTech.photon:
					PhotonWrapper.instance.SendStream( stream, receiver );
					break;
				/*
				case NetTech.tnet:
					TNetWrapper.instance.SendStream( stream, receiver );
					break;
				*/
			}	
		}
	}

	void SendVideo( int receiver ) {
		byte[] video = StreamSDK.GetVideo();
		if( video != null ) {
			switch( netTech ) {
				case NetTech.photon:
					PhotonWrapper.instance.SendVideo( video, receiver );
					break;
				/*
				case NetTech.tnet:
					TNetWrapper.instance.SendVideo( video, receiver );
					break;
				*/
			}	
		}
	}
}
