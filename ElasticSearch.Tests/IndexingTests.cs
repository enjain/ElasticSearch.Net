﻿using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class IndexingTests
	{
		 [Test,Ignore]
		public void TestIndexingWithParentId()
		 {
             //TODO crate parent-child mapping
		 	var client = new ElasticSearch.Client.ElasticSearchClient("localhost");
		 	var result= client.Index("index", "type", "1", new Dictionary<string, object>() {{"medcl", "value"}}, "1");
			 Assert.AreEqual(true,result.Success);
			 Thread.Sleep(200);
		 	var doc= client.Get("index", "type", "1", "1");
			 Assert.AreEqual("1",doc.Hits.Id);
		 }

		[Test]
		public void TestOpenCloseIndex()
		{
			var client = new ElasticSearch.Client.ElasticSearchClient("localhost");
			string indexAabbTestOpen = "index_aabb_test_open"+Guid.NewGuid().ToString("N");
			client.CreateIndex(indexAabbTestOpen);

			client.CloseIndex(indexAabbTestOpen);

			var op= client.Index(indexAabbTestOpen, "type", "key", "{a:1}");
			
			Assert.AreEqual(false,op.Success);
			Assert.True(op.JsonString.IndexOf("closed")>0);

			client.OpenIndex(indexAabbTestOpen);
			op = client.Index(indexAabbTestOpen, "type", "key", "{a:1}");
			
			Assert.AreEqual(true, op.Success);
			client.DeleteIndex(indexAabbTestOpen);
		}
	}
}