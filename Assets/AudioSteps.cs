using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class AudioSteps : MonoBehaviour
    {
        private AudioSource AudioSource;

        [SerializeField] private AudioClip[] StepClips;

        private bool IsPlaying = false;

        private float cooldown = 0.4f;
        // Start is called before the first frame update
        void Start()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void PlaySteps()
		{
            int randomint = 0;
            int random = Random.Range(0, StepClips.Length);

            if(random == 0 && IsPlaying == false)
			{
                Invoke("EnableNext", 0.4f);
                AudioSource.PlayOneShot(StepClips[0]);
                IsPlaying = true;
                
            }
            if (random == 1 && IsPlaying == false)
            {
                Invoke("EnableNext", 0.4f);
                AudioSource.PlayOneShot(StepClips[1]);
                IsPlaying = true;
                
            }
            if (random == 2 && IsPlaying == false)
            {
                Invoke("EnableNext", 0.4f);
                AudioSource.PlayOneShot(StepClips[2]);
                IsPlaying = true;
               
            }
            if (random == 3 && IsPlaying == false)
            {
                Invoke("EnableNext", 0.4f);
                AudioSource.PlayOneShot(StepClips[3]);
                IsPlaying = true;
              
            }
        }

        void EnableNext()
		{
            IsPlaying = false;
		}
    }
}
