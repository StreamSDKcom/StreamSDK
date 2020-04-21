#define submit

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StreamSDKSpace;
using System.Linq;
using System.Runtime.InteropServices;

#if submit
//This is a placeholder class, once Photon is imported comment out the #define submit at the top of this script to access the full class below
public class PhotonWrapper : MonoBehaviour {
	public bool       test;
	public int        numberReceivers = 1;
	public int        sendRate = 30;
	public GameObject contentContainer;
	public bool       overridePhotonServerSettings;
	public bool       reloadScene;

	public static PhotonWrapper instance;

	[HideInInspector]
	public bool initialized;

	private string     roomName;

	public IEnumerator Restart() {	
		yield return new WaitForSeconds( 0 );
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
		Resources.UnloadUnusedAssets();
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}

	public IEnumerator JoinRoom( string pRoomName, bool pTest ) {
		yield return new WaitForSeconds( 0 );
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
	}

	public IEnumerator CreateRoom( string pRoomName, bool pTest ) {
		yield return new WaitForSeconds( 0 );
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
	}

	void Start() {
		PhotonWrapper.instance = this;
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
	}

	public void SendAudio( byte[] data, int receiver ) {
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
	}

	public void SendData( byte[] data, int receiver ) {
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
	}

	public void SendInput( byte[] data, int receiver ) {
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
	}

	public void SendMouse( byte[] data, int receiver ) {
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
	}

	public void SendVideo( byte[] data, int receiver ) {
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
	}
	
	public void SendStream( byte[] data, int receiver ) {
		Debug.LogError( "You need to import Photon and comment out this placeholder class!" );
	}
}

#else

//This is the full PhotonWrapper class, comment out the above placeholder class, then uncomment this class once Photon is imported.
public class PhotonWrapper : Photon.MonoBehaviour {

	public bool       test;
	public int        numberReceivers = 1;
	public int        sendRate = 30;
	public GameObject contentContainer;
	public bool       overridePhotonServerSettings;
	public bool       reloadScene;

	public static PhotonWrapper instance;

	[HideInInspector]
	public bool initialized;

	private bool       joinedLobby;
	private bool       joinedRoom;
	private PhotonView photonView;
	private string     roomName;

	private float      packetStartTime;
	private float      packetCount;

	public IEnumerator Restart() {		
		if( !initialized ) {
			StreamSDKUIManager.instance.ShowContainer( StreamSDKUIManager.instance.containers[ 0 ] );
			yield break;
		}
	
		joinedLobby = false;
		joinedRoom = false;
		test = false;
				
		if( reloadScene ) {
			initialized = false;
		}

		yield return new WaitForEndOfFrame();
		
		if( photonView.isMine )
			PhotonNetwork.RemoveRPCs( photonView );

		if( PhotonNetwork.room != null )
			PhotonNetwork.LeaveRoom();

		if( reloadScene ) {
			if( PhotonNetwork.lobby != null )
				PhotonNetwork.LeaveLobby();
			
			PhotonNetwork.lobby = null;
			
			DoDisconnect();

			yield return new WaitUntil( ()=>PhotonNetwork.connectionState == ConnectionState.Disconnected );
		}

		Resources.UnloadUnusedAssets();

		StreamSDK.Stop();

		if( reloadScene ) {
			#if UNITY_WEBGL
				Application.Quit();
			#else
				SceneManager.LoadScene( SceneManager.GetActiveScene().name );
			#endif
		} else {
			StreamSDKUIManager.instance.ShowContainer( StreamSDKUIManager.instance.containers[ 0 ] );	
		}
	}

	public IEnumerator JoinRoom( string pRoomName, bool pTest ) {
		if( test != pTest )
			test = pTest;

		DoConnect();

		yield return new WaitUntil( ()=>PhotonNetwork.connectedAndReady );

		roomName = pRoomName;
		LoadBalancingPeer.OutgoingStreamBufferSize = 512000;//65536;
		PhotonNetwork.NetworkStatisticsEnabled = false;
		PhotonNetwork.sendRate = sendRate;//(int)StreamSDK.instance.framerate;
		PhotonNetwork.JoinRoom( roomName );
		if( StreamSDKUIManager.instance != null )
			StreamSDKUIManager.instance.ShowContainer( contentContainer );
	}

	public IEnumerator CreateRoom( string pRoomName, bool pTest ) {		
		if( test != pTest )
			test = pTest;
		

		if( test ) {
			DoDisconnect();

			StreamSDK.instance.id = -1;

			yield return new WaitUntil( ()=>PhotonNetwork.connectionState == ConnectionState.Disconnected );

			roomName = pRoomName;
			PhotonNetwork.offlineMode = true;
	
			RoomOptions roomOptions = new RoomOptions();
			roomOptions.MaxPlayers = (byte)( numberReceivers + 1 );
			roomOptions.CleanupCacheOnLeave = true;
			roomOptions.IsVisible = true;
			roomOptions.IsOpen = true;
			
			TypedLobby typedLobby = new TypedLobby();
			typedLobby.Name = "chat";
			typedLobby.Type = LobbyType.Default;
			
			LoadBalancingPeer.OutgoingStreamBufferSize = 512000;//65536;
			
			PhotonNetwork.NetworkStatisticsEnabled = false;
			PhotonNetwork.sendRate = sendRate;//(int)StreamSDK.instance.framerate;
			PhotonNetwork.CreateRoom( roomName, roomOptions, typedLobby );
			StreamSDKUIManager.instance.ShowContainer( contentContainer );
		} else {	
			roomName = pRoomName;
			PhotonNetwork.offlineMode = false;
	
			RoomOptions roomOptions = new RoomOptions();
			roomOptions.MaxPlayers = (byte)( numberReceivers + 1 );
			roomOptions.CleanupCacheOnLeave = true;
			roomOptions.IsVisible = true;
			roomOptions.IsOpen = true;

			TypedLobby typedLobby = new TypedLobby();
			typedLobby.Name = "chat";
			typedLobby.Type = LobbyType.Default;

			LoadBalancingPeer.OutgoingStreamBufferSize = 512000;//65536;
			
			PhotonNetwork.NetworkStatisticsEnabled = false;
			PhotonNetwork.sendRate = sendRate;//(int)StreamSDK.instance.framerate;
								
			DoConnect();

			yield return new WaitUntil( ()=>PhotonNetwork.connectedAndReady );
			
			yield return new WaitUntil( ()=>joinedLobby == true );
			
			PhotonNetwork.CreateRoom( roomName, roomOptions, typedLobby );
			
			yield return new WaitUntil( ()=>joinedRoom == true );
			
			StreamSDKUIManager.instance.ShowContainer( contentContainer );
		}

		yield return new WaitForSeconds( 0 );
	}

	public void DoConnect() {
		if( Application.internetReachability != NetworkReachability.NotReachable && PhotonNetwork.connectionState == ConnectionState.Disconnected ) {
			if( overridePhotonServerSettings ) {
				PhotonNetwork.SwitchToProtocol( (ExitGames.Client.Photon.ConnectionProtocol)PlayerPrefs.GetInt( "StreamSDKPhotonProtocol" ) );
				PhotonNetwork.OverrideBestCloudServer( (CloudRegionCode)PlayerPrefs.GetInt( "StreamSDKPhotonRegion" ) );
				PhotonNetwork.ConnectToRegion( (CloudRegionCode)(PlayerPrefs.GetInt( "StreamSDKPhotonRegion" ) ), "1.0" );
			} else {
				PhotonNetwork.ConnectUsingSettings( "1.0" );
			}
		}
	}

	public void DoDisconnect() {
		if( PhotonNetwork.connectionState != ConnectionState.Disconnected && PhotonNetwork.connectionState != ConnectionState.Disconnecting )
			PhotonNetwork.Disconnect();
	}
	
	[PunRPC]
	void NewPlayerConnected( PhotonPlayer player ) {
		if( player == null )
			return;
		if( PhotonNetwork.player == null )
			return;
		
		if( player.ID == PhotonNetwork.player.ID )
			return;

		StreamSDK.AddClient( player.ID );
	}

	void OnMasterClientSwitched( PhotonPlayer player ) {
		StartCoroutine( Restart() );
	}
	
	void OnJoinedLobby() {
		joinedLobby = true;	
	}

	void OnPhotonPlayerConnected( PhotonPlayer player ) {
		StreamSDK.AddClient( player.ID );
		//Server back to client (server should always be the first in the array of SendData)
		photonView.RPC( "NewPlayerConnected", player, PhotonNetwork.player );
		//Message others of this client
		photonView.RPC( "NewPlayerConnected", PhotonTargets.OthersBuffered, player );
	}

	void OnPhotonPlayerDisconnected( PhotonPlayer player ) {
		StreamSDK.RemoveClient( player.ID );
    }

	void OnDisconnectedFromPhoton() {
		StreamSDK.Stop();
	}

	void OnJoinedRoom() {
		joinedRoom = true;
		StreamSDK.instance.id = PhotonNetwork.player.ID;
		StreamSDK.instance.ids.Clear();
		if( test )
			StreamSDK.AddClient( PhotonNetwork.player.ID );
		StreamSDK.InitMic( StreamSDKTransporter.instance.numberReceivers );
		StreamSDK.InitAudio( StreamSDKTransporter.instance.numberReceivers );
	}

	[PunRPC]
	public void Quit() {
		StartCoroutine( Restart() );
	}

	void DebugPackets() {
		packetCount++;
		if( packetCount == 1 )
			packetStartTime = Time.time;
		Debug.Log( "Packets per Second = " + packetCount / ( Time.time - packetStartTime ) );
		Debug.Log( "Packet Count = " + packetCount );
		Debug.Log( "Seconds = " + ( Time.time - packetStartTime ) );
	}

	[PunRPC]
	public void ReceiveStream( byte[] data ) {
		//DebugPackets();
		StreamSDK.UpdateStreamRemote( data );
	}

	[PunRPC]
	public void ReceiveAudio( byte[] data ) {
		//DebugPackets();
		StreamSDK.UpdateStreamRemoteAudio( data );
	}

	[PunRPC]
	public void ReceiveVideo( byte[] data ) {
		//DebugPackets();
		StreamSDK.UpdateStreamRemoteVideo( data );
	}

	[PunRPC]
	public void ReceiveInput( byte[] data ) {
		StreamSDK.UpdateInputRemote( data );		
	}

	[PunRPC]
	public void ReceiveMouse( byte[] data ) {
		//#if !UNITY_WSA
		StreamSDK.UpdateMouseRemote( data );
		//#endif
	}

	public void OnPhotonJoinRoomFailed() {
		DoDisconnect();
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}

	public void SendStream( byte[] data, int receiver ) {
		if( !initialized || !PhotonNetwork.connectedAndReady )
			return;

		//if( photonView.isMine )
			//PhotonNetwork.RemoveRPCs( photonView );

		if( PhotonNetwork.offlineMode && test ) {
			photonView.RPC( "ReceiveStream", PhotonTargets.All, data );
			return;
		}
			
		if( StreamSDK.instance.ids.Count <= receiver )
			return;
		
		if( PhotonNetwork.connectionState != ConnectionState.Connected || PhotonNetwork.room == null )
			return;	
		
		if( receiver == -1 ) {
			photonView.RPC( "ReceiveStream", PhotonTargets.Others, data );
			return;
		}

		if( PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ) == null )
			return;
		
		photonView.RPC( "ReceiveStream", PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ), data );
	}

	public void SendAudio( byte[] data, int receiver ) {
		if( !initialized || !PhotonNetwork.connectedAndReady )
			return;

		//if( photonView.isMine )
			//PhotonNetwork.RemoveRPCs( photonView );

		if( PhotonNetwork.offlineMode && test ) {
			photonView.RPC( "ReceiveAudio", PhotonTargets.All, data );
			return;
		}
			
		if( StreamSDK.instance.ids.Count <= receiver )
			return;
			
		if( StreamSDK.instance.ids.Count <= receiver )
			return;

		if( PhotonNetwork.connectionState != ConnectionState.Connected || PhotonNetwork.room == null )
			return;				
			
		if( receiver == -1 ) {
			photonView.RPC( "ReceiveAudio", PhotonTargets.Others, data );
			return;
		}
			
		if( PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ) == null )
			return;

		photonView.RPC( "ReceiveAudio", PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ), data );	
	}

	public void SendVideo( byte[] data, int receiver ) {
		if( !initialized || !PhotonNetwork.connectedAndReady )
			return;

		//if( photonView.isMine )
			//PhotonNetwork.RemoveRPCs( photonView );

		if( PhotonNetwork.offlineMode && test ) {
			photonView.RPC( "ReceiveVideo", PhotonTargets.All, data );
			return;
		}
			
		if( StreamSDK.instance.ids.Count <= receiver )
			return;

		if( PhotonNetwork.connectionState != ConnectionState.Connected || PhotonNetwork.room == null )
			return;
			
		if( receiver == -1 ) {
			photonView.RPC( "ReceiveVideo", PhotonTargets.Others, data );
			return;
		}

		if( PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ) == null )
			return;
		
		photonView.RPC( "ReceiveVideo", PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ), data );
	}

	public void SendInput( byte[] data, int receiver ) {
		if( !initialized || !PhotonNetwork.connectedAndReady )
			return;

		//if( photonView.isMine )
			//PhotonNetwork.RemoveRPCs( photonView );

		if( PhotonNetwork.offlineMode && test ) {
			photonView.RPC( "ReceiveInput", PhotonTargets.All, data );
			return;
		}

		if( StreamSDK.instance.ids.Count <= receiver )
			return;

		if( PhotonNetwork.connectionState != ConnectionState.Connected || PhotonNetwork.room == null )
			return;	

		if( receiver == -1 ) {
			photonView.RPC( "ReceiveInput", PhotonTargets.Others, data );
			return;
		}

		if( PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ) == null )
			return;

		photonView.RPC( "ReceiveInput", PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ), data );
	}

	public void SendMouse( byte[] data, int receiver ) {
		if( !initialized || !PhotonNetwork.connectedAndReady )
			return;

		//if( photonView.isMine )
			//PhotonNetwork.RemoveRPCs( photonView );

		if( PhotonNetwork.offlineMode && test ) {
			photonView.RPC( "ReceiveMouse", PhotonTargets.All, data );
			return;
		}
		
		if( StreamSDK.instance.ids.Count <= receiver )
			return;

		if( PhotonNetwork.connectionState != ConnectionState.Connected || PhotonNetwork.room == null )
			return;	
		
		if( receiver == -1 ) {
			photonView.RPC( "ReceiveMouse", PhotonTargets.Others, data );
			return;
		}

		if( PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ) == null )
			return;

		photonView.RPC( "ReceiveMouse", PhotonPlayer.Find( StreamSDK.instance.ids[ receiver ] ), data );
	}

	// Use this for initialization
	void Start() {
		PhotonWrapper.instance = this;
		
		//streamType                 = StreamSDKTransporter.instance.streamType;
		test                         = StreamSDKTransporter.instance.test;
		sendRate                     = StreamSDKTransporter.instance.sendRate;
		numberReceivers              = StreamSDKTransporter.instance.numberReceivers;
		contentContainer             = StreamSDKTransporter.instance.contentContainer;
		overridePhotonServerSettings = StreamSDKTransporter.instance.overridePhotonServerSettings;
		reloadScene                  = StreamSDKTransporter.instance.reloadScene;

		PhotonNetwork.autoJoinLobby = true;

		if( PlayerPrefs.GetInt( "StreamSDKOptions" ) != 1 || !PlayerPrefs.HasKey( "StreamSDKOptions" ) ) {
			PlayerPrefs.SetInt( "StreamSDKPhotonProtocol", 1 );
			PlayerPrefs.SetInt( "StreamSDKPhotonRegion", 0 );
		}

		photonView = gameObject.AddComponent< PhotonView >();
		photonView.synchronization = ViewSynchronization.Off;
		photonView.ObservedComponents = new List<Component>();
		photonView.ObservedComponents.Add( this );
		photonView.viewID = 1;

		initialized = true;
	}
}
#endif