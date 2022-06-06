using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwerveInputSystem))]
public class SwerveMovement : MonoBehaviour
{
    public static SwerveMovement swerve;

    private SwerveInputSystem _swerveInputSystem;
    public float zSpeed;
    public float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    
    private void Awake()
    {
        swerve = this;
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    private void Update()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount, 0, zSpeed);

    }
}