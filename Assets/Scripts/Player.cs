using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class Player : MonoBehaviour
    {
        protected StateMachine _stateMachine;

        // Use this for initialization
        void Start()
        {
            _stateMachine = new StateMachine(this);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            _stateMachine.ProcessStateChanges();
            _stateMachine.ProcessState();
        }
    }
}

