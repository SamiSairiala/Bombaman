using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public interface IMove
    {
        void Move(Vector2 direction); // Handles direction of the movement

        float Speed { get; } // Speed of the mover

        void Setup(float speed);
    }
}
