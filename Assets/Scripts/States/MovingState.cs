using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class MovingState : State
    {
        //hidden _player, _rb
        GameObject _camera;

        Vector2 _mouseAbsolute;
        Vector2 _smoothMouse;
        private int _layerMask = ~(1 << 10);

        //todo Moveto Settings options
        [SerializeField] bool    _lockCursor = true;
        [SerializeField] Vector2 _clampInDegrees = new Vector2(360, 180);
        [SerializeField] Vector2 _sensitivity = new Vector2(1, .7f);
        [SerializeField] Vector2 _smoothing = new Vector2(2, 2);
        [SerializeField] Vector3 _offset = new Vector3(0, 1.5f, -4);
        Vector2 _targetDirection;
        Vector2 _targetCharacterDirection;



        public MovingState(Player player) : base(player)
        {
            _camera = _player.transform.Find("Main Camera").gameObject;
        }

        public override void Update()
        {
            base.Update();
            SmoothLook();

            //simple movement
            Vector3 keyIn = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _player.RunSpeed;
            _rb.velocity = _player.transform.rotation * new Vector3(keyIn.x, _rb.velocity.y, keyIn.z);
        }

        //Modded Smooth look Script from https://forum.unity.com/threads/a-free-simple-smooth-mouselook.73117/
        void SmoothLook()
        {
            // Ensure the cursor is always locked when set
            if (_lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Allow the script to clamp based on a desired target value.
            var targetOrientation = Quaternion.Euler(_targetDirection);
            var targetCharacterOrientation = Quaternion.Euler(_targetCharacterDirection);

            // Get raw mouse input for a cleaner reading on more sensitive mice.
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            // Scale input against the sensitivity setting and multiply that against the smoothing value.
            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(_sensitivity.x * _smoothing.x, _sensitivity.y * _smoothing.y));

            // Interpolate mouse movement over time to apply smoothing delta.
            _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / _smoothing.x);
            _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / _smoothing.y);

            // Find the absolute mouse movement value from point zero.
            _mouseAbsolute += _smoothMouse;

            // Clamp and apply the local x value first, so as not to be affected by world transforms.
            if (_clampInDegrees.x < 360)
                _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -_clampInDegrees.x * 0.5f, _clampInDegrees.x * 0.5f);

            // Then clamp and apply the global y value.
            if (_clampInDegrees.y < 360)
                _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -_clampInDegrees.y * 0.45f, _clampInDegrees.y * 0.55f);


            

            // Move camera
            _camera.transform.localRotation = (Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation );
            _camera.transform.localPosition = _camera.transform.localRotation * _offset;

            RaycastHit wallHit = new RaycastHit();
            //linecast from your player (targetFollow) to your cameras mask (camMask) to find collisions.
            if (Physics.Linecast(_camera.transform.position, _player.transform.position, out wallHit, _layerMask))
            {
                Debug.Log("Moveing Camera");
                _camera.transform.position = wallHit.point;
                Debug.DrawLine(_camera.transform.position, _player.transform.position, Color.red);
            }
            else
            {
                Debug.DrawLine(_camera.transform.position, _player.transform.position, Color.green);
            }

            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
            _player.transform.localRotation = yRotation * targetCharacterOrientation;
        }
    }
}