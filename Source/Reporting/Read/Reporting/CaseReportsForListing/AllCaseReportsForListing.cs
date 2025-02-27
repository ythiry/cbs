/*---------------------------------------------------------------------------------------------
 *  Copyright (c) The International Federation of Red Cross and Red Crescent Societies. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Linq;
using System.Text.RegularExpressions;
using Dolittle.Queries;
using Dolittle.ReadModels;

namespace Read.Reporting.CaseReportsForListing
{
    public class AllCaseReportsForListing : IQueryFor<CaseReportForListing>
    {
        private readonly IReadModelRepositoryFor<CaseReportForListing> _collection;
        public AllCaseReportsForListing(IReadModelRepositoryFor<CaseReportForListing> collection)
        {
            _collection = collection;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool SortAscending { get; set; } 
        public string SortField { get; set; }

        public IQueryable<CaseReportForListing> Query
        {
            get
            {
                IQueryable<CaseReportForListing> query = _collection.Query.Where(_ => _.Origin.Length > 5 && !Regex.IsMatch(_.Origin, "[a-zA-Z]"));
                if (string.IsNullOrWhiteSpace(SortField))
                    query = SortAscending ? query.OrderBy(x => x.Timestamp) : query.OrderByDescending(x => x.Timestamp);
                else
                    query = query.OrderByFieldName(SortField, SortAscending);

                if (PageSize > 0 && PageNumber >= 0) query = query.Skip(PageSize * PageNumber).Take(PageSize);

                return query;
            }
        }
    }
}
