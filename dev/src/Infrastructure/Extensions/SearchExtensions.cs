using EPiServer.Find;
using EPiServer.Find.Api;
using EPiServer.Find.Api.Querying;
using EPiServer.Find.Api.Querying.Queries;
using EPiServer.Find.Cms;
using Perficient.Infrastructure.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Perficient.Infrastructure.Extensions
{
    public static class SearchExtensions
    {
        public static ITypeSearch<T> ForWithWildcards<T>(this ITypeSearch<T> search,
    string query, params (Expression<Func<T, string>>, double?)[] fieldSelectors)
        {
            return search
                    .For(query)
                    .InFields(fieldSelectors.Select(x => x.Item1).ToArray())
                    .ApplyBestBets()
                    .WildcardSearch(query, fieldSelectors);
        }

        public static ITypeSearch<T> WildcardSearch<T>(this ITypeSearch<T> search,
            string query, params (Expression<Func<T, string>>, double?)[] fieldSelectors)
        {
            if (string.IsNullOrWhiteSpace(query))
                return search;

            query = query.ToLowerInvariant().Replace('\'', '*');

            var words = query.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(WrapInAsterisks)
                .ToList();

            var wildcardQueries = new List<WildcardQuery>();

            foreach (var fieldSelector in fieldSelectors)
            {
                string fieldName = search.Client.Conventions
                    .FieldNameConvention
                    .GetFieldNameForAnalyzed(fieldSelector.Item1);

                foreach (var word in words)
                {
                    wildcardQueries.Add(new WildcardQuery(fieldName, word)
                    {
                        Boost = fieldSelector.Item2
                    });
                }
            }

            return new Search<T, WildcardQuery>(search, context =>
            {
                var boolQuery = new BoolQuery();

                if (context.RequestBody.Query != null)
                {
                    boolQuery.Should.Add(context.RequestBody.Query);
                }

                foreach (var wildcardQuery in wildcardQueries)
                {
                    boolQuery.Should.Add(wildcardQuery);
                }

                boolQuery.MinimumNumberShouldMatch = 1;
                context.RequestBody.Query = boolQuery;
            });
        }

        public static string WrapInAsterisks(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? "*" : $"*{input.Trim().Trim('*')}*";
        }

        public static ITypeSearch<TSource> OrderByScore<TSource>(this ITypeSearch<TSource> search)
        {
            return new Search<TSource, IQuery>(search, context =>
                context.RequestBody.Sort.Add(new Sorting("_score")));
        }
    }
}
