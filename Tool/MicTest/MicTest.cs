using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicTest : MonoBehaviour
{
    // Start recording with built-in Microphone and play the recorded audio right away
    void Start()
    {
		#if !UNITY_WEBGL
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start("", true, 10, 44100);
        audioSource.Play();
		#endif
    }
}
