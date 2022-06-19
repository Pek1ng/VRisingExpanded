using Wetstone.API;
using Wetstone.Hooks;

namespace VRisingExpanded.Chat
{
    public static class ChatMessageHandler
    {
        public static void HandleChatMessgae(VChatEvent ev)
        {
            GlobalData.Logger.LogInfo(ev.Message);

            if (!ev.Message.StartsWith('#')) return;

            if (ev.Cancelled) return; // 忽略其他插件已经处理的信息。
            ev.Cancel();  // 阻止其他的插件的处理。

            if (VWorld.IsServer)
            {
                Server.Commands.CommandHandler.HandleCommands(ev);
            }
        }
    }
}