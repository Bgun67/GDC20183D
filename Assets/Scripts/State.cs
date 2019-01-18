using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GDC
{
    public class State
    {
        protected Player _player;
        protected Rigidbody _rb;

        public State(Player player)
        {
            _player = player;
            _rb = player.GetComponent<Rigidbody>();
        }
        
        public virtual void FixedUpdate()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Pause()
        {

        }

        public virtual void Resume()
        {

        }
    }
}

