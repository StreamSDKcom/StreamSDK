using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using StreamSDKSpace;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class StreamSDKCarController : MonoBehaviour
    {
		public int playerIndex = 0;

        private CarController m_Car; // the car controller we want to use

		private Vector3 originalPosition;

		private void Awake()
		{
		// get the car controller
		m_Car = GetComponent<CarController>();

		originalPosition = transform.position;
		}


		private void FixedUpdate()
		{
			//if( Input.GetKeyDown( KeyCode.R ) ) {
			if( StreamSDK.GetAxisDown( "Reset", playerIndex ) ) {
				transform.rotation = Quaternion.identity;
				transform.position = originalPosition;
			}

			// pass the input to the car!
			float h = StreamSDK.GetAxis( "Horizontal", playerIndex );//Input.GetAxis("Horizontal");
			float v = StreamSDK.GetAxis( "Vertical", playerIndex );//Input.GetAxis("Vertical");
			#if !MOBILE_INPUT
        		  float handbrake = StreamSDK.GetAxis( "Jump", playerIndex );//CrossPlatformInputManager.GetAxis("Jump");
        		  m_Car.Move(h, v, v, handbrake);
			#else
			  m_Car.Move(h, v, v, 0f);
			#endif
			}
		}
}
