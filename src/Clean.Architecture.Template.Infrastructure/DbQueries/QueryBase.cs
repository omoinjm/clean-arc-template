using System.Web;
using Clean.Architecture.Template.Core.Entity.Common;
using Clean.Architecture.Template.Core.Specs;
using Newtonsoft.Json;

namespace Clean.Architecture.Template.Infrastructure.DbQueries
{
    public abstract class QueryBase
    {
        protected static string? _sortExpression;
        protected static string? _whereClause;

        public abstract string Query(GeneralSpecParams generalSpecParams);

        public void ApplySortExpression(string field, bool? ascending = null)
        {
            string direction = "ASC";

            if (ascending == false) direction = "DESC";

            if (string.IsNullOrEmpty(field)) field = this.GetDefaultSortField();

            _sortExpression = string.Format("{0} {1}", field, direction);
        }

        protected abstract string GetDefaultSortField();

        /// <summary>
        /// Default implementation for setting the WHERE clause.
        /// Derived classes can override and append additional conditions.
        /// </summary>
        protected virtual void SetWhereClause(GeneralSpecParams generalSpecParams)
        {
            var lookups = new List<LookupEntity>();

            if (!string.IsNullOrEmpty(generalSpecParams.Lookups))
            {
                lookups = [.. JsonConvert
                    .DeserializeObject<List<LookupEntity>>(HttpUtility.UrlDecode(generalSpecParams.Lookups))
                    .Where(x => x.Id != null)];

                foreach (var lookup in lookups)
                {
                    _whereClause += $" AND ({lookup.Name} = {lookup.Id})";
                }
            }

            var ranges = new List<DateRange>();

            if (!string.IsNullOrEmpty(generalSpecParams.Ranges))
            {
                ranges = JsonConvert
                    .DeserializeObject<List<DateRange>>(HttpUtility.UrlDecode(generalSpecParams.Ranges))
                    .Where(x => x.IsOnChange == true).ToList();
            }

            foreach (var range in ranges)
            {
                if (range.IsOnChange == true)
                {

                    if (range.IsRange == true && (range.StartDate != null && range.EndDate != null) && (range.StartDate != range.EndDate))
                    {
                        _whereClause +=
                            $" AND ({range.Name} >= '{range.StartDate?.ToString("yyyy-MM-dd HH:mm:ss")}' AND {range.Name} <= '{range.EndDate?.ToString("yyyy-MM-dd HH:mm:ss")}')";
                    }
                    else if (range.IsRange == false && (range.StartDate != null))
                    {
                        _whereClause +=
                            $" AND ({range.Name} >= '{range.StartDate?.AddDays(1).ToString("yyyy-MM-dd 00:00:00")}' AND {range.Name} < '{range.StartDate?.AddDays(2).ToString("yyyy-MM-dd 00:00:00")}')";
                    }
                }
            }
        }
    }
}