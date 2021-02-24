using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BarCreator : MonoBehaviour, IPointerDownHandler
{
    public GameManager GameManager;

    public GameObject RoadBar;
    public GameObject WoodBar;
    public GameObject SteelBar;

    bool BarCreationStarted = false;

    public Bar CurrentBar;
    public GameObject BarToInstantiate;
    public Transform BarParent;
    public Point CurrentStartPoint;
    public Point CurrentEndPoint;
    public GameObject PointToInstantiate;
    public Transform PointParent;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Time.timeScale == 1)
            return;
        if (BarCreationStarted == false)
        {
            BarCreationStarted = true;
            StartBarCreation(Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(eventData.position)));
        }
        else
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (GameManager.CanPlaceItem(CurrentBar.ActualCost))
                {
                    FinishBarCreation();
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                DeleteCurrentBar();
            }
        }
    }

    private void DeleteCurrentBar()
    {
        BarCreationStarted = false;
        Destroy(CurrentBar.gameObject);
        if (CurrentStartPoint.ConnectedBars.Count == 0 && CurrentStartPoint.Runtime == true)
        {
            Destroy(CurrentStartPoint.gameObject);
        }
        if (CurrentEndPoint.ConnectedBars.Count == 0 && CurrentEndPoint.Runtime == true)
        {
            Destroy(CurrentEndPoint.gameObject);
        }
    }

    void StartBarCreation(Vector2 startPosition)
    {
        CurrentBar = Instantiate(BarToInstantiate, BarParent).GetComponent<Bar>();
        CurrentBar.startPosition = startPosition;

        if (GameManager.AllPoints.ContainsKey(startPosition))
        {
            CurrentStartPoint = GameManager.AllPoints[startPosition];
        }
        else
        {
            CurrentStartPoint = Instantiate(PointToInstantiate, startPosition, Quaternion.identity, PointParent).GetComponent<Point>();
            GameManager.AllPoints.Add(startPosition, CurrentStartPoint);
        }

        CurrentEndPoint = Instantiate(PointToInstantiate, startPosition, Quaternion.identity, PointParent).GetComponent<Point>();
    }
    void FinishBarCreation()
    {
        if (CurrentStartPoint.transform.position == CurrentEndPoint.transform.position)
        {
            Debug.Log("0 length!");
        }
        else
        {
            if (GameManager.AllPoints.ContainsKey(CurrentEndPoint.transform.position))
            {
                Destroy(CurrentEndPoint.gameObject);
                CurrentEndPoint = GameManager.AllPoints[CurrentEndPoint.transform.position];
            }
            else
            {
                GameManager.AllPoints.Add(CurrentEndPoint.transform.position, CurrentEndPoint);
            }
            CurrentStartPoint.ConnectedBars.Add(CurrentBar);
            CurrentEndPoint.ConnectedBars.Add(CurrentBar);

            CurrentBar.StartJoint.connectedBody = CurrentStartPoint.rb;
            CurrentBar.StartJoint.anchor = CurrentBar.transform.InverseTransformPoint(CurrentBar.startPosition);

            CurrentBar.EndJoint.connectedBody = CurrentEndPoint.rb;
            CurrentBar.EndJoint.anchor = CurrentBar.transform.InverseTransformPoint(CurrentEndPoint.transform.position);

            GameManager.UpdateBudget(CurrentBar.ActualCost);

            StartBarCreation(CurrentEndPoint.transform.position);
        }
    }
    private void Update()
    {
        if (BarCreationStarted == true)
        {
            Vector2 endPosition = (Vector2)Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector2 direction = endPosition - CurrentBar.startPosition;
            Vector2 clampedPosition = CurrentBar.startPosition + Vector2.ClampMagnitude(direction, CurrentBar.MaxLength);

            CurrentEndPoint.transform.position = (Vector2)Vector2Int.RoundToInt(clampedPosition);
            CurrentEndPoint.PointID = CurrentEndPoint.transform.position;
            CurrentBar.UpdateCreatingBar(CurrentEndPoint.transform.position);
        }
    }
}
