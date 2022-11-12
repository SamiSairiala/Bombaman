using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bombaman
{
    public class Explosion : MonoBehaviour
    {
        public int Damage = 10;
        public AnimatedSpriteRenderer start;
        public AnimatedSpriteRenderer middle;
        public AnimatedSpriteRenderer end;

        public void DestroyAfter(float seconds)
        {
            Destroy(gameObject, seconds);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IHealth target = collision.GetComponent<IHealth>();
            if (target != null)
            {

           
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (!target.DecreseHealth(Damage))
            {
                damageable.Death();
                
                
            }
            }

        }

        public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
		{
            start.enabled = renderer == start;
            end.enabled = renderer == end;
		}

        public void SetDirection(Vector2 direction)
		{
            float angle = Mathf.Atan2(direction.y, direction.x);
            transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
		}

    }
}
