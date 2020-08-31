
using System.Collections.Generic;
using System.Text;


namespace SimpleORM
{
    /// <summary>
    /// string creator for inserting to sql server database
    /// </summary>
    public class InsertSqlStringCreator : SqlStringCreator
    {
        private string _tableName;
        private IEnumerable<string> _propNames;
        public InsertSqlStringCreator(string tableName, IEnumerable<string> propNames)
        {
            _tableName = tableName;
            _propNames = propNames;
        }

        /// <summary>
        /// creates a string for inserting to database
        /// </summary>
        /// <returns>insertion query</returns>
        public override string Create()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO ");
            strBuilder.Append(_tableName);
            strBuilder.Append(" (");
            foreach(string pName in _propNames)
                strBuilder.Append(pName + ", ");

            strBuilder.Remove(strBuilder.Length - 2, 2);
            strBuilder.Append(") VALUES (");

            foreach (string pName in _propNames)
                strBuilder.Append("@" + pName + ", ");

            strBuilder.Remove(strBuilder.Length - 2, 2);
            strBuilder.Append(")");
            return strBuilder.ToString();
        }
    }
}
