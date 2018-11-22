using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class CameraFollow : MonoBehaviour
    {

        Player _player;
        [SerializeField] Vector3 _offset = new Vector3(0, 1.5f, -4);
        [SerializeField] float _objectAvoidRange;
        [Range(0,1)]
        [SerializeField] float _objectAvoidPower;
        [Range(0, 1)]
        [SerializeField] float _cameraSmooth;
    
        int _layerMask = ~(1 << 10);

        Vector2 _mouseAbsolute;
        Vector2 _smoothMouse;
        Vector3 velocity = Vector3.zero;

        [SerializeField] Vector2 _sensitivity = new Vector2(1, .7f); //move to settings
        [SerializeField] Vector2 _smoothing = new Vector2(2, 2);
        Vector3 lastPos;// = Vector3.zero;
        Quaternion lastRot;// = Quaternion.identity;

        Rigidbody rb;

        public Transform _lookAtTarget;

        // Use this for initialization
        void Start()
        {
            _player = transform.parent.gameObject.GetComponent<Player>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void FixedUpdate()
        {
            //TODO: add cutscene suport
            GameplayCamera();
        }

        //standard follow cam
        private void GameplayCamera()
        {
            //repeat code TODO: fix
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            // Scale input against the sensitivity setting and multiply that against the smoothing value.
            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(_sensitivity.x * _smoothing.x, _sensitivity.y * _smoothing.y));

            // Interpolate mouse movement over time to apply smoothing delta.
            _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / _smoothing.x);
            _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / _smoothing.y);

            // Find the absolute mouse movement value from point zero.
            _mouseAbsolute += _smoothMouse;
            transform.localRotation = (Quaternion.AngleAxis(-_mouseAbsolute.y, Vector3.right));
            transform.localPosition = transform.localRotation * _offset;

            RaycastHit wallHit;//linecast from your player to camera to find collisions.
            Vector3 targetPos = lastPos;

            
            if (Physics.Linecast(_player.transform.position, transform.position - (_player.transform.position - transform.position).normalized, out wallHit, _layerMask))
            {
                Vector3 s = wallHit.normal * .4f;
                transform.position = wallHit.point + s;
                //Debug.DrawLine(_player.transform.position, wallHit.point, Color.red);
            }
            else
            {
                //Debug.DrawLine(_player.transform.position, transform.position - (_player.transform.position - transform.position).normalized, Color.green);
            }
            

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _objectAvoidRange);
            float weight = 0;
            Vector3 vcr = Vector3.zero;
            

            for (int x = 0; x < hitColliders.Length; x++)
            {
                if (!hitColliders[x].transform.Equals(_player.transform))
                {
                    if (wallHit.collider != null)
                    {
                        if (!wallHit.collider.transform.Equals(hitColliders[x].transform))
                        {
                            Vector3 close = _player.transform.InverseTransformPoint(hitColliders[x].ClosestPointOnBounds(transform.position));
                            weight = Mathf.Clamp(((close - transform.localPosition)).sqrMagnitude / (_objectAvoidRange), 0, 1) * -1 + 1;
                            vcr = -(close - transform.localPosition).normalized * _objectAvoidPower;
                            targetPos += vcr * weight;
                        }
                    }
                    else
                    {
                        Vector3 close = _player.transform.InverseTransformPoint(hitColliders[x].ClosestPointOnBounds(transform.position));
                        weight = Mathf.Clamp(((close - transform.localPosition)).sqrMagnitude / (_objectAvoidRange), 0, 1) * -1 + 1;
                        vcr = -(close - transform.localPosition).normalized * _objectAvoidPower;
                        targetPos += vcr * weight;
                    }
                }
            }

            lastPos = Vector3.SmoothDamp(targetPos, transform.localPosition, ref velocity, _cameraSmooth);
            
            rb.transform.localPosition = lastPos;
            //TODO: Smooth Look Point
            if (_lookAtTarget)
            {
                transform.LookAt(_lookAtTarget);
            }
        }
    }
}
