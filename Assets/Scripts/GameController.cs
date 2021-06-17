using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController instance;
    public static GameController Instance { get { return instance; } }

    public Player player;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
