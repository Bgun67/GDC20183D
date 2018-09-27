using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class Player : MonoBehaviour
    {
        //var decleration
        protected StateMachine _stateMachine;

        [SerializeField] float _RunSpeed;
        [SerializeField] float _SprintSpeed;
        [SerializeField] float _WalkSpeed;

        //Public get and sets
        public float SprintSpeed
        {
            get
            {
                return _SprintSpeed;
            }

            set
            {
                _SprintSpeed = value;
            }
        }
        public float WalkSpeed
        {
            get
            {
                return _WalkSpeed;
            }

            set
            {
                _WalkSpeed = value;
            }
        }
        public float RunSpeed
        {
            get
            {
                return _RunSpeed;
            }

            set
            {
                _RunSpeed = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            _stateMachine = new StateMachine(this);                 
            _stateMachine.AddState(new MovingState(this), false);  //add starting state
        }

        // Update is called once per frame
        void Update()
        {

        }

        //50hz update loop
        private void FixedUpdate()
        {
            _stateMachine.ProcessStateChanges();
            _stateMachine.ProcessState();
        }
    }
}

