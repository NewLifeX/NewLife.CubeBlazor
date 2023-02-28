using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace NewLife.CubeBlazor.Razor;

public class JsInitVariables : IAsyncDisposable
{
    IJSObjectReference? _helper;
    IJSRuntime _jsRuntime;
    TimeSpan _timezoneOffset;
  ILocalStorageService _storage;
    static readonly string _timezoneOffsetKey = "timezoneOffset";

    public TimeSpan TimezoneOffset
    {
        get => _timezoneOffset;
        set
        {
            _storage.SetItemAsync(_timezoneOffsetKey, value.TotalMinutes);
            _timezoneOffset = value;
        }
    }

    public JsInitVariables(IJSRuntime jsRuntime, ILocalStorageService storage)
    {
        _jsRuntime = jsRuntime;
        _storage = storage;
    }

    public async Task SetTimezoneOffset()
    {
        var timezoneOffsetResult = await _storage.GetItemAsync<double>(_timezoneOffsetKey);
        if (timezoneOffsetResult >= 0)
        {
            TimezoneOffset = TimeSpan.FromMinutes(timezoneOffsetResult);
            return;
        }
        _helper ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Masa.Stack.Components/js/jsInitVariables/jsInitVariables.js");
        var offset = await _helper.InvokeAsync<double>("getTimezoneOffset");
        TimezoneOffset = TimeSpan.FromMinutes(-offset);
    }

    public async ValueTask DisposeAsync()
    {
        if (_helper is not null)
            await _helper.DisposeAsync();
    }
}
