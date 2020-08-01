﻿using System;


namespace BinaryTreeLibrary
{
    /// <summary>
    /// Describes a test result
    /// </summary>
    public class Test : IComparable
    {
        public int ID { get; set; }

        public string StudentName { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int Grade { get; set; }

        public Test()
        {
            ID = Guid.NewGuid().GetHashCode();
        }

        public int CompareTo(object obj)
        {
            return ID.CompareTo(((Test)obj).ID);
        }
    }


}
