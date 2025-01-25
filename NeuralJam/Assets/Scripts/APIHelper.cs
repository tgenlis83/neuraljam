using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public static class APIHelper
{
    private static readonly HttpClient client = new HttpClient();

    private static string api_url = "http://fastapi-alb-1551100572.eu-central-1.elb.amazonaws.com/api/";

    public static TrainData GetWagons()
    {
        HttpResponseMessage response = client.GetAsync(api_url + "wagons").Result;
        response.EnsureSuccessStatusCode();
        string responseBody = response.Content.ReadAsStringAsync().Result;
        return JsonUtility.FromJson<TrainData>(responseBody);
    }
}