using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class MoveingState : State
    {
        Rigidbody rb;

        public MoveingState(Player player) : base(player)
        {
            //Finds the rigibody on the player
            rb = _player.GetComponent<Rigidbody>();
        }

        public override void Update()
        {
            base.Update();
            //todo add movement code
        }

    }
}
