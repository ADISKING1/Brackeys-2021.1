using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public delegate void LC();
    public static LC LevelComplete;
}
