using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class MovingState : State
    {
        //hidden _player, _rb

        public MovingState(Player player) : base(player)
        {
            
        }

        public override void Update()
        {
            base.Update();
            
            /**todo simple look
            Vector2 mouse = new Vector2(0, 0); // differance in mouse loc
            _player.transform.rotation = Quaternion.AngleAxis(mouse.x, Vector3.up);
            camera rotation x axis -> rotation from mouse y
            **/

            //simple movement
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _player.RunSpeed;
            _rb.velocity = new Vector3(input.x, _rb.velocity.y, input.z);
        }
    }
}