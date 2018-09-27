using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GDC
{
    public class State
    {
        protected Player _player;

        public State(Player player)
        {
            _player = player;
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

