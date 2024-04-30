using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Select.Messaging;

public class AppMessage
{
}

public static class AppMessageBus
{
    private static readonly ConcurrentDictionary<string, List<object>> Subscribers =
        new(StringComparer.InvariantCultureIgnoreCase);

    private static string GetKey(Type type) => type.Name;

    public static void Subscribe<T>(Action<T> subscriber) where T : AppMessage
    {
        var key = GetKey(typeof(T));

        if (!Subscribers.TryGetValue(key, out var subscribers))
        {
            subscribers = [];
            Subscribers.TryAdd(key, subscribers);
        }

        subscribers.Add(subscriber);
    }

    public static void Unsubscribe<T>(Action<T> subscriber) where T : AppMessage
    {
        var key = GetKey(typeof(T));

        if (Subscribers.TryGetValue(key, out var subscribers))
        {
            subscribers.Remove(subscriber);
        }
    }

    public static void Send<T>() where T : AppMessage, new() => Send(new T());

    public static void Send<T>(T message) where T : AppMessage
    {
        var key = message.GetType().Name;

        if (!Subscribers.TryGetValue(key, out var subscribers)) return;

        var i = 0;
        while (i < subscribers.Count)
        {
            var subscriber = subscribers[i++];
            ((Action<T>)subscriber)(message);
        }
    }
}

