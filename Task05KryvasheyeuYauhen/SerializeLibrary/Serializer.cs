using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Xml;

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
        // number of fiedls of class T
        private int _fieldsCount;
        // names of fields of class T
        private string[] _fildNames;
        // types of fields of class T
        private string[] _fildTypes;

        /// <summary>
        /// constructor without parameters for serialization
        /// </summary>
        private Serializer()
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
            // get number of fields, name of fields and type of fields of class T
            _fieldsCount = fields.Length;
            _fildNames = new string[_fieldsCount];
            _fildTypes = new string[_fieldsCount];

            for(int i = 0; i < _fieldsCount; i++)
            {
                _fildNames[i] = fields[i].Name;
                _fildTypes[i] = fields[i].FieldType.ToString();
            }

        }
        /// <summary>
        /// Serializes the class that was used during initialization the Serializer to a binary file
        /// </summary>
        /// <param name="path"></param>
        public void BinarySerialize(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {                
                formatter.Serialize(fStream, this);
            }
        }
        /// <summary>
        /// Deserializes class type T from a binary file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>result of deserialization</returns>
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
        /// <summary>
        /// Serializes a collection of class type T to a binary file
        /// </summary>
        /// <param name="dataCollection"></param>
        /// <param name="path"></param>
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
        /// <summary>
        /// Deserializes a collection of class type T from a binary file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Serializes the class that was used during initialization the Serializer to a json file
        /// </summary>
        /// <param name="path"></param>
        public void JsonSerialize(string path)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Serializer<T>));
            using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fStream, this);
            }

        }
        /// <summary>
        /// Deserializes class type T from a json file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>deserialization result</returns>
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

        /// <summary>
        /// Serializes a collection of class type T to a json file
        /// </summary>
        /// <param name="dataCollection"></param>
        /// <param name="path"></param>
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

        /// <summary>
        /// Deserializes a collection of class type T from a json file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>deserialization result</returns>
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

        /// <summary>
        /// Serializes the class that was used during initialization the Serializer to a xml file
        /// </summary>
        /// <param name="path"></param>
        public void XmlSerialize(string path)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Serializer<T>));
            using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fStream, this);
            }
        }

        /// <summary>
        /// Deserializes class type T from a xml file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>deserialization result</returns>
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

        /// <summary>
        /// Serializes a collection of class type T to a xml file
        /// </summary>
        /// <param name="dataCollection"></param>
        /// <param name="path"></param>
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

        /// <summary>
        /// Deserializes a collection of class type T from a xml file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>deserialization result</returns>
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

        /// <summary>
        /// checks the version of the class and throws an exception if the version is wrong
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="serializationResult"></param>
        private static void ClassVersionCheck(FieldInfo[] fields, Serializer<T> serializationResult)
        {
            if (fields.Length != serializationResult._fieldsCount)
                throw new SerializationException("number of fields does not match");

            bool flagName = false;
            bool flagType = false;

            for (int i = 0; i < serializationResult._fieldsCount; i++)
            {
                for(int j = 0; j < fields.Length; j++)
                {
                    if(fields[j].Name == serializationResult._fildNames[i])
                    {
                        flagName = true;
                        if (fields[j].FieldType.ToString() == serializationResult._fildTypes[i])
                            flagType = true;
                    }
                        
                }
                if (flagName == false)                
                    throw new SerializationException("name of field does not match");                    
                
                if (flagType == false)
                    throw new SerializationException("type of field does not match");

                flagName = false;
                flagType = false;
            }
        }
    }
}
