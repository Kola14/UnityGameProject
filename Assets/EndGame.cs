﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
