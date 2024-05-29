using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody _rb;

    [HideInInspector] public bool IsGround;
    [HideInInspector] public bool IsSloped;
    [HideInInspector] public bool IsBox;

    [SerializeField] private Transform _Cam;

    [SerializeField] private LayerMask _JumpableGround;
    [SerializeField] private LayerMask _JumpableSlope;
    [SerializeField] private LayerMask _GroundPound;

    [SerializeField] public float _MoveSpeed;
    [SerializeField] private float _MoveSpeedAir;
    [SerializeField] private float _MaxSpeed;
    [SerializeField] private float _JumpSpeed;
    [SerializeField] private float _JumpSpeedDown;
    [SerializeField] private float _JumpStartTime;

    [SerializeField] private AudioSource _jump;
    [SerializeField] private AudioClip _jumpEffect;

    private SpeedBoost _speedbooster;
    private JumpBoost _jumpBooster;

    private float _jumpTime;
    private bool _isJumping = false;

    private RaycastHit _hit;

    private float _xInput;
    private float _zInput;
    private float _timer;

    private bool Drag2ndAmount;

    private bool MusicFadeOutEnabled = false;

    //tutorial
    [SerializeField] private GameObject _TutorialObstacles;

    [SerializeField] private GameObject _Coin;
    [SerializeField] private GameObject _Box;

    [SerializeField] private GameObject _UItext1;
    [SerializeField] private GameObject _UItext2;
    [SerializeField] private GameObject _UItext3;
    [SerializeField] private GameObject _UItext4;
    [SerializeField] private GameObject _UItext5;
    [SerializeField] private GameObject _UItext6;

    private bool _startTutorial1 = true;
    private float _timer1;

    private bool _startTutorial2 = false;
    private float _timer2;

    private bool _startTutorial3 = false;
    private float _timer3;

    private bool _startTutorial4 = false;
    public bool _endTutorial4 = false;

    private bool _startTutorial5 = false;
    public bool _endTutorial5 = false;

    public bool _endTutorial = false;
    private float _TimerEnd;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _speedbooster = FindObjectOfType<SpeedBoost>();
        _jumpBooster = FindObjectOfType<JumpBoost>();
        Drag2ndAmount = false;
        _UItext1.gameObject.SetActive(false);
        _UItext2.gameObject.SetActive(false);
        _UItext3.gameObject.SetActive(false);
        _UItext4.gameObject.SetActive(false);
        _UItext5.gameObject.SetActive(false);
        _UItext6.gameObject.SetActive(false);
        _Coin.gameObject.SetActive(false);
        _Box.gameObject.SetActive(false);
        _TutorialObstacles.SetActive(false);

    }

    void Update()
    {
        Tutorial();

        if (IsGrounded())
        {
            IsGround = true;
        }
        else
        {
            IsGround = false;
        }
        if (IsSlope())
        {
            IsSloped = true;
        }
        else
        {
            IsSloped = false;
        }
        if (IsBoxed())
        {
            IsBox = true;
        }
        else
        {
            IsBox = false;
        }
    }

    private void Tutorial()
    {
        if (_startTutorial1 && !_endTutorial)
        {
            _UItext1.gameObject.SetActive(true);
            _timer1 += Time.deltaTime;
            Movement();
            Drag();
            if (_timer1 >= 5f)
            {
                _timer1 = 5f;
                _UItext1.gameObject.SetActive(false);
                _startTutorial2 = true;

                if (_startTutorial2)
                {
                    _timer2 += Time.deltaTime;
                    _UItext2.gameObject.SetActive(true);
                    if (_timer2 >= 5f)
                    {
                        _timer2 = 5f;
                        _UItext2.gameObject.SetActive(false);
                        _startTutorial3 = true;

                        if (_startTutorial3)
                        {
                            _timer3 += Time.deltaTime;
                            Jump();
                            _UItext3.gameObject.SetActive(true);
                            if (_timer3 >= 5f)
                            {
                                _timer3 = 5f;
                                _UItext3.gameObject.SetActive(false);
                                _startTutorial4 = true;

                                if (_startTutorial4)
                                {
                                    if (_Coin != null)
                                    {
                                        _Coin.gameObject.SetActive(true);
                                    }
                                    _UItext4.gameObject.SetActive(true);
                                    if (_endTutorial4)
                                    {
                                        _UItext4.gameObject.SetActive(false);
                                        _startTutorial5 = true;

                                        if (_startTutorial5)
                                        {
                                            if (_Box != null)
                                            {
                                                _Box.gameObject.SetActive(true);
                                            }
                                            _UItext5.gameObject.SetActive(true);
                                            if (_endTutorial5)
                                            {
                                                _UItext5.gameObject.SetActive(false);
                                                _endTutorial = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (_endTutorial)
        {
            _TimerEnd += Time.deltaTime;

            _TutorialObstacles.SetActive(true);
            Movement();
            Drag();
            Jump();

            if (_TimerEnd < 5f)
            {
                _UItext6.gameObject.SetActive(true);
            }

            if (_TimerEnd >= 5f)
            {
                _TimerEnd = 5f;
                _UItext6.gameObject.SetActive(false);
            }
        }
    }

    private void Movement()
    {

        _xInput = Input.GetAxis("Horizontal");
        _zInput = Input.GetAxis("Vertical");

        // Get the forward and right direction relative to the camera
        Vector3 camForward = _Cam.forward;
        Vector3 camRight = _Cam.right;

        camForward.y = 0;
        camRight.y = 0;

        // Calculate the forward and right movement relative to the camera
        Vector3 forwardRelative = _zInput * camForward;
        Vector3 rightRelative = _xInput * camRight;

        Vector3 moveDirection = forwardRelative + rightRelative;

        // if the player is on the ground or a slope
        if (IsGround || IsSloped || IsBox)
        {
            _rb.AddForce(new Vector3(moveDirection.x, 0, moveDirection.z).normalized * _MoveSpeed * Time.deltaTime);
            _MoveSpeed = Mathf.Min(_MoveSpeed, _MaxSpeed);
        }
        // if the player is in the air
        else if (!IsGround || !IsSloped)
        {
            _rb.AddForce(new Vector3(moveDirection.x, 0, moveDirection.z).normalized * _MoveSpeedAir * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (IsGround || IsSloped || IsBox)
        {
            if (Input.GetButtonDown("Jump"))
            {
                MusicFadeOutEnabled = false;
                if (!MusicFadeOutEnabled)
                {
                    _jump.volume = 1;
                    _jump.PlayOneShot(_jumpEffect);
                }

                _isJumping = true;
                _jumpTime = _JumpStartTime;
                Vector3 jump = new Vector3(0, 20, 0);
                _rb.AddForce(jump * _JumpSpeed * Time.deltaTime);
            }
        }
        if (Input.GetButton("Jump") && _isJumping)
        {
            if (_jumpTime > 0) // Apply a vertical force to the player as long as there's jump time remaining
            {
                Vector3 jump = new Vector3(0, 20, 0);
                _rb.AddForce(jump * _JumpSpeed * Time.deltaTime);
                _jumpTime -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            _isJumping = false;
            MusicFadeOutEnabled = true;
        }

        if (!_isJumping && !IsGround)
        {
            Vector3 jump = new Vector3(0, 20, 0);
            _rb.AddForce(jump * _JumpSpeedDown * Time.deltaTime);
        }

        if (MusicFadeOutEnabled)
        {
            if (_jump.volume <= 0.01f)
            {
                _jump.Stop();
                MusicFadeOutEnabled = false;
            }
            else
            {
                float newVolume = _jump.volume - (5f * Time.deltaTime);
                if (newVolume < 0f)
                {
                    newVolume = 0f;
                }
                _jump.volume = newVolume;
            }
        }
    }

    private void Drag()
    {
        if (IsGround)
        {
            if (_xInput == 0 && _zInput == 0 &&  // Check if the player is not providing any movement input, and if there are no speed or jump boosts active
                !_speedbooster.IsMoving && !_jumpBooster.IsJumping)
            {
                if (!Drag2ndAmount)
                {
                    _timer += Time.deltaTime;
                    if (_timer > 0.4f)
                    {
                        _rb.drag += 0.05f;
                        _timer = 0;
                    }
                }
            }
            if (_rb.drag > 0.5f)
            {
                Drag2ndAmount = true;  // Set Drag2ndAmount to true, indicating that the second stage of drag can now be applied
            }
            if (Drag2ndAmount)
            {
                _timer += Time.deltaTime;
                if (_timer > 0.2f)
                {
                    _rb.drag += 0.05f;
                    _timer = 0;
                }
            }
            if (_rb.drag > 1f)
            {
                _rb.drag = 1;
            }
        }

        // drag resets to 0;
        if (_xInput > 0 || _zInput > 0 || _xInput < 0 || _zInput < 0 ||
            Input.GetButtonDown("Jump") || _speedbooster.IsMoving || _jumpBooster.IsJumping)
        {
            _rb.drag = 0;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out _hit, 0.3f, _JumpableGround);
    }

    private bool IsSlope()
    {
        return Physics.Raycast(transform.position, Vector3.down, out _hit, 0.3f, _JumpableSlope);
    }

    private bool IsBoxed()
    {
        return Physics.Raycast(transform.position, Vector3.down, out _hit, 5f, _GroundPound);
    }
}
