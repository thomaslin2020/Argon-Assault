using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    [Header("Speed")] [Tooltip("In ms^-1")] [SerializeField]
    private float xSpeed = 20f;

    [Tooltip("In ms^-1")] [SerializeField] private float ySpeed = 20f;
    [SerializeField] private float positionPitchFactor = -5f;
    [SerializeField] private float controlPitchFactor = -20f;
    [SerializeField] private float positionYawFactor = 5f;
    [SerializeField] private float controlRollFactor = -20f;

    [Header("Range")] [Tooltip("In m")] [SerializeField]
    private float xRange = 4f;

    [Tooltip("In m")] [SerializeField] private float yRangeMin = -3f;
    [Tooltip("In m")] [SerializeField] private float yRangeMax = 3f;

    private float _xThrow;
    private float _yThrow;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation() {
        float pitch = transform.localPosition.y * positionPitchFactor + _yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = _xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation() {
        _xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        _yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = _xThrow * xSpeed * Time.deltaTime;
        float yOffset = _yThrow * ySpeed * Time.deltaTime;

        var position = transform.localPosition;

        float rawXPos = position.x + xOffset;
        float rawYPos = position.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, yRangeMin, yRangeMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, position.z);
    }
}