using System.Collections;
using Rewired;
using UnityEngine;

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
    [SerializeField] private int _rotationSpeed;
    [Space(5)]
    [SerializeField] private bool _goFront;
    [SerializeField] private bool _goBack;
    [SerializeField] private bool _turnLeft;
    [SerializeField] private bool _turnRight;

    [Header("Power Up"), Space(5)]
    public Item currentItemScript;
    
    [Header("Rewired"), Space(5)]
    public Player _player;
    [SerializeField] private int _playerId;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _player = ReInput.players.GetPlayer(_playerId);
    }
    
    private void Update()
    {
        //ClampRotationX();

        // *************************************************************************************************************
        // INPUT MOVEMENT **********************************************************************************************
        // *************************************************************************************************************
        
        if (_player.GetButtonDown("GoFront"))
        {
            _goFront = true;
        }
        else if (_player.GetButtonUp("GoFront"))
        {
            _goFront = false;
            StartCoroutine(DecreaseSpeed());
        }
        
        if (_player.GetButtonDown("GoBack"))
        {
            _goBack = true;
        }
        else if (_player.GetButtonUp("GoBack"))
        {
            _goBack = false;
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

        // *************************************************************************************************************
        // INPUT ITEM **************************************************************************************************
        // *************************************************************************************************************

        if (_player.GetButtonDown("UsePowerUp"))
        {
            currentItemScript.UseItem(currentItemScript._itemPowerUpTypeTarget);
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
    }

    private void ClampRotationX()
    {
        var xAngle = Mathf.Clamp(transform.eulerAngles.x, -60, 60);
        var yAngle = transform.eulerAngles.y;
        var zAngle = transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
    }

    private IEnumerator DecreaseSpeed()
    {
        while (_currentSpeed > _neutralSpeed)
        {
            yield return new WaitForSeconds(0.035f);
            _currentSpeed = Mathf.Max(_currentSpeed - _acceleration, _neutralSpeed);
            _cartRigidbody.MovePosition(transform.position + transform.forward * _currentSpeed * Time.deltaTime);
            

            if (_player.GetButton("GoFront"))
            {
                yield break;
            }
        }

        while (_currentSpeed < _neutralSpeed)
        {
            _currentSpeed = Mathf.Min(_currentSpeed + _acceleration, _neutralSpeed);
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
    
    public void ActiveSpeedBoostByItem()
    {
        StartCoroutine(SpeedBoostByItem(currentItemScript.itemBoostAmount));
    }
    
    public IEnumerator SpeedBoostByPad()
    {
        _currentSpeed = _maxSpeed * 3;

        _currentSpeed = Mathf.Max(_currentSpeed, _maxSpeed); 

        //yield return new WaitForSeconds(0.75f); // Durée du boost

        // Réduction progressive du boost pour un effet fluide
        while (_currentSpeed > _maxSpeed)
        {
            _currentSpeed -= _acceleration; 
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    public IEnumerator SpeedBoostByItem(int amount)
    {
        _currentSpeed = _maxSpeed * amount;

        _currentSpeed = Mathf.Max(_currentSpeed, _maxSpeed); 

        //yield return new WaitForSeconds(0.75f); // Durée du boost

        // Réduction progressive du boost pour un effet fluide
        while (_currentSpeed > _maxSpeed)
        {
            _currentSpeed -= _acceleration; 
            yield return new WaitForSeconds(0.1f);
        }
    }

}
