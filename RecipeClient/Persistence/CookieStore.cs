using System;
using System.Collections.Generic;

namespace RecipeClient.Persistence;

internal class CookieStore
{
    private static CookieStore? _store;

    private static bool isInitalized = false;

    public static CookieStore Store
    {
        get
        {
            if (_store is { } store)
            {
                return store;
            }

            if (!isInitalized)
            {
                throw new ArgumentNullException("Must be initialized with cookies!");
            }

            _store = new CookieStore();
            return _store;
        }
    }

    public bool IsAuthenticated
    {
        get
        {
            try
            {
                string sessionId = GetSessionId();
                return !string.IsNullOrWhiteSpace(sessionId);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public IEnumerable<string> Cookies { get; private set; } = null!;

    private void Initialize(IEnumerable<string> cookies)
    {
        Cookies = cookies;
    }

    public static CookieStore FromCookies(IEnumerable<string> cookies)
    {
        Store.Initialize(cookies);
        isInitalized = true;
        return Store;
    } 

    private string GetSessionId()
    {
        foreach(string value in Cookies)
        {
            if (value.StartsWith("sid"))
            {
                return value.Substring("sid=".Length);
            }
        }

        throw new Exception("Session id is not present!");
    }
}
