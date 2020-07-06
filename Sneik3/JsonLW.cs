using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneik3
{
    public class JsonLW
    {
        private string[] Documents = Directory.GetFiles("../../Data/");
        public List<Jugador> LoadJson()
        {
            using (StreamReader r = new StreamReader(Documents[0]))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Jugador>>(json);
            }
        }
        public void WriteJson(List<Jugador> jugadores)
        {
            File.WriteAllText(Documents[0], JsonConvert.SerializeObject(jugadores));
        }
    }
}
