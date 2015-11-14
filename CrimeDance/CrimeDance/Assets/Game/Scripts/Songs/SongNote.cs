using UnityEngine;
using System.Collections;
using BonaJson;

namespace SongData
{
    public class SongNote
    {
        public float Beat{ get; set; }
        public Direction Direction { get; set; }
        public NoteType NoteType { get; set; }

        public JObject JsonSave()
        {
            var result = new JObjectCollection();
            result.Add("Beat", Beat);
            result.Add("Direction", (int)Direction);
            result.Add("NoteType", (int)NoteType);

            return result;
        }

        public void JsonLoad(JObject jObject)
        {
            this.Beat = jObject["Beat"].GetAsFloat();
            this.Direction = jObject["Direction"].Value<Direction>();
            this.NoteType = jObject["NoteType"].Value<NoteType>();
        }
    }
}