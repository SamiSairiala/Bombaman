using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class AnimatedSpriteRenderer : MonoBehaviour
    {
        [SerializeField] private float animationTime = 0.25f;

        private int animationFrame;

        public Sprite idleSprite;
        [SerializeField] private Sprite[] animationSprites;

        public bool loop = true;
        public bool idle = true;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            spriteRenderer.enabled = true;
        }


        private void OnDisable()
        {
            spriteRenderer.enabled = false;
        }

        private void Start()
        {
            InvokeRepeating(nameof(NextFrame), 0, animationTime);
        }

        private void NextFrame()
        {
            animationFrame++;

            if(loop && animationFrame >= animationSprites.Length)
            {
                animationFrame = 0;
            }

            if (idle)
            {
                spriteRenderer.sprite = idleSprite;
            }
            else if(animationFrame >= 0 && animationFrame > animationSprites.Length)
            {
                spriteRenderer.sprite = animationSprites[animationFrame];
            }
        }
    }
}
