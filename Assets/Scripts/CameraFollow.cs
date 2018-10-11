using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class CameraFollow : MonoBehaviour
    {

        Player _player;
        [SerializeField] Vector3 _offset = new Vector3(0, 1.5f, -4);
        List<Collider> _collidingObj = new List<Collider>();
        private int _layerMask = ~(1 << 10);

        // Use this for initialization
        void Start()
        {
            _player = transform.parent.gameObject.GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            transform.localPosition = transform.localRotation * _offset;



            RaycastHit wallHit = new RaycastHit();//linecast from your player to camera to find collisions.

            //Todo: use normals of collided object to move camera
            //Todo: smooth camera movement 
            //Todo: smooth out camera bump 


            if (Physics.Linecast(transform.position, _player.transform.position, out wallHit, _layerMask))
            {


                Debug.Log("Moveing Camera");
                Vector3 s = -wallHit.normal * .25f;
                Vector3 targetCamHitPos = wallHit.point + s;

                transform.position = wallHit.point + s;


                Debug.DrawLine(transform.position, _player.transform.position, Color.red);
            }
            else
            {
                Debug.DrawLine(transform.position, _player.transform.position, Color.green);
            }

            Debug.Log(_collidingObj.Count);

            foreach(Collider col in _collidingObj)
            {
                Debug.DrawLine(col.ClosestPointOnBounds(transform.position), _player.transform.position, Color.red);
                
            }

        }
        
        private void OnTriggerEnter(Collider collider)
        {
            _collidingObj.Add(collider);
            
        }
        
        private void OnTriggerExit(Collider collider)
        {
            _collidingObj.Remove(collider);
        }
    }

}
