using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class HttpClientHelper
{
	public static void DeleteDataFromApi(string requestUri)
	{
		string data = "";
		using (var client = new HttpClient())
		{
			var baseUrl = ConfigurationManager.ConnectionStrings["ApiUrl"].ConnectionString;
			client.BaseAddress = new Uri(baseUrl);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			// HTTP Delete
			HttpResponseMessage response = client.DeleteAsync(requestUri).Result;
			if (response.IsSuccessStatusCode)
			{
				data = response.Content.ReadAsAsync<string>().Result;
			}
		}
	}

	public static List<T> GetDataFromApi<T>(string requestUri)
	{
		var data = new List<T>();
		using (var client = new HttpClient())
		{
			var baseUrl = ConfigurationManager.ConnectionStrings["ApiUrl"].ConnectionString;
			client.BaseAddress = new Uri(baseUrl);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			// HTTP GET
			HttpResponseMessage response = client.GetAsync(requestUri).Result;
			try
			{
				data = response.Content.ReadAsAsync<List<T>>().Result;
			}
			catch (Exception e)
			{
				return data;
			}
		}
		return data;
	}

	public static string GetSingleDataFromApi<T>(string requestUri) where T : new()
	{
		System.Text.Encoding encode = System.Text.Encoding.ASCII;
		string data;
		using (var client = new HttpClient())
		{
			var baseUrl = ConfigurationManager.ConnectionStrings["ApiUrl"].ConnectionString;
			client.BaseAddress = new Uri(baseUrl);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			// HTTP GET
			HttpResponseMessage response = client.GetAsync(requestUri).Result;
			data = response.Content.ReadAsAsync<String>().Result;			
		}
		return data;
	}

	public async static Task<string> PostDataToApi(string requestUri)
	{
		string result;
		var baseAddress = new Uri(ConfigurationManager.ConnectionStrings["ApiUrl"].ConnectionString);

		using (var client2 = new HttpClient())
		{
			var baseUrl = ConfigurationManager.ConnectionStrings["ApiUrl"].ConnectionString;
			client2.BaseAddress = new Uri(baseUrl);
			client2.DefaultRequestHeaders.Accept.Clear();
			client2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			//string password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", "axysqu", "June25!")));
			string password = "YXh5c3F1Okp1bmUyNSE=";

			client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", password);

			// HTTP GET
			HttpResponseMessage response = client2.GetAsync(requestUri).Result;
			result = await response.Content.ReadAsStringAsync();
		}

		return result;
	}

	public static string PutDataToApi<T>(string requestUri, T data)
	{
		using (var client = new HttpClient())
		{
			var baseUrl = ConfigurationManager.ConnectionStrings["ApiUrl"].ConnectionString;
			client.BaseAddress = new Uri(baseUrl);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			// HTTP PUT
			HttpResponseMessage response = client.PutAsJsonAsync(requestUri, data).Result;
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string id = response.Content.ReadAsAsync<string>().Result;
				//Uri gizmoUrl = response.Headers.Location;
				return id.ToString();
			}
			else
			{
				return "";
			}
		}
	}
}