using System.Text.Json;
using System.Xml.Serialization;

namespace daprota.Services
{
    public class Storage
    {
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
