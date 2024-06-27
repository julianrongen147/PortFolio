using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSlam : MonoBehaviour
{
    [SerializeField] private Animator _Animator;

    [SerializeField] private float _Interval;
    [SerializeField] private float _IntervalPlusAnimationLength;
    [SerializeField] private float _IntervalPlus1Point25;

    private bool _hasSlammed;
    private float _timer;

    private void Start()
    {
        _hasSlammed = false;
        _Animator = GetComponent<Animator>();
        _Animator.SetBool("Slam", false);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        // Check if the interval has passed
        if (_timer > _Interval)
        {
            _Animator.SetBool("Slam", true);
        }

        // Check if the interval plus animation length has passed
        if (_timer > _IntervalPlusAnimationLength)
        {
            _hasSlammed = true;
        }
        // Check if the object has slammed
        if (_hasSlammed)
        {
            // Reset the animation parameters and variables
            _Animator.SetBool("Slam", false);
            _timer = 0;
            _hasSlammed = false;
        }
    }
}
