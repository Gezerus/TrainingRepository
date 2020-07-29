using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;


namespace BinaryTreeLibrary
{
    /// <summary>
    /// Class for serialization and deserialization any binary tree to xml file
    /// </summary>
    public static class BinaryTreeSerializer
    {
        /// <summary>
        /// serializes a binary tree to xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree"></param>
        /// <param name="path"></param>
        public static void SerializeToXml<T>(BinaryTree<T> tree, string path) where T: IComparable
        {
            var settings = new DataContractSerializerSettings { PreserveObjectReferences = true };
            var serializer = new DataContractSerializer(typeof(BinaryTree<T>), settings);

            using(Stream fStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(fStream, tree);
            }
        }

        /// <summary>
        /// Deserializes a binary tree from xml file and record to reference parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree"></param>
        /// <param name="path"></param>
        public static void DeserializeFromXml<T>(ref BinaryTree<T> tree, string path) where T: IComparable
        {
            using(Stream fStream = new FileStream(path, FileMode.Open))
            {
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fStream, new XmlDictionaryReaderQuotas());
                DataContractSerializer serializer = new DataContractSerializer(typeof(BinaryTree<T>));
                tree = (BinaryTree<T>)serializer.ReadObject(reader, true);                
            }
        }
    }
}
