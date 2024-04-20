using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private FixedJoystick _fixedJoysticks;
    [SerializeField] private float _speed;

    void FixedUpdate()
    {
        Vector3 direction = _mainCamera.transform.forward * _fixedJoysticks.Vertical + _mainCamera.transform.right * _fixedJoysticks.Horizontal;
        _rb.AddForce(direction * _speed);
    }
}