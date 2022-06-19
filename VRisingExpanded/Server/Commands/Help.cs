using System.Text;
using Wetstone.API;

namespace VRisingExpanded.Server.Commands
{
    public class Help : ICommand
    {
        public string Keys => "help";

        public string Usage => "";

        public string Description => "列出所有的命令。";

        public bool AdminOnly => false;

        public void Initialize(CommandContext context)
        {
            foreach (var item in CommandHandler.Commands)
            {
                if (!context.ChatEvent.User.IsAdmin && item.Value.AdminOnly)
                {
                    continue;
                }

                string usage = $"#{item.Key} {item.Value.Usage}";

                context.SendSystemMessage($"{item.Key}|{usage}|{item.Value.Description}");
            }
        }
    }
}