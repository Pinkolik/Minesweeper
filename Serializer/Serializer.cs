using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Minesweeper
{
    public static class Serializer
    {
        public static void Serialize(object obj, string resourceName)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(resourceName, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, obj);
            }
        }

        public static object Deserialize(string resourceName)
        {
            var formatter = new BinaryFormatter();
            object obj;
            using (var stream = new FileStream(resourceName, FileMode.Open, FileAccess.Read))
            {
                obj = formatter.Deserialize(stream);
            }

            return obj;
        }
    }
}