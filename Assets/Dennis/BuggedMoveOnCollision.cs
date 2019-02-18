﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuggedMoveOnCollision : MonoBehaviour
{
    private Vector2 startPos;
    public float timeToDestination = 10f;
    private float timeToLerp;
    private Vector2 differenceInPos;
    private Vector2 travelPos;
    private bool isLerping = false;
    public float narmeVarde;


    private void Start()
    {
        differenceInPos = transform.position;
        travelPos = transform.position;
    }
    private void Update()
    {
        startPos = transform.position;
        timeToLerp += Time.deltaTime / timeToDestination;
        transform.position = Vector2.Lerp(startPos, travelPos, timeToLerp);
        if (Vector2.Distance(startPos, travelPos) < narmeVarde)
        {
            isLerping = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLerping == false)
        {
            if (collision.transform.tag == "Player")
            {
                differenceInPos = -collision.transform.position + transform.position;
                if (Mathf.Abs(differenceInPos.x) > Mathf.Abs(differenceInPos.y))
                {
                    differenceInPos.y = 0;
                    travelPos = new Vector2(transform.position.x - differenceInPos.x, transform.position.y);
                    transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
                }
                else
                {
                    differenceInPos.x = 0;
                    travelPos = new Vector2(transform.position.x, transform.position.y - differenceInPos.y);
                    transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
                }
            }
        }
        isLerping = true;
    }
}