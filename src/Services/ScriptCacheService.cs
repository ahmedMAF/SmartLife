using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using SmartLife.Data;

namespace SmartLife.Services;

public class ScriptCacheService(IMemoryCache cache)
{
    private const string CacheKeyPrefix = "DynamicScripts_";

    // Call this after admin updates scripts.json
    public void LoadScriptsToCache()
    {
        string jsonPath = "scripts.json";

        if (!File.Exists(jsonPath))
            return;

        string json = File.ReadAllText(jsonPath);
        List<ScriptModel> scripts = JsonSerializer.Deserialize<List<ScriptModel>>(json) ?? [];

        foreach (ScriptLocation loc in Enum.GetValues<ScriptLocation>())
        {
            // Filter scripts for the current location
            IEnumerable<string> scriptsForLoc = scripts
                .Where(x => x.Location == loc)
                .Select(x => x.Content);

            // Render scripts into a single string
            StringBuilder builder = new();

            foreach (string script in scriptsForLoc)
                builder.AppendLine(script).AppendLine();

            // Cache the rendered scripts for this location
            cache.Set($"{CacheKeyPrefix}{loc}", builder.ToString());
        }
    }

    public string? GetScripts(ScriptLocation location)
    {
        cache.TryGetValue($"{CacheKeyPrefix}{location}", out string? scripts);
        return scripts;
    }
}