using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        print(other.name);
        StartDeathSequence();
    }

    private void StartDeathSequence() {
        SendMessage("OnPlayerDeath");
    }
}