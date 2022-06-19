using System.Text;
using VRisingExpanded.Objects;

namespace VRisingExpanded.Server.Commands
{
    public class Players : ICommand
    {
        public string Keys => "players";

        public string Usage => "players [<bool>]";

        public string Description => "列出所有玩家";

        public bool AdminOnly => false;

        public void Initialize(CommandContext context)
        {
            bool listOfflinePlayers = false;

            if (context.Args.Length >= 1)
            {
                bool.TryParse(context.Args[0], out listOfflinePlayers);
            }

            StringBuilder stringBuilder = new StringBuilder();

            if (listOfflinePlayers)
            {
                foreach (var player in Player.GetPlayerList(context.ChatEvent))
                {
                    stringBuilder.Append(',');
                    stringBuilder.AppendLine(player.CharacterName.ToString());
                }
            }
            else
            {
                foreach (var player in Player.GetPlayerList(context.ChatEvent))
                {
                    if (player.IsConnected)
                    {
                        stringBuilder.Append(',');
                        stringBuilder.Append(player.CharacterName.ToString());
                    }
                }
            }

            context.SendSystemMessage(stringBuilder.ToString().Remove(0, 1));
        }
    }
}