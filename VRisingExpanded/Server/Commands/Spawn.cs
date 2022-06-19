using System;
using Wetstone.API;
using Unity.Entities;
using ProjectM;
using Wetstone.Hooks;
using Unity.Mathematics;
using Unity.Transforms;

namespace VRisingExpanded.Server.Commands
{
    public class Spawn : ICommand
    {
        public string Keys => "spawn";

        public string Usage => "<名称> [<位置>]";

        public string Description => "生成一个单位.";

        public bool AdminOnly => true;

        public void Initialize(CommandContext context)
        {
            if (context.Args.Length != 0)
            {
                string name = "";

                if (context.Args.Length == 1)
                {
                    name = context.Args[0];
                    var pos = context.EntityManager.GetComponentData<LocalToWorld>(context.ChatEvent.SenderCharacterEntity).Position;
                    SpawnAtPosition(context, name, new(pos.x, pos.z));
                }
            }
            else
            {
                SystemMessage.MissingArguments(context, "<名称>");
            }
        }

        public static void SpawnAtPosition(CommandContext context, string npc, float2 position)
        {
            try
            {
                var bufferSystem = VWorld.Server.GetExistingSystem<EntityCommandBufferSystem>();
                var buffer = new EntityCommandBufferSafe(Unity.Collections.Allocator.Temp)
                {
                    Unsafe = bufferSystem.CreateCommandBuffer()
                };

                var prefabCollectionSystem = VWorld.Server.GetExistingSystem<PrefabCollectionSystem>();
                foreach (var kv in prefabCollectionSystem._PrefabGuidToNameMap)
                {
                    if (kv.Value.ToString() == npc)
                    {
                        foreach (var pkv in prefabCollectionSystem._PrefabGuidToEntityMap)
                        {
                            if (pkv.Key == kv.Key)
                            {
                                var prefabEntity = pkv.Value;
                                Entity spawnedEntity = buffer.Instantiate(prefabEntity);
                                var translation = VWorld.Server.EntityManager.GetComponentData<Translation>(context.ChatEvent.SenderUserEntity);
                                var f3pos = new float3(position.x, translation.Value.y, position.y);
                                context.ChatEvent.User.SendSystemMessage($"生成单位: {npc} <{f3pos.x}, {f3pos.z}>");
                                buffer.SetComponent(spawnedEntity, new Translation() { Value = f3pos });
                                return;
                            }
                        }
                    }
                }
                SystemMessage.InvalidArguments(context, npc);
                return;
            }
            catch
            {
                SystemMessage.CommandError(context);
            }
        }

        private static Entity _ent = new Entity();

        public static string SpawnUnit(string npc, float x, float z)
        {
            var prefabCollectionSystem = VWorld.Server.GetExistingSystem<PrefabCollectionSystem>();
            foreach (var kv in prefabCollectionSystem._PrefabGuidToNameMap)
            {
                if (kv.Value.ToString().ToLower() != npc.ToLower()) continue;
                VWorld.Server.GetExistingSystem<UnitSpawnerUpdateSystem>().SpawnUnit(_ent, kv.Key, new float3(x, 0, z), 1, 1, 2, -1);
                break;
            }
            return "";
        }
    }
}