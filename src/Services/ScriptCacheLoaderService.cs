namespace SmartLife.Services;

public class ScriptCacheLoaderService(ScriptCacheService scriptCache) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        scriptCache.LoadScriptsToCache();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}