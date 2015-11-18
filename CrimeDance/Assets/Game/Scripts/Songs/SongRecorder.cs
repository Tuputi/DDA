using UnityEngine;
using System.Collections;
using System;

namespace SongData
{
    public static class SongRecorder
    {
        public static Song Song { get; set; }
        public static float TimeScale { get; set; }
        public static float TimePassed { get; set; }

        public static float SnappingScale { get; set; }

        public static bool IsStopped
        { get; set; }

        public static void Update()
        {
            if (IsStopped) {
                return;
            }

            var deltaTime = Time.deltaTime;
            TimePassed += deltaTime * TimeScale;
        }

        public static void StartRecording(String name, String songName, string artistName, string musicName, int beatsPerSecond, SongRecordingSnapping snapping, SongDifficulty difficulty)
        {
            TimePassed = 0;
            IsStopped = false;
            Song = new Song(name, difficulty);
            Song.ArtistName = artistName;
            Song.SongName = songName;
            Song.MusicName = musicName;
            Song.StepListName = name;
            Song.BeatsPerSecond = beatsPerSecond;
            Song.Snapping = snapping;

            TimeScale = (float)beatsPerSecond / 60;
            SnappingScale = 1.0f / (int)snapping;
        }

        public static void AddNote(Direction direction)
        {

            var adjustment = TimePassed % SnappingScale;
            var exactTime = TimePassed - adjustment;

            Song.SongNotes.Add(new SongNote { Beat = exactTime, Direction = direction, NoteType = NoteType.normal });
        }

        public static Song EndRecording()
        {
            IsStopped = true;
            return Song;
        }
    }
}