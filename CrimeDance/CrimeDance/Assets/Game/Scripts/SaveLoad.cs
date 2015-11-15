using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour {


public class NoteXml
{
    [XmlAttribute("direction")]
    public int dir;

    [XmlAttribute("time")]
    public double time;

    [XmlAttribute("Row")]
    public int row;

        [XmlAttribute("Type")]
        public int type;

    //[XmlAttribute("IsGolden")]
    //public bool isGolden;

    [XmlAttribute("IsStarter")]
    public bool isStarter;

    [XmlAttribute("IsEndnote")]
    public bool isEndNote;

    }

[XmlRoot("SongXml")]
public class SongXml
{
    [XmlAttribute("SongName")]
    public string SongName;

    [XmlAttribute("Time")]
    public int time;

    [XmlArray("Notes")]
    [XmlArrayItem("Note")]
    public List<NoteXml> SongNotes = new List<NoteXml>();
}

public static class SongSaveLoad
{
    public static SongXml CreateSongXml(string songname, int songlenght, List<Note> notes)
    {

        List<NoteXml> SongNotes = new List<NoteXml>();
        for (int i = 0; i < notes.Count; i++)
        {
            
           SongNotes.Add(SongSaveLoad.CreateNoteXml(notes[i]));
            
        }

        return new SongXml()
        {
            SongName = songname,
            time = songlenght,
            SongNotes = SongNotes
        };
 }

 public static NoteXml CreateNoteXml(Note note)
 {
            return new NoteXml
            {
                dir = (int)note.direction,
                time = note.PlayTime,
                row = note.Row,
                type = (int)note.NoteType,
                //isGolden = note.isGolden,
                isStarter = note.isStarter,
                isEndNote = note.isEndNote
        };
 }

 public static void Save(SongXml songXml, string filename)
 {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        if (!File.Exists(path + "/DanceDanceAssassination/Songs/" + filename))
        {
                //System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Songs/");     
                System.IO.Directory.CreateDirectory( path + "/DanceDanceAssassination/Songs/");
            }
        var encoding = System.Text.Encoding.GetEncoding("UTF-8");
        Debug.Log(Application.persistentDataPath + "/Songs/" + filename);
        var serializer = new XmlSerializer(typeof(SongXml));
        using (StreamWriter stream = new StreamWriter(path + "/DanceDanceAssassination/Songs/" + filename, false, encoding))
        {
            serializer.Serialize(stream, songXml);
        }
        Debug.Log(path + "/DanceDanceAssassination/Songs/" + filename);
    }


    public static SongXml Load(string filename)
    {
        TextAsset ta = Resources.Load(filename) as TextAsset;
        Debug.Log(ta);
        var serializer = new XmlSerializer(typeof(SongXml));
        using (var reader = new System.IO.StringReader(ta.text))
        {
            return serializer.Deserialize(reader) as SongXml;
        }

    }

}

}
