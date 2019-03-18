﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class conditionPlaylist
{
    public QuestSO completedQuest;
    public List<AudioClip> songs = new List<AudioClip>();
}

public class Playlist : MonoBehaviour
{

    public enum PlayBeaviour { loopLast, loopPlaylist };
    public PlayBeaviour playlistBehaviour = PlayBeaviour.loopLast;

    public List<AudioClip> songs = new List<AudioClip>();
    public conditionPlaylist playlistWithCondition;

    void Start()
    {
        if (playlistWithCondition.completedQuest == null)
        {
            if (songs.Count == 1)
            {
                AudioManager.instance.changeSong(songs[0]);
            }
            else if (songs.Count > 0)
            {
                AudioManager.instance.playSongs(songs, playlistBehaviour);
            }
            else
            {
                Debug.LogError("No songs in playlist");
            }
        }
    }

}
