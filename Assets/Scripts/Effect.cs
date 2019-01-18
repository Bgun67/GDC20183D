using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class Effect
    {
        protected Player _player;
        protected StateMachine _stateMachine;
        protected Rigidbody2D _rb;

        public Effect()
        {

        }

        public virtual void Init(Player player, StateMachine stateMachine)
        {
            _player = player;
            _stateMachine = stateMachine;
            _rb = _player.GetComponent<Rigidbody2D>();
        }

        public virtual void FixedUpdate()
        {

        }

        public virtual void Update()
        {

        }
    }
}

