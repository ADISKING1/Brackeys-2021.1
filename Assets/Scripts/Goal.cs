using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Events.LevelComplete != null)
            Events.LevelComplete();
    }
}
