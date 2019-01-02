using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace GDC
{
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
		public bool uvOn;
		public bool thermalOn;

		public Material[] UVMats;
		public Material[] thermalMats;


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
				SwitchToUV();
			}
			if (Input.GetKeyDown(KeyCode.H))
			{
				SwitchToThermal();
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
		void SwitchToUV()
		{
			//print(shaderOBJ.GetComponent<MeshRenderer>().sharedMaterial.name);

			if (!uvOn)
			{
				foreach (Material UVMat in UVMats)
				{
					UVMat.SetFloat("Vector1_E4277239", 1);
				}
				GameObject.Find("Main Camera").GetComponent<PostProcessVolume>().enabled = true;
			}
			else
			{
				foreach (Material UVMat in UVMats)
				{
					UVMat.SetFloat("Vector1_E4277239", 0);
				}
				GameObject.Find("Main Camera").GetComponent<PostProcessVolume>().enabled = false;

			}
			uvOn = !uvOn;
		}
		void SwitchToThermal()
		{

			if (!thermalOn)
			{
				foreach (Material thermalMat in thermalMats)
				{
					thermalMat.SetFloat("Vector1_B5C75C03", 1);
					thermalMat.SetInt("_ZWrite", 0);
					thermalMat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
				//"Queue" = "Transparent"

            		thermalMat.SetInt("Queue", (int)UnityEngine.Rendering.RenderQueue.Transparent);

            		thermalMat.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.Always);
       					print(thermalMat.GetInt("_ZWrite"));
					print(thermalMat.GetInt("_ZTest"));
					print(thermalMat.GetInt("_Cull"));

				}
				GameObject.Find("Main Camera").GetComponent<PostProcessVolume>().enabled = true;
			}
			else
			{
				foreach (Material thermalMat in thermalMats)
				{
					thermalMat.SetFloat("Vector1_B5C75C03", 0);
				}
				GameObject.Find("Main Camera").GetComponent<PostProcessVolume>().enabled = false;

			}

			thermalOn = !thermalOn;
		}
	}
}

