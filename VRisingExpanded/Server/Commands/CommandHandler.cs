using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wetstone.API;
using Wetstone.Hooks;

namespace VRisingExpanded.Server.Commands
{
    public static class CommandHandler
    {
        private static Dictionary<string, ICommand> _commands = null;

        public static Dictionary<string, ICommand> Commands
        {
            get
            {
                if (_commands == null)
                {
                    _commands = new Dictionary<string, ICommand>();

                    var types = Assembly.GetExecutingAssembly().GetTypes();

                    foreach (var type in types)
                    {
                        if (type.GetInterfaces().Contains(typeof(ICommand)))
                        {
                            var command = (ICommand)Activator.CreateInstance(type);

                            foreach (var item in command.Keys.Split('/'))
                            {
                                _commands.Add(item.ToLower(), command);
                            }
                        }
                    }
                }

                return _commands;
            }
        }

        public static void HandleCommands(VChatEvent ev)
        {
            string key = ev.Message.Split(' ')[0].Remove(0, 1).ToLower();
            foreach (var item in Commands)
            {
                if (item.Key == key)
                {
                    CommandContext context = new CommandContext(ev, item.Key);
                    item.Value.Initialize(context);
                    return;
                }
            }

            ev.User.SendSystemMessage("<color=#ff0000ff>未知的命令!</color>");
        }
    }
}