using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
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
		if( Input.GetKeyDown( KeyCode.R ) ) {
			transform.rotation = Quaternion.identity;
			transform.position = originalPosition;
		}

		// pass the input to the car!
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		#if !MOBILE_INPUT
        	  float handbrake = CrossPlatformInputManager.GetAxis("Jump");
        	  m_Car.Move(h, v, v, handbrake);
		#else
		  m_Car.Move(h, v, v, 0f);
		#endif
		}
	}
}
