
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SerializeLibrary
{
    /// <summary>
    /// Describes a class for serialization any type that implements the ISerialize interface.
    /// </summary>
    /// <typeparam name="T">a type that implements the ISerialize interface</typeparam>
    [Serializable]
    public class Serializer<T>  where T : ISerialize
    {
        private T _data;

        private int _fieldsCount;

        private string[] _fildNames;

        private string[] _fildTypes;

        /// <summary>
        /// constructor without parameters for serialization
        /// </summary>
        public Serializer()
        {

        }

        /// <summary>
        /// initializes a serializator
        /// </summary>
        /// <param name="data"></param>
        public Serializer(T data)
        {
            _data = data;
            var fields = _data.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            _fieldsCount = fields.Length;
            _fildNames = new string[_fieldsCount];
            _fildTypes = new string[_fieldsCount];

            for(int i = 0; i < _fieldsCount; i++)
            {
                _fildNames[i] = fields[i].Name;
                _fildTypes[i] = fields[i].FieldType.ToString();
            }

        }

        public void BinarySerialize(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {                
                formatter.Serialize(fStream, this);
            }
        }

        public static T BinaryDeserialize(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fStream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    var result = (Serializer<T>)formatter.Deserialize(fStream);
                    var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    ClassVersionCheck(fields, result);
                    return result._data;
                }
                catch(ArgumentException e)
                {
                    throw e;
                } 
                catch(SerializationException e)
                {
                    throw e;
                }
            }
        }

        public static void BinaryCollectionSerialize(ICollection<T> dataCollection, string path)
        {
            var serializators = new List<Serializer<T>>();

            foreach (T data in dataCollection)
                serializators.Add(new Serializer<T>(data));

            BinaryFormatter formater = new BinaryFormatter();

            using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                formater.Serialize(fStream, serializators);
            }
        }

        public static ICollection<T> BinaryCollectionDeserialize(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fStream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    var deserializeResult = (List<Serializer<T>>)formatter.Deserialize(fStream);
                    var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    if (deserializeResult.Count != 0)
                    {
                        ClassVersionCheck(fields, deserializeResult[0]);
                    }

                    List<T> result = new List<T>();

                    foreach (Serializer<T> ser in deserializeResult)
                        result.Add(ser._data);

                    return result;
                }
                catch (ArgumentException e)
                {
                    throw e;
                }
                catch (SerializationException e)
                {
                    throw e;
                }

            }
        }

        public void JsonSerialize(string path)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Serializer<T>));
            using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fStream, this);
            }

        }

        public static T JsonDeserialize(string path)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Serializer<T>));

            using (FileStream fStream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    var result = (Serializer<T>)serializer.ReadObject(fStream);
                    var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    ClassVersionCheck(fields, result);
                    return result._data;
                }
                catch (ArgumentException e)
                {
                    throw e;
                }
                catch (SerializationException e)
                {
                    throw e;
                }
            }
        }

        public static void JsonCollectionSerialize(ICollection<T> dataCollection, string path)
        {
            var serializators = new List<Serializer<T>>();

            foreach (T data in dataCollection)
                serializators.Add(new Serializer<T>(data));

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Serializer<T>>));

            using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fStream, serializators);
            }
        }

        public static ICollection<T> JsonCollectionDeserialize(string path)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Serializer<T>>));

            using (FileStream fStream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    var deserializeResult = (List<Serializer<T>>)serializer.ReadObject(fStream);
                    var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    if (deserializeResult.Count != 0)
                    {
                        ClassVersionCheck(fields, deserializeResult[0]);
                    }

                    List<T> result = new List<T>();

                    foreach (Serializer<T> ser in deserializeResult)
                        result.Add(ser._data);

                    return result;
                }
                catch (ArgumentException e)
                {
                    throw e;
                }
                catch (SerializationException e)
                {
                    throw e;
                }

            }
        }

        public void XmlSerialize(string path)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Serializer<T>));
            using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fStream, this);
            }
        }

        public static T XmlDeserialize(string path)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Serializer<T>));

            using (FileStream fStream = new FileStream(path, FileMode.Open))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fStream, new XmlDictionaryReaderQuotas());
                try
                {
                    var result = (Serializer<T>)serializer.ReadObject(reader, true);
                    var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    ClassVersionCheck(fields, result);
                    return result._data;
                }
                catch (ArgumentException e)
                {
                    throw e;
                }
                catch (SerializationException e)
                {
                    throw e;
                }
            }
        }

        public static void XmlCollectionSerialize(ICollection<T> dataCollection, string path)
        {
            var serializators = new List<Serializer<T>>();

            foreach (T data in dataCollection)
                serializators.Add(new Serializer<T>(data));

            DataContractSerializer serializer = new DataContractSerializer(typeof(List<Serializer<T>>));

            using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fStream, serializators);
            }
        }

        public static ICollection<T> XmlCollectionDeserialize(string path)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<Serializer<T>>));

            using (FileStream fStream = new FileStream(path, FileMode.Open))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fStream, new XmlDictionaryReaderQuotas());
                try
                {
                    var deserializeResult = (List<Serializer<T>>)serializer.ReadObject(reader,true);
                    var fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    if (deserializeResult.Count != 0)
                    {
                        ClassVersionCheck(fields, deserializeResult[0]);
                    }

                    List<T> result = new List<T>();

                    foreach (Serializer<T> ser in deserializeResult)
                        result.Add(ser._data);

                    return result;
                }
                catch (ArgumentException e)
                {
                    throw e;
                }
                catch (SerializationException e)
                {
                    throw e;
                }

            }
        }
        private static void ClassVersionCheck(FieldInfo[] fields, Serializer<T> serializationResult)
        {
            if (fields.Length != serializationResult._fieldsCount)
                throw new SerializationException("number of fields does not match");
            for (int i = 0; i < serializationResult._fieldsCount; i++)
            {
                if (fields[i].Name != serializationResult._fildNames[i])
                    throw new SerializationException("name of field does not match");
                if (fields[i].FieldType.ToString() != serializationResult._fildTypes[i])
                    throw new SerializationException("type of field does not match");
            }
        }
    }
}
