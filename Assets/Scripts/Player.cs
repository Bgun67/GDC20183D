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
        #region VarDec 
        protected StateMachine stateMachine;

        [SerializeField] GunBase primaryGun;
        [SerializeField] GunBase secondaryGun;

        [SerializeField] float health;
        [SerializeField] float lowHealthWarning;

        public GameObject CameraRig;
		public GameObject mainCamera;
		public GameObject UVCamera;
		public GameObject thermalCamera;

        MoveType    currMoveType = MoveType.run;
		CameraState currCamState = CameraState.Normal;

        //var set - long bois edition
        private float[] speed = {
            3f,     //stealth
            5f,     //walk
            7f,     //run
            10f     //sprint
        };

        public enum MoveType
        {
            stealth,
            walk,
            run,
            sprint
        }

        //Public get and sets
        public float Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }
        public float LowHealthWarning
        {
            get
            {
                return lowHealthWarning;
            }

            set
            {
                lowHealthWarning = value;
            }
        }

        public GunBase PrimaryGun
        {
            get
            {
                return primaryGun;
            }

            set
            {
                primaryGun = value;
            }
        }
        public GunBase SecondaryGun
        {
            get
            {
                return secondaryGun;
            }

            set
            {
                secondaryGun = value;
            }
        }

        public MoveType CurrMoveType
        {
            get
            {
                return currMoveType;
            }

            set
            {
                currMoveType = value;
            }
        }

        [SerializeField]
        public float[] Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }
        #endregion
        
        void Start()
        {
            stateMachine = new StateMachine(this);                 
            stateMachine.AddState(new GameplayState(this), false);  //add starting state
        }
        
        void Update()
        {
            stateMachine.ProcessStateChanges();
            stateMachine.ProcessStateFast();
        }
        
        void FixedUpdate()
        {
            stateMachine.ProcessStateChanges();
            stateMachine.ProcessState();
        }

		public void ChangeCamera(CameraState state)
		{
			DisableCameras();
            currCamState = state;
			switch (state)
			{
				case CameraState.Thermal:
                    thermalCamera.SetActive(true);
					break;
				case CameraState.UV:
					UVCamera.SetActive(true);
					break;
                case CameraState.Normal:
                    mainCamera.SetActive(true);
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

