using System.Collections.Generic;
using Newtonsoft.Json;

namespace ElasticSearch.Client.QueryDSL
{
	/// <summary>
	/// A custom_filters_score query allows to execute a query, and if the hit matches a provided filter (ordered), use either a boost or a script associated with it to compute the score
	/// </summary>
	[JsonConverter(typeof(CustomFiltersScoreQueryConverterer))]
	[JsonObject("custom_filters_score")]
	public class CustomFiltersScoreQuery:IQuery
	{
		public IQuery Query;
		public List<IFilter> Filters;
		/// <summary>
		/// A score_mode can be defined to control how multiple matching filters control the score. By default, it is set to first which means the first matching filter will control the score of the result. It can also be set to max/total/avg which will aggregate the result from all matching filters based on the aggregation type.
		/// </summary>
		public string ScoreMode;

		/// <summary>
		/// A script can be used instead of boost for more complex score calculations. With optional params and lang (on the same level as query and filters).
		/// </summary>
		public string Script;

		public CustomFiltersScoreQuery(IQuery query,List<IFilter> filters)
		{
			Query = query;
			Filters = filters;
		}
	}
}