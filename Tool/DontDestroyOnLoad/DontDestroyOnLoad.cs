using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DontDestroyOnLoad : MonoBehaviour
{
	public static Dictionary<string, int> instances = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
		if( instances.ContainsKey( gameObject.name ) )
			Destroy( gameObject );
		else {
			instances.Add( gameObject.name, gameObject.GetInstanceID() );	
			DontDestroyOnLoad( gameObject );
		}
    }
}
