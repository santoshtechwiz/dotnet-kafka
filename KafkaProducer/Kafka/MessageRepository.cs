using System;
using System.Collections.Generic;

public class MessageRepository
{
    private readonly List<string> _messageStore = new List<string>();

    public void SaveMessage(string message)
    {
        _messageStore.Add(message);
        Console.WriteLine($"Message saved: {message}");
    }

    public IEnumerable<string> GetAllMessages()
    {
        return _messageStore;
    }
}
