using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GUIRayClick : MonoBehaviour {

	public void OnClick() {
		if( GetComponent<Button>() != null )
			GetComponent<Button>().onClick.Invoke();
	}
 }
