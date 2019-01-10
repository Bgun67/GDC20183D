using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace GDC
{
	public enum CameraState
	{
		Normal,
		UV,
		Thermal
	}
    public class Player : MonoBehaviour
    {
        //var decleration
        protected StateMachine _stateMachine;

        [SerializeField] GunBase _primaryGun;
        [SerializeField] GunBase _secondaryGun;

        [SerializeField] float _RunSpeed;
        [SerializeField] float _SprintSpeed;
        [SerializeField] float _WalkSpeed;
        [SerializeField] float _Health;
        [SerializeField] float _LowHealthWarning;
		public GameObject mainCamera;
		public GameObject UVCamera;
		public GameObject thermalCamera;
		CameraState currCamState = CameraState.Normal;

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
        public float Health
        {
            get
            {
                return _Health;
            }

            set
            {
                _Health = value;
            }
        }
        public float LowHealthWarning
        {
            get
            {
                return _LowHealthWarning;
            }

            set
            {
                _LowHealthWarning = value;
            }
        }

        public GunBase PrimaryGun
        {
            get
            {
                return _primaryGun;
            }

            set
            {
                _primaryGun = value;
            }
        }
        public GunBase SecondaryGun
        {
            get
            {
                return _secondaryGun;
            }

            set
            {
                _secondaryGun = value;
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
            if (Input.GetKey(KeyCode.C)) 
            {
                _primaryGun.Shoot();
            }
			if (Input.GetKeyDown(KeyCode.G))
			{
				ChangeCamera(CameraState.Thermal);
			}
			if (Input.GetKeyDown(KeyCode.H))
			{
				ChangeCamera(CameraState.UV);
			}
		}

        //50hz update loop
        private void FixedUpdate()
        {
            _stateMachine.ProcessStateChanges();
            _stateMachine.ProcessState();
        }

        void ProcessHealth()
        {
            if(_Health <= 0)
            {
                //add death state isreplaceing = true
            }
            else if(_Health < _LowHealthWarning)
            {
                //low health waring effect (add bool)
            }
        }
		//testing code - Needs to be thrown away
		void ChangeCamera(CameraState _state)
		{
			DisableCameras();
			switch (_state)
			{
				case CameraState.Thermal:
					if (currCamState != CameraState.Thermal)
					{
						currCamState = CameraState.Thermal;
						thermalCamera.SetActive(true);
					}
					else
					{
						mainCamera.SetActive(true);
						currCamState = CameraState.Normal;

					}
					break;
				case CameraState.UV:
					if (currCamState != CameraState.UV)
					{
						currCamState = CameraState.UV;
						UVCamera.SetActive(true);
					}
					else
					{
						mainCamera.SetActive(true);
						currCamState = CameraState.Normal;

					}
					break;

			}
		}
		void DisableCameras()
		{
			UVCamera.SetActive(false);
			thermalCamera.SetActive(false);
			mainCamera.SetActive(false);
		}
	}
}

