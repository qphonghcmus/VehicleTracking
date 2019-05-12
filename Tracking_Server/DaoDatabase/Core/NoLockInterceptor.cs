#region header

// /*********************************************************************************************/
// Project :DaoDatabase
// FileName : NoLockInterceptor.cs
// Time Create : 9:59 AM 09/03/2017
// Author:  Văn Luật (vanluat1992@gmail.com)
// /********************************************************************************************/

#endregion

#region include

using System;
using System.Linq;
using NHibernate;
using NHibernate.SqlCommand;

#endregion

namespace DaoDatabase.Core
{
    public class NoLockInterceptor : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            //var log = new StringBuilder();
            //log.Append(sql.ToString());
            //log.AppendLine();

            // Modify the sql to add hints
            if (sql.StartsWithCaseInsensitive("select"))
            {
                var parts = sql.ToString().Split().ToList();
                var fromItem = parts.FirstOrDefault(p => p.Trim().Equals("from", StringComparison.OrdinalIgnoreCase));
                var fromIndex = fromItem != null ? parts.IndexOf(fromItem) : -1;
                var whereItem = parts.FirstOrDefault(p => p.Trim().Equals("where", StringComparison.OrdinalIgnoreCase));
                var whereIndex = whereItem != null ? parts.IndexOf(whereItem) : parts.Count;

                if (fromIndex == -1)
                    return sql;

                parts.Insert(parts.IndexOf(fromItem) + 3, "WITH (NOLOCK)");
                for (var i = fromIndex; i < whereIndex; i++)
                {
                    if (parts[i - 1].Equals(","))
                    {
                        parts.Insert(i + 3, "WITH (NOLOCK)");
                        i += 3;
                    }
                    if (parts[i].Trim().Equals("on", StringComparison.OrdinalIgnoreCase))
                    {
                        parts[i] = "WITH (NOLOCK) on";
                    }
                }
                // MUST use SqlString.Parse() method instead of new SqlString()
                sql = SqlString.Parse(string.Join(" ", parts));
            }

            //log.Append(sql);
            return sql;
        }
    }
}