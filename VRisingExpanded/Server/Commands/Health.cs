using Wetstone.API;
using Wetstone.Hooks;

namespace VRisingExpanded.Server.Commands
{
    public class Health : ICommand
    {
        public string Keys => "health/hp";

        public string Usage => "<数值>";

        public string Description => "改变自己当前生命值。";

        public bool AdminOnly => true;

        public void Initialize(CommandContext context)
        {
            float hp;

            if (context.Args.Length < 1)
            {
                SystemMessage.MissingArguments(context, "<数值>");
                return;
            }

            if (!float.TryParse(context.Args[0], out hp))
            {
                SystemMessage.InvalidArguments(context, context.Args[0]);
                return;
            }

            context.ChatEvent.SenderCharacterEntity.WithComponentData((ref ProjectM.Health health) =>
            {
                health.Value = hp;
            });

            SystemMessage.CommandSuccess(context, $"已将你的血量调整到{hp}。");
        }
    }
}