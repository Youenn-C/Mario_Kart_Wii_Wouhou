using System.Collections;
using Rewired;
using Unity.Collections;
using UnityEngine;

public class CartController : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Rigidbody _cartRigidbody;

    [Header("Variables"), Space(5)]
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _neutralSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _rotationSpeed;
    //private float _accelerationLerpInterpolator;
    //private float _terrainSpeedVariator;
    [Space(5)]
    [SerializeField] private bool _goFront;
    [SerializeField] private bool _goBack;
    [SerializeField] private bool _turnLeft;
    [SerializeField] private bool _turnRight;
    [SerializeField] private bool _isTurbo;
    //[SerializeField] private bool _isAccelerating;
    [Space(5)]
    [SerializeField] private AnimationCurve _accelerationCurve;
    [SerializeField] private AnimationCurve _decelerationCurve;
    
    [Header("Power Up"), Space(5)]
    [SerializeField] private int _maxSpeedTurbo;
    public Transform frontPosition;
    
    [Header("Rewired"), Space(5)]
    [SerializeField] private int _playerId;
    public Player _player;

    void Start()
    {
        _player = ReInput.players.GetPlayer(_playerId);
    }
    
    private void Update()
    {
        if (transform.eulerAngles.z != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
        
        // *************************************************************************************************************
        // INPUT MOVEMENT **********************************************************************************************
        // *************************************************************************************************************
        
        if (_player.GetButtonDown("GoFront"))
        {
            _goFront = true;
            //_isAccelerating = true;
        }
        else if (_player.GetButtonUp("GoFront"))
        {
            _goFront = false;
            //_isAccelerating = false;
            StartCoroutine(DecreaseSpeed());
        }
        
        if (_player.GetButtonDown("GoBack"))
        {
            _goBack = true;
            //_isAccelerating = true;
        }
        else if (_player.GetButtonUp("GoBack"))
        {
            _goBack = false;
            //_isAccelerating = false;
            StartCoroutine(DecreaseSpeed());
        }

        // *************************************************************************************************************
        // INPUT ROTATION **********************************************************************************************
        // *************************************************************************************************************
        
        if (_player.GetButtonDown("TurnLeft"))
        {
            _turnLeft = true;
        }
        else if (_player.GetButtonUp("TurnLeft"))
        {
            _turnLeft = false;
        }
        
        if (_player.GetButtonDown("TurnRight"))
        {
            _turnRight = true;
        }
        else if (_player.GetButtonUp("TurnRight"))
        {
            _turnRight = false;
        }
    }

    private void FixedUpdate()
    {
        // *************************************************************************************************************
        // MOVEMENT ****************************************************************************************************
        // *************************************************************************************************************
        
        if (_goFront && _currentSpeed < _maxSpeed)
        {
            _currentSpeed = Mathf.Min(_currentSpeed + _acceleration, _maxSpeed);
        }
        
        if (_goBack && _currentSpeed < _maxSpeed && _currentSpeed > _minSpeed)
        {
            _currentSpeed = Mathf.Max(_currentSpeed - _acceleration, _minSpeed);
        }

        if (_goFront || _goBack)
        {
            _cartRigidbody.MovePosition(transform.position + transform.forward * _currentSpeed * Time.deltaTime);
        }
        
        // *************************************************************************************************************
        // ROTATION ****************************************************************************************************
        // *************************************************************************************************************
        
        if (_turnLeft)
        {
            transform.eulerAngles += Vector3.down * _rotationSpeed * Time.deltaTime;
            
            if (_goBack)
            {
                transform.eulerAngles += Vector3.up * _rotationSpeed * Time.deltaTime;
            }
        }
        
        if (_turnRight)
        {
            transform.eulerAngles += Vector3.up * _rotationSpeed * Time.deltaTime;
                
            if (_goBack)
            {
                transform.eulerAngles += Vector3.down * _rotationSpeed * Time.deltaTime;
            }   
        }
        
        // *************************************************************************************************************
        // TURBO *******************************************************************************************************
        // *************************************************************************************************************
    
        if(_isTurbo)
        {
            _currentSpeed = _maxSpeedTurbo;
        }
    }
    
    private IEnumerator DecreaseSpeed()
    {
        while (_currentSpeed > _neutralSpeed)
        {
            yield return new WaitForSeconds(0.035f);
            _currentSpeed -= _acceleration;
            _cartRigidbody.MovePosition(transform.position + transform.forward * _currentSpeed * Time.deltaTime);
            

            if (_player.GetButton("GoFront"))
            {
                yield break;
            }
        }

        while (_currentSpeed < _neutralSpeed)
        {
            _currentSpeed += _acceleration;
            _cartRigidbody.MovePosition(transform.position + transform.forward * _currentSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);

            if (_player.GetButton("GoBack"))
            {
                yield break;
            }
        }
    }

    // *****************************************************************************************************************
    // ACTIVE BOOST ITEMS **********************************************************************************************
    // *****************************************************************************************************************
    
    public void ActiveSpeedBoostByPad()
    {
        StartCoroutine(SpeedBoostByPad());
    }
    
    public IEnumerator SpeedBoostByPad()
    {
        _currentSpeed = _maxSpeed * 3;

        _currentSpeed = Mathf.Max(_currentSpeed, _maxSpeed);

        // Réduction progressive du boost pour un effet fluide
        while (_currentSpeed > _maxSpeed)
        {
            _currentSpeed -= _acceleration; 
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public void Turbo()
    {
        if (!_isTurbo)
        {
            StartCoroutine(Turboroutine());
        }
    }

    private IEnumerator Turboroutine()
    {
        _isTurbo = true;
        
        yield return new WaitForSeconds(3);
        
        _isTurbo = false;
        
        while (_currentSpeed > _maxSpeed)
        {
            _currentSpeed -= _acceleration; 
            yield return new WaitForSeconds(0.1f);
        }
    }

}
