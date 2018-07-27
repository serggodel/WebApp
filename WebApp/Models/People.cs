using System;
using Newtonsoft.Json;

namespace WebApp.Models
{
    public class People
    {
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
