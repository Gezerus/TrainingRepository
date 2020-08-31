using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleORM
{
    /// <summary>
    /// string creator for deleting from sql server database
    /// </summary>
    class DeleteSqlStringCreator : SqlStringCreator
    {
        private string _tableName;       
        private IEnumerable<String> _pkPropNames;

        public DeleteSqlStringCreator(string tableName, IEnumerable<string> pkPropNames)
        {
            _tableName = tableName;            
            _pkPropNames = pkPropNames;
        }
        /// <summary>
        /// creates a string for deleting from database
        /// </summary>
        /// <returns>deletion query</returns>
        public override string Create()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("DELETE FROM ");
            strBuilder.Append(_tableName);
            strBuilder.Append(" WHERE ");

            foreach (string pkName in _pkPropNames)
                strBuilder.Append(pkName + " = @" + pkName + ", ");

            strBuilder.Remove(strBuilder.Length - 2, 2);
            strBuilder.Append(";");
            return strBuilder.ToString();
        }
    }
}
