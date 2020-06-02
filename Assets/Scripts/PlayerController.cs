using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    [Header("General")] [Tooltip("In ms^-1")] [SerializeField]
    float speed = 20f;

    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [Header("Screen-position Based")] [SerializeField]
    float positionPitchFactor = -5f;

    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-throw Based")] [SerializeField]
    float controlPitchFactor = -20f;

    [SerializeField] float controlRollFactor = -20f;
    float _xThrow, _yThrow;
    private bool _isControlEnabled = true;

    // Use this for initialization
    void Start() {
    }

    void OnPlayerDeath() {
        _isControlEnabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (_isControlEnabled) {
            ProcessTranslation();
            ProcessRotation();
        }
    }

    private void ProcessRotation() {
        var localPosition = transform.localPosition;
        float pitchDueToPosition = localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = _yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = localPosition.x * positionYawFactor;

        float roll = _xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation() {
        _xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        _yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = _xThrow * speed * Time.deltaTime;
        float yOffset = _yThrow * speed * Time.deltaTime;

        var localPosition = transform.localPosition;
        float rawXPos = localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        localPosition = new Vector3(clampedXPos, clampedYPos, localPosition.z);
        transform.localPosition = localPosition;
    }
}