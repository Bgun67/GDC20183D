using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDC
{
    public class GameplayState : State
    {
        Vector2 mouseAbsolute;
        Vector2 smoothMouse;
        Vector2 targetCharacterDirection = Vector2.zero;

        //todo Moveto Settings options
        [SerializeField] bool lockCursor = true;
        [SerializeField] Vector2 clampInDegrees = new Vector2(360, 180);
        [SerializeField] Vector2 sensitivity = new Vector2(1, .7f);
        [SerializeField] Vector2 smoothing = new Vector2(2, 2);

        protected Vector3 vel = Vector3.zero;
        //camera 

        public GameplayState(Player player) : base(player)
        {
            
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            SmoothLook();
            Movement();
        }

        public override void Update()
        {
            base.Update();
            HandleInput();
        }

        void Movement()
        {
            int moveState = (int)_player.CurrMoveType;
            Vector3 keyIn = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            keyIn = Vector3.ClampMagnitude(keyIn, 1);
            keyIn *= _player.Speed[moveState];
            _rb.velocity = _player.transform.rotation * new Vector3(keyIn.x, _rb.velocity.y, keyIn.z);
        }

        void HandleInput()
        {
            if (Input.GetKey(KeyCode.C))
            {
                //primaryGun.Shoot();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                _player.ChangeCamera(CameraState.Thermal);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                _player.ChangeCamera(CameraState.UV);
            }
        }

        void SmoothLook()
        {
            // Ensure the cursor is always locked when set
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Allow the script to clamp based on a desired target value.
            //var targetOrientation = Quaternion.Euler(_targetDirection);
            var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

            // Get raw mouse input for a cleaner reading on more sensitive mice.
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            // Scale input against the sensitivity setting and multiply that against the smoothing value.
            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

            // Interpolate mouse movement over time to apply smoothing delta.
            smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
            smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

            // Find the absolute mouse movement value from point zero.
            mouseAbsolute += smoothMouse;

            // Clamp and apply the local x value first, so as not to be affected by world transforms.
            if (clampInDegrees.x < 360)
                mouseAbsolute.x = Mathf.Clamp(mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

            // Then clamp and apply the global y value.
            if (clampInDegrees.y < 360)
                mouseAbsolute.y = Mathf.Clamp(mouseAbsolute.y, -clampInDegrees.y * 0.45f, clampInDegrees.y * 0.55f);


            var yRotation = Quaternion.AngleAxis(mouseAbsolute.x, Vector3.up);
            _player.transform.localRotation = yRotation * targetCharacterOrientation;


            var xRotation = Quaternion.AngleAxis(mouseAbsolute.y, Vector3.left);
            _player.CameraRig.transform.localRotation = xRotation;
            _player.CameraRig.transform.localPosition = Vector3.SmoothDamp(_player.CameraRig.transform.localPosition, _player.cameraRotatePoint + xRotation * _player.cameraOffset, ref vel, .1f); ;// _player.cameraRotatePoint + xRotation * _player.cameraOffset;
        }
    }
}