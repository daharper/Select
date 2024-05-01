using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Select.Messaging;

/// <summary>
/// Implements a simple message bus for the application.
/// </summary>
public static class AppMessageBus
{
    #region private
    
    private static readonly ConcurrentDictionary<string, List<object>> Subscribers =
        new(StringComparer.InvariantCultureIgnoreCase);

    private static string GetKey(Type type) => type.Name;

    #endregion

    /// <summary>
    /// Adds a subscriber for the specified message type.
    /// </summary>
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

    /// <summary>
    /// Unsubscribes the specified subscriber from the specified message type.
    /// </summary>
    public static void Unsubscribe<T>(Action<T> subscriber) where T : AppMessage
    {
        var key = GetKey(typeof(T));

        if (Subscribers.TryGetValue(key, out var subscribers))
        {
            subscribers.Remove(subscriber);
        }
    }

    /// <summary>
    /// Sends a message of the specified type to all subscribers.
    /// </summary>
    public static void Send<T>() where T : AppMessage, new() => Send(new T());

    /// <summary>
    /// Sends the specified message to all subscribers.
    /// </summary>
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

