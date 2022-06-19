using ProjectM;
using System;
using Unity.Entities;
using VRisingExpanded.Objects;
using Wetstone.API;

namespace VRisingExpanded.Server.Commands
{
    public class BloodPotion : ICommand
    {
        public string Keys => "bloodpotion/bp";

        public string Usage => "<血型> <血质>";

        public string Description => "获得一瓶自定义血型和血质的药水。";

        public bool AdminOnly => throw new NotImplementedException();

        public void Initialize(CommandContext context)
        {
            if (context.Args.Length != 0)
            {
                try
                {
                    BloodType type = BloodType.Frailed;
                    float quality = 100;

                    if (context.Args.Length >= 1) type = GetBloodTypeFromName(context.Args[0]);
                    if (context.Args.Length >= 2)
                    {
                        quality = float.Parse(context.Args[1]);
                        if (float.Parse(context.Args[1]) < 0) quality = 0;
                        if (float.Parse(context.Args[1]) > 100) quality = 100;
                    }

                    Entity entity = Inventory.AddItem(context.ChatEvent.SenderCharacterEntity, new PrefabGUID(828432508), 1);
                    var blood = context.EntityManager.GetComponentData<StoredBlood>(entity);
                    blood.BloodQuality = quality;
                    blood.BloodType = new PrefabGUID((int)type);
                    VWorld.Server.EntityManager.SetComponentData(entity, blood);

                    SystemMessage.CommandSuccess(context, $"获得一瓶{type},血质{quality}% ");
                }
                catch
                {
                    SystemMessage.CommandError(context);
                }

                return;
            }

            SystemMessage.MissingArguments(context, "<血质>");
        }

        public static BloodType GetBloodTypeFromName(string name)
        {
            BloodType type = BloodType.Frailed;

            Enum.TryParse(name, true, out type);

            return type;
        }

        public enum BloodType
        {
            未定义 = -899826404,
            生物 = -77658840,
            战士 = -1094467405,
            无赖 = 793735874,
            猛兽 = 581377887,
            学者 = -586506765,
            工人 = -540707191,

            Frailed = -899826404,
            sw = -77658840,
            zs = -1094467405,
            wl = 793735874,
            ms = 581377887,
            xz = -586506765,
            gr = -540707191,
        }
    }
}