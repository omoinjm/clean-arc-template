using System.Web;
using Clean.Architecture.Template.Core.Specs;
using Newtonsoft.Json;

namespace Clean.Architecture.Template.Infrastructure.DbQueries.Common
{
    public class LookupQuery : QueryBase
    {
        public override string Query(GeneralSpecParams generalSpecParams)
        {
            return $@"";
        }

        public static string Query(LookupParams lookupSpecParams)
        {
            SetWhereClause(lookupSpecParams);

            _sortExpression = $" ORDER BY {lookupSpecParams.LookupName} ASC";

            var query = @$"

                SELECT

                    {lookupSpecParams.LookupPrimaryKey} AS id,
                    COALESCE({lookupSpecParams.LookupName}, 'No Name') AS name
                
                FROM {lookupSpecParams.LookupTableName}

                WHERE 1=1 AND COALESCE(deleted, true) = true {_whereClause}
                
                {_sortExpression}
            ";

            return query;
        }

        protected override string GetDefaultSortField()
        {
            return "";
        }

        private static void SetWhereClause(LookupParams lookupSpecParams)
        {
            var selectedIds = JsonConvert.DeserializeObject<int[]>(HttpUtility.UrlDecode(lookupSpecParams.SelectedIds));

            if (selectedIds != null && selectedIds.Length > 0)
            {
                _whereClause += $" AND ({lookupSpecParams.LookupPrimaryKey} IN ({string.Join(",", selectedIds)}))";
            }
        }
    }
}