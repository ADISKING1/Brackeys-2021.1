using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public float Cost = 1;

    public float MaxLength = 1;
    public Vector2 startPosition;
    public SpriteRenderer BarSpriteRenderer;
    public BoxCollider2D BoxCollider;
    public HingeJoint2D StartJoint;
    public HingeJoint2D EndJoint;

    public float StartJointCurrentLoad = 0;
    public float EndJointCurrentLoad = 0;
    public float MaxLoad;

    public float ActualCost;

    public MaterialPropertyBlock propertyBlock;

    public void UpdateCreatingBar(Vector2 ToPosition)
    {
        transform.position = (ToPosition + startPosition) / 2;
        Vector2 direction = ToPosition - startPosition;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        float length = direction.magnitude;
        BarSpriteRenderer.size = new Vector2(length, BarSpriteRenderer.size.y);

        BoxCollider.size = BarSpriteRenderer.size;

        ActualCost = length * Cost;
    }

    public void UpdateMaterial()
    {
        if(StartJoint)
            StartJointCurrentLoad = StartJoint.reactionForce.magnitude / StartJoint.breakForce;
        if(EndJoint)
            StartJointCurrentLoad = EndJoint.reactionForce.magnitude / EndJoint.breakForce;

        MaxLoad = Mathf.Max(StartJointCurrentLoad, EndJointCurrentLoad);

        propertyBlock = new MaterialPropertyBlock();
        BarSpriteRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_Load", MaxLoad);
        BarSpriteRenderer.SetPropertyBlock(propertyBlock);
    }
    private void Update()
    {
        if (Time.timeScale == 1)
            UpdateMaterial();
    }
}
