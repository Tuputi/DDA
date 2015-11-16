using BonaJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SongData
{
    public static class SongImportExport
    {
        public static void SaveSong(SongData.Song song, String filename)
        {
           // var jObjectResult = BonaJson.
        }

        public static SongData.Song LoadSong(String filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var fileContent = File.ReadAllText(path +"/DanceDanceAssassination/Steps/"+filename);
            var songJson = JObject.Parse(fileContent);
            var song = new SongData.Song();
            song.JsonLoad(songJson);
            return song;
        }

        public static SongData.Song LoadSongPath(string wholepath)
        {
            var fileContent = File.ReadAllText(wholepath);
            var songJson = JObject.Parse(fileContent);
            var song = new SongData.Song();
            song.JsonLoad(songJson);
            return song;
        }
    }
}
