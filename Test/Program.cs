using System.Reflection;
using System.Text;
using VRisingExpanded.Server.Commands;

StringBuilder sb = new StringBuilder();
foreach (var item in CommandHandler.Commands)
{
    sb.AppendLine($"{item.Key.PadRight(8)}|#{item.Key} {item.Value.Usage}|{item.Value.Description}");
}

Console.WriteLine(sb.ToString());