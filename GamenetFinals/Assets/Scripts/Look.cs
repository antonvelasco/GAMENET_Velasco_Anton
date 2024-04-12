
using UnityEngine;

public class Look : MonoBehaviour
{
    #region Variables
    private float _xRotation = 0f;
    private float _x;
    private float _y;

    public bool _isCursorEnabled = false;
    public Transform _player;
    #endregion

    #region Private Functions
    private void Update()
    {
        if (!_isCursorEnabled)
        {
            _x = Input.GetAxis("Mouse X") * 500f * Time.deltaTime;
            _y = Input.GetAxis("Mouse Y") * 500f * Time.deltaTime;
            _xRotation -= _y;
            _xRotation = Mathf.Clamp(_xRotation, -90, 90);
            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f).normalized;
            _player.Rotate(Vector3.up * _x);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    #endregion
}