using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotate : MonoBehaviour
{
	
	public float speed;
	public Vector3 axes;
	public Space space;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( axes * speed * Time.deltaTime, space );
    }
}
