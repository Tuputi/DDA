using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using BonaJson;

namespace SongData
{
    public class Song
    {
        public string DisplayName { get; set; } //What the player sees as the name of the level
        public string ArtistName;               //what the artist is called
        public string SongName;                 //what the song is called
        public string MusicName;                //what the filename for the music is called
        public SongDifficulty Difficulty { get; set; }

        public int BeatsPerSecond { get; set; }
        public int SongLength { get; set; }
        public List<SongNote> SongNotes { get; set; }

        // Recording data
        public SongRecordingSnapping Snapping{ get; set; }

        public Song()
        {
            SongNotes = new List<SongNote>();
        }

        public Song(String name, SongDifficulty difficulty)
        {
            DisplayName = name;
            Difficulty = difficulty;
            SongNotes = new List<SongNote>();
        }

        public JObject JsonSave()
        {
            var result = new JObjectCollection();
            result.Add("DisplayName", DisplayName);
            result.Add("ArtistName", ArtistName);
            result.Add("SongName", SongName);
            result.Add("MusicName", MusicName);
            result.Add("Difficulty", (int)Difficulty);
            result.Add("BeatsPerSecond", BeatsPerSecond);
            result.Add("SongLength", SongLength);
            result.Add("Snapping", (int)Snapping);


            var notesJObject = new JObjectArray();
            result.Add("Notes", notesJObject);
            foreach (var note in SongNotes) {
                notesJObject.Add(note.JsonSave());
            }

            return result;
        }

        public void JsonLoad(JObject jObject)
        {
            this.DisplayName = jObject["DisplayName"].Value <String>();
            this.ArtistName = jObject["ArtistName"].Value<String>();
            this.SongName = jObject["SongName"].Value<String>();
            this.MusicName = jObject["MusicName"].Value<String>();
            this.Difficulty = jObject["Difficulty"].Value<SongDifficulty>();
            this.BeatsPerSecond = jObject["BeatsPerSecond"].Value<int>();
            this.SongLength = jObject["SongLength"].Value<int>();
            this.Snapping = jObject["Snapping"].Value<SongRecordingSnapping>();

            foreach(var note in jObject["Notes"]) {
                var songNote = new SongNote();
                SongNotes.Add(songNote);
                songNote.JsonLoad(note);
            }
        }
    }
}
