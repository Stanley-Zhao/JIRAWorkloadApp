using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Threading.Tasks;
using System.IO;

public class HttpClientHelper
{
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
            //if (response.IsSuccessStatusCode)
            //{
            try
            {
                data = response.Content.ReadAsAsync<List<T>>().Result;
            }
            catch (Exception e)
            {
                return data;
            }
            //}
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
            //if (response.IsSuccessStatusCode)
            //{
            data = response.Content.ReadAsAsync<String>().Result;
            //}
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

            string password = "amd1bzpBZHZlbnQuNw==";
            //Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "jguo", "")));

            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", password);

            //client2.DefaultRequestHeaders.Add("cookie", ses);
            //client2.DefaultRequestHeaders.Authorization.add

            // HTTP GET
            HttpResponseMessage response = client2.GetAsync(requestUri).Result;
            //if (response.IsSuccessStatusCode)
            //{
            result = await response.Content.ReadAsStringAsync();

            //string yy   = response2.Content.read();
            //}
        }

        return result;
    }

    //private string GetEncodedCredentials()
    //{
    //    string mergedCredentials = string.Format("{0}:{1}", m_Username, m_Password);
    //    byte[] byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
    //    return Convert.ToBase64String(byteCredentials);
    //}




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
}
