using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Bombaman
{
    public class VolumeControl : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;

       public void SetVolume(float volume)
		{
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
		}
        
    }
}
