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

    public static UserSession CreateSession()
    {
        HttpResponseMessage response = client.PostAsync(api_url + "chat/session", null).Result;
        response.EnsureSuccessStatusCode();
        string responseBody = response.Content.ReadAsStringAsync().Result;
        return JsonUtility.FromJson<UserSession>(responseBody);
    }

    public static UserSession GetSessionStatus(string sessionId)
    {
        HttpResponseMessage response = client.GetAsync(api_url + $"chat/session/{sessionId}").Result;
        response.EnsureSuccessStatusCode();
        string responseBody = response.Content.ReadAsStringAsync().Result;
        return JsonUtility.FromJson<UserSession>(responseBody);
    }

    public static async Task<AdvanceResponse> AdvanceToNextWagon(string sessionId)
    {
        HttpResponseMessage response = await client.PostAsync(api_url + $"chat/session/{sessionId}/advance", null);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Debug.Log(responseBody);
        return JsonUtility.FromJson<AdvanceResponse>(responseBody);
    }

    public static async Task<ChatResponse> ChatWithCharacter(string sessionId, string uid, ChatMessage message)
    {
        var content = new StringContent(JsonUtility.ToJson(message), System.Text.Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(api_url + $"chat/session/{sessionId}/{uid}", content);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        var j = JsonUtility.FromJson<ChatResponse>(responseBody);
        return j;
    }

    public static async Task<ChatHistory> GetChatHistory(string sessionId, string uid)
    {
        HttpResponseMessage response = await client.GetAsync(api_url + $"chat/session/{sessionId}/{uid}/history");
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        return JsonUtility.FromJson<ChatHistory>(responseBody);
    }

    public static PlayerNames GetPlayerNames(int wagonId)
    {
        HttpResponseMessage response = client.GetAsync(api_url + $"players/wagon-{wagonId}?properties=name_info").Result;
        response.EnsureSuccessStatusCode();
        string responseBody = response.Content.ReadAsStringAsync().Result;
        return JsonUtility.FromJson<PlayerNames>(responseBody);
    }

    [Serializable]
    public class PlayerNames
    {
        public Player[] players;
    }


    [Serializable]
    public class Player
    {
        public Name_Info name_info;
    }

    [Serializable]
    public class Name_Info
    {
        public string firstName;
        public string lastName;
        public string sex;
        public string fullName;
    }


    [Serializable]
    public class UserSession
    {
        public string session_id;
        public CurrentWagon current_wagon;
    }

    [Serializable]
    public class CurrentWagon
    {
        public int wagon_id;
    }

    [Serializable]
    public class AdvanceResponse
    {
        public string message;
        public int current_wagon;
    }

    [Serializable]
    public class ChatMessage
    {
        public string message;
    }

    [Serializable]
    public class ChatResponse
    {
        public string uid;
        public string response;
        public string timestamp;
    }

    [Serializable]
    public class ChatHistory
    {
        public string uid;
        public Message[] messages;
    }

    [Serializable]
    public class Message
    {
        public string role;
        public string content;
        public string timestamp;
    }
}