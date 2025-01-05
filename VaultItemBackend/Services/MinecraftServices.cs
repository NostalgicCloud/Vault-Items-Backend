using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace SQLiteMinecraftApi.Services
{
    public interface IMinecraftServices
    {
        int GetAllowedAmount(string uuid);
        Task<string?> GetPlayerUUID(string playerName);
    }
    public class MinecraftServices : IMinecraftServices
    {
        private class PlayerData
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        private readonly IMemoryCache _cache;
       public MinecraftServices(IMemoryCache cache)
        {
            _cache = cache;
        }
        public int GetAllowedAmount(string uuid)
        {
            //plans to add the ability to allow another 
            return 9;
        }
        public async Task<string?> GetPlayerUUID(string playerName)
        {
            if (_cache.TryGetValue(playerName, out string? uuid))
            {
                return uuid;
            }
            using (var httpClient = new HttpClient())
            {
                var url = $"https://api.mojang.com/users/profiles/minecraft/{playerName}";
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var playerData = JsonSerializer.Deserialize<PlayerData>(jsonResponse);
                    uuid = playerData?.id;

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                    _cache.Set(playerName, uuid, cacheEntryOptions);
                    return uuid;
                }
                else
                {
                    throw new HttpRequestException($"Failed to fetch UUID for player {playerName}. Status code: {response.StatusCode}");
                }
            }
        }
    }
}
