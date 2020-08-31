using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleORM
{
    /// <summary>
    /// string creator for iupdating sql server database
    /// </summary>
    class UpdateSqlStringCreator : SqlStringCreator
    {
        private string _tableName;
        private IEnumerable<string> _noIdPropNames;
        private IEnumerable<String> _pkPropNames;

        public UpdateSqlStringCreator(string tableName, IEnumerable<string> noIdPropNames, IEnumerable<string> pkPropNames)
        {
            _tableName = tableName;
            _noIdPropNames = noIdPropNames;
            _pkPropNames = pkPropNames;
        }

        /// <summary>
        /// creates a string for updating database
        /// </summary>
        /// <returns>updation query</</returns>
        public override string Create()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("UPDATE ");
            strBuilder.Append(_tableName);
            strBuilder.Append(" SET ");
            foreach (string noIdName in _noIdPropNames)
                strBuilder.Append(noIdName + " = @" + noIdName + ", ");

            strBuilder.Remove(strBuilder.Length - 2, 2);
            strBuilder.Append(" WHERE ");

            foreach (string pkName in _pkPropNames)
                strBuilder.Append(pkName + " = @" + pkName + ", ");

            strBuilder.Remove(strBuilder.Length - 2, 2);
            strBuilder.Append(";");
            return strBuilder.ToString();
        }
    }
}
