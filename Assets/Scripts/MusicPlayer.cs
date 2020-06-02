using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Invoke(nameof(LoadFirstScene), 2f);
    }

    private void LoadFirstScene() {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update() {
    }
}
