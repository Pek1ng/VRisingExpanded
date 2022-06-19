using ProjectM;
using System.Linq;
using VRisingExpanded.Objects;

namespace VRisingExpanded.Server.Commands
{
    public class Give : ICommand
    {
        public string Keys => "give";

        public string Usage => "<物品> [<数量>]";

        public string Description => "给予物品到背包";

        public bool AdminOnly => true;

        public void Initialize(CommandContext context)
        {
            try
            {
                string name = string.Join(' ', context.Args);
                int amount = 1;
                if (int.TryParse(context.Args.Last(), out int a))
                {
                    name = string.Join(' ', context.Args.SkipLast(1));
                    amount = a;
                }
                PrefabGUID guid = Items.GetGUIDFromName(name);
                if (guid.GuidHash == 0)
                {
                    SystemMessage.ErrorMessage(context, $"未知的物品:{name}");
                    return;
                }

                Inventory.AddItem(context.ChatEvent.SenderCharacterEntity, guid, amount);
                SystemMessage.CommandSuccess(context, $"你获得了{amount}个 {name}");
            }
            catch
            {
                SystemMessage.CommandError(context);
            }
        }
    }
}