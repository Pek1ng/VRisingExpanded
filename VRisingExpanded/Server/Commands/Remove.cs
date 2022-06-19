using System;
using VRisingExpanded.Objects;

namespace VRisingExpanded.Server.Commands
{
    public class Remove : ICommand
    {
        public string Keys => "remove";

        public string Usage => "<物品> [<数量>]";

        public string Description => "移除一个物品。";

        public bool AdminOnly => true;

        public void Initialize(CommandContext context)
        {
            int stacks = 1;

            if (context.Args.Length < 1)
            {
                SystemMessage.MissingArguments(context, "<物品>");
                return;
            }

            if (context.Args.Length >= 2)
            {
                int.TryParse(context.Args[1], out stacks);
            }

            var guid = Items.GetGUIDFromName(context.Args[0]);

            if (guid.GuidHash == 0)
            {
                SystemMessage.ErrorMessage(context, $"未知的物品:{context.Args[0]}");
                return;
            }

            if (!Inventory.RemoveItem(context.ChatEvent.SenderCharacterEntity, guid, stacks))
            {
                SystemMessage.ErrorMessage(context, $"移除物品失败!");
            }
        }
    }
}