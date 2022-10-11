using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

namespace Bombaman
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput playerInput;
        private Character mover;
        private IMove imover;
        [SerializeField] private float Speed;
        

        //private void Awake()
        //{
        //    playerInput = GetComponent<PlayerInput>();
        //    var movers = FindObjectsOfType<Character>();
        //    var index = playerInput.playerIndex;
        //    mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        //    imover.Setup(Speed);
        //}

        //public void OnMove(CallbackContext context)
        //{
        //    mover.Move(context);
                
        //}
    }
}
