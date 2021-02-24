using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Point : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 PointID;
    public bool Runtime = true;
    public List<Bar> ConnectedBars;
    private void Start()
    {
        if(Runtime == false)
        {
            rb.bodyType = RigidbodyType2D.Static;
            PointID = transform.position;
            if(GameManager.AllPoints.ContainsKey(PointID) == false)
            {
                GameManager.AllPoints.Add(PointID, this);
            }
        }
    }
    private void Update()
    {
        if(Runtime == false)
        {
            if(transform.hasChanged == true)
            {
                transform.hasChanged = false;
                transform.position = Vector3Int.RoundToInt(transform.position);
            }
        }
    }
}
