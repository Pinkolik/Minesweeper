using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class Serializer
    {
        public static void Serialize(object obj, string resourceName)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(resourceName, FileMode.Create, FileAccess.Write))
                formatter.Serialize(stream, obj);
        }

        public static object Deserialize(string resourceName)
        {
            var formatter = new BinaryFormatter();
            object obj;
            using (var stream = new FileStream(resourceName, FileMode.Open, FileAccess.Read))
                obj = formatter.Deserialize(stream);

            return obj;
        }
    }
}