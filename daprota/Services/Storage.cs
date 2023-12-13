using daprota.Models;
using System.Text.Json;
using System.Xml.Serialization;

namespace daprota.Services
{
    public class Storage
    {
        public static List<M_Question> questions { get; set; }

        public string GetUserDataFromPrefs()
        {
            string s = Preferences.Default.Get<string>("Settings", null);
            return s;
        }

        public void SetUserDataToPrefs<T>(T obj)
        {
            // Convert Obj to JSON string
            Preferences.Default.Set<string>("Settings", ConvertObjToJSONString<T>(obj));
        }

        public T? ConvertJSONStringToObj<T>(string jsonString){
            try
            {
                T? obj = JsonSerializer.Deserialize<T>(jsonString);
                return obj;
            } catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON string: " + ex.Message);
                return default;
            }
        }

        public string ConvertObjToJSONString<T>(T obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Converting obj to JSON string: " + ex.Message);
                return default;
            }
        }

        public T? ReadJsonFile<T>(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                T? deserializedObject = JsonSerializer.Deserialize<T>(jsonData);
                return deserializedObject;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON file: " + ex.Message);
                return default;
            }
        }

        public async Task<T> ReadEmbeddedXML<T>(string file)
        {
            string data = string.Empty;

            try
            {
                using Stream stream = await FileSystem.Current.OpenAppPackageFileAsync(file);
                using (TextReader reader = new StreamReader(stream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERROR ", $"DATA: {data}\n {ex}", "ok");
                return default(T);
            }
        }
    }
}
