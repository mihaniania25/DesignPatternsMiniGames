using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternsMiniGames.Utility
{
    public static class GameLog
    {
        private static readonly List<LogChannel> ACTIVE_CHANNELS = new List<LogChannel>
        {
            LogChannel.Default, 
        };

        public static void Info(string message, LogChannel channel = LogChannel.Default)
        {
            if (ACTIVE_CHANNELS.Contains(channel))
                Debug.Log(HandleMessage(message, channel));
        }

        public static void Error(string message, LogChannel channel = LogChannel.Default)
        {
            if (ACTIVE_CHANNELS.Contains(channel))
                Debug.LogError(HandleMessage(message, channel));
        }

        public static void Warning(string message, LogChannel channel = LogChannel.Default)
        {
            if (ACTIVE_CHANNELS.Contains(channel))
                Debug.LogWarning(HandleMessage(message, channel));
        }

        private static string HandleMessage(string message, LogChannel channel)
        {
            string formattedMessage = message;
            if (channel != LogChannel.Default)
                formattedMessage = $"<{channel}> {message}";

            return formattedMessage;
        }
    }
}