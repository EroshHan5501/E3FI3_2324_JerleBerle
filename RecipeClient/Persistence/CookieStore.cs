﻿using System;
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
                throw new ArgumentNullException("Must be initialized from cookies!");
            }

            _store = new CookieStore();
            return _store;
        }
    }

    public IEnumerable<string>? Cookies { get; private set; }

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
}
