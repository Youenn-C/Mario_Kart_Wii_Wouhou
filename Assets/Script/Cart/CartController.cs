using System.Collections;
using Rewired;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CartController : MonoBehaviour
{
    public static CartController Instance;
    
    [Header("References"), Space(5)]
    [SerializeField] private Rigidbody _cartRigidbody;

    [Header("Variables"), Space(5)]
    [SerializeField] private int _currentSpeed;
    [SerializeField] private int _maxSpeed;
    [SerializeField] private int _neutralSpeed;
    [SerializeField] private int _minSpeed;
    [SerializeField] private int _acceleration;
    [Space(5)]
    [SerializeField] private int _rotationSpeed;
    
    [Header("Rewired"), Space(5)]
    [SerializeField] private Player _player;
    [SerializeField] private int _playerId;


    void Start()
    {
        _player = ReInput.players.GetPlayer(_playerId);
    }
    
    private void Update()
    {
        HandleRotation();
        //ClampRotationX();

        if (_player.GetButtonUp("GoFront") || _player.GetButtonUp("GoBack"))
        {
            StartCoroutine(DecreaseSpeed());
        }
    }

    private void FixedUpdate()
    {
        HandleMovementForward();
        HandleMovementBackward();
    }

    private void HandleRotation()
    {
        if (_player.GetButton("TurnLeft"))
        {
            transform.eulerAngles += Vector3.down * _rotationSpeed * Time.deltaTime;
        }
        
        if (_player.GetButton("TurnRight"))
        {
            transform.eulerAngles += Vector3.up * _rotationSpeed * Time.deltaTime;
        }
    }

    private void ClampRotationX()
    {
        var xAngle = Mathf.Clamp(transform.eulerAngles.x, -60, 60);
        var yAngle = transform.eulerAngles.y;
        var zAngle = transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
    }

    private void HandleMovementForward()
    {
        if (_player.GetButton("GoFront") && _currentSpeed < _maxSpeed)
        {
            _currentSpeed = Mathf.Min(_currentSpeed + _acceleration, _maxSpeed);
        }
    
        if (_currentSpeed > _neutralSpeed)
        {
            _cartRigidbody.MovePosition(transform.position + transform.forward * _currentSpeed * Time.deltaTime);
        }
    }

    private void HandleMovementBackward()
    {
        if (_player.GetButton("GoBack") && _currentSpeed < _maxSpeed && _currentSpeed > _minSpeed)
        {
            _currentSpeed = Mathf.Max(_currentSpeed - _acceleration, _minSpeed);
        }

        if (_currentSpeed < _neutralSpeed)
        {
            _cartRigidbody.MovePosition(transform.position + transform.forward * _currentSpeed * Time.deltaTime);
        }
    }

    private IEnumerator DecreaseSpeed()
    {
        while (_currentSpeed > _neutralSpeed)
        {
            _currentSpeed = Mathf.Max(_currentSpeed - _acceleration, _neutralSpeed);
            _cartRigidbody.MovePosition(transform.position + transform.forward * _currentSpeed);
            yield return new WaitForSeconds(0.05f);

            if (_player.GetButton("GoFront"))
            {
                yield break;
            }
        }

        while (_currentSpeed < _neutralSpeed)
        {
            _currentSpeed = Mathf.Min(_currentSpeed + _acceleration, _neutralSpeed);
            _cartRigidbody.MovePosition(transform.position + transform.forward * _currentSpeed);
            yield return new WaitForSeconds(0.05f);

            if (_player.GetButton("GoBack"))
            {
                yield break;
            }
        }
    }

    public void ActiveSpeedBoostByPad()
    {
        StartCoroutine(SpeedBoostByPad());
    }

    public IEnumerator SpeedBoostByPad()
    {
        _currentSpeed *= 2;

        float floatCurrentSpeed = _currentSpeed;
        float floatMaxSpeed = _maxSpeed;
        
        Mathf.Lerp(floatCurrentSpeed, floatMaxSpeed, Time.deltaTime);
        
        yield break;
    }
}
