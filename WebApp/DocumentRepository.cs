using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp
{
	public class DocumentRepository : IDisposable
	{
		private readonly string AccountUri;
		private readonly string AccountKey;
		private const string DbName = "TestDbOne";
		private const string CollectionName = "People";
		private DocumentClient _docClient;

		public DocumentRepository(IConfiguration configuration)
		{
			AccountUri = configuration.GetValue<string>("DbAccount:Uri");
			AccountKey = configuration.GetValue<string>("DbAccount:Key");

			ConnectionPolicy connectionPolicy = new ConnectionPolicy
			{
				ConnectionMode = ConnectionMode.Gateway,
				//ConnectionProtocol = Protocol.Tcp
			};

			_docClient = new DocumentClient(new Uri(AccountUri), AccountKey, connectionPolicy);
		}

		public async Task<List<People>> GetPeoples()
		{
			try
			{
				// var docs = await _docClient.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri("TestDbOne", "People"));
				//return docs.ToList<People>();
				return _docClient.CreateDocumentQuery<People>(UriFactory.CreateDocumentCollectionUri(DbName, CollectionName)).ToList();
			}
			catch (Exception e)
			{
			}
			return new List<People>(0);
		}

		public async Task<People> GetPeople(string id)
		{
			var document = await _docClient.ReadDocumentAsync(UriFactory.CreateDocumentUri(DbName, CollectionName, id));
			return JsonConvert.DeserializeObject<People>(document.Resource.ToString());			
		}

		public async Task AddPeople(People people)
		{
			try
			{
				await _docClient.ReadDocumentAsync(UriFactory.CreateDocumentUri(DbName, CollectionName, people.Id));
			}
			catch (DocumentClientException e)
			{
				if (e.StatusCode == HttpStatusCode.NotFound)
				{
					await _docClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DbName, CollectionName), people);					
				}
				else
				{
					throw;
				}
			}
		}

		public async Task UpdatePeople(People people)
		{
			await _docClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DbName, CollectionName, people.Id), people);
		}

		public async Task DeletePeople(string id)
		{
			await _docClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DbName, CollectionName, id));
		}

		public void Dispose()
		{
			_docClient.Dispose();
		}
	}
}
