using System;
using System.Collections.Generic;
using System.Text;

namespace VRisingExpanded.Server.Commands
{
    public static class SystemMessage
    {
        public static void ErrorMessage(CommandContext context, string message)
        {
            context.SendSystemMessage($"<color=#ff00ff>{message}</color>");
        }

        public static void MissingArguments(CommandContext context, string arg)
        {
            ErrorMessage(context, $"缺失参数:{arg}!");
        }

        public static void InvalidArguments(CommandContext context, string arg)
        {
            ErrorMessage(context, $"错误的参数:{arg}!");
        }

        public static void CommandError(CommandContext context)
        {
            ErrorMessage(context, $"命令执行失败!");

            GlobalData.Logger.LogWarning($"{context.ChatEvent.User.CharacterName}执行命令{context.CommandName} {string.Join(' ', context.Args)}失败！");
        }

        public static void CommandSuccess(CommandContext context, string message)
        {
            context.SendSystemMessage($"<color=#00ff00>{message}</color>");
        }
    }
}