﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        MenuController.Instance.OnTitleScreen();
        MenuController.Instance.transition.StartTransition(Color.clear, 1);
    }
}
