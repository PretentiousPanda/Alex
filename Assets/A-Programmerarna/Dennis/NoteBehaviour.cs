﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NoteBehaviour : MonoBehaviour
{
    [SerializeField] UnityEvent afterQuestIsDone;
    List<GameObject> pianoNotes = new List<GameObject>();
    private List<int> playerNoteOrder = new List<int>();
    public List<int> correctNoteOrder = new List<int>();
    [SerializeField] PuzzelController puzzelController;
    int currentNote;

    public event System.Action playAudio;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!pianoNotes.Contains(transform.GetChild(i).gameObject))
            {
                pianoNotes.Add(transform.GetChild(i).gameObject);
            }

        }

        currentNote = pianoNotes.Count / 2;
        pianoNotes[currentNote].GetComponent<Outline>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyInput();

        if (playerNoteOrder.Count == correctNoteOrder.Count)
        {
            if (CheckIfCorrectOrder())
            {
                Debug.Log("Correct Order!");
                puzzelController.updateChanges();
                afterQuestIsDone.Invoke();
            }
            else if (!CheckIfCorrectOrder())
            {
                Debug.Log("GO AGAIN!!!!!!");
                playerNoteOrder.Clear();
            }

        }
    }

    void ApplyInput()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (currentNote != 0)
                {
                    pianoNotes[currentNote].GetComponent<Outline>().enabled = false;
                    currentNote -= 1;
                    pianoNotes[currentNote].GetComponent<Outline>().enabled = true;
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                if (currentNote != (pianoNotes.Count - 1))
                {
                    pianoNotes[currentNote].GetComponent<Outline>().enabled = false;
                    currentNote += 1;
                    pianoNotes[currentNote].GetComponent<Outline>().enabled = true;
                }
            }
        }

            playNoteAudio pianoNote = gameObject.GetComponent<CalculateNotePosition>().pianoNotes[currentNote].gameObject.GetComponent<playNoteAudio>();
        if (Input.GetKeyDown(KeyCode.Space) && !pianoNote.hasBeenSelected)
        {
            if (pianoNote.canBeSelected)
            {
                if (playerNoteOrder.Count < (correctNoteOrder.Count))
                {
                    playerNoteOrder.Add(currentNote);
                    pianoNote.PlayNoteAudio();
                    //pianoNote.GetComponent<Image>().material.color = selectedColor;
                    pianoNote.hasBeenSelected = true;
                    pianoNote.gameObject.GetComponent<Image>().color = Color.gray;
                    if (playerNoteOrder[playerNoteOrder.Count - 1] != correctNoteOrder[playerNoteOrder.Count - 1])
                    {
                        for (int i = 0; i < gameObject.GetComponent<CalculateNotePosition>().pianoNotes.Count; i++)
                        {
                            //pianoNote.hasBeenSelected = false;
                            gameObject.GetComponent<CalculateNotePosition>().pianoNotes[i].gameObject.GetComponent<playNoteAudio>().hasBeenSelected = false;
                            gameObject.GetComponent<CalculateNotePosition>().pianoNotes[i].gameObject.GetComponent<Image>().color = Color.white;
                        }
                        playerNoteOrder.Clear();
                    }
                }
            }
            else
            {
                pianoNote.PlayNoteAudio();
            }
        }
    }
    bool CheckIfCorrectOrder()
    {
        for (int i = 0; i < playerNoteOrder.Count; i++)
        {
            if (playerNoteOrder[i] != correctNoteOrder[i])
            {
                //playerNoteOrder.Clear();
                return false;
            }
        }
        return true;
    }

}
