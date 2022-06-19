using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wetstone.Hooks;
using Wetstone.API;
using Unity.Entities;

namespace VRisingExpanded.Server.Commands
{
    public class CommandContext
    {
        public string CommandName { get; set; }

        public VChatEvent ChatEvent { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string[] Args { get; set; }

        /// <summary>
        /// 发送系统消息
        /// </summary>
        public Action<string> SendSystemMessage { get; set; }

        public EntityManager EntityManager { get; set; }

        public CommandContext(VChatEvent chatEvent, string commandName)
        {
            ChatEvent = chatEvent;
            SendSystemMessage = chatEvent.User.SendSystemMessage;
            EntityManager = VWorld.Server.EntityManager;
            CommandName = commandName;

            Args = chatEvent.Message.Split(' ').Skip(1).ToArray();
        }
    }
}