using ProjectM;
using System;
using System.Runtime.InteropServices;
using Unity.Entities;
using Wetstone.API;

namespace VRisingExpanded.Objects
{
    public class Inventory
    {
        private static GameDataSystem gameData = VWorld.Server.GetExistingSystem<GameDataSystem>();

        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="targe">添加到那个实体</param>
        /// <param name="guid">guid</param>
        /// <param name="itemStacks">数量</param>
        /// <returns>添加的物品的实体</returns>
        public static Entity AddItem(Entity target, PrefabGUID guid, int itemStacks)
        {
            unsafe
            {
                var bytes = stackalloc byte[Marshal.SizeOf<FakeNull>()];
                var bytePtr = new IntPtr(bytes);
                Marshal.StructureToPtr<FakeNull>(new()
                {
                    value = 7,
                    has_value = true
                }, bytePtr, false);
                var boxedBytePtr = IntPtr.Subtract(bytePtr, 0x10);

                Il2CppSystem.Nullable<int> hack;
                hack = new Il2CppSystem.Nullable<int>(boxedBytePtr);

                InventoryUtilitiesServer.TryAddItem(VWorld.Server.EntityManager,
                                                    gameData.ItemHashLookupMap,
                                                    target,
                                                    guid,
                                                    itemStacks,
                                                    out _,
                                                    out Entity e,
                                                    default,
                                                    hack);
                return e;
            }
        }

        /// <summary>
        /// 清除背包
        /// </summary>
        public static void Clear(Entity target, EntityManager entityManager)
        {
            InventoryUtilities.TryGetInventoryEntity(entityManager, target, out Entity playerInventory);

            for (int i = 9; i < InventoryUtilities.GetItemSlots(entityManager, playerInventory); i++)
            {
                InventoryUtilitiesServer.ClearSlot(VWorld.Server.EntityManager, playerInventory, i);
            }
        }

        public static bool RemoveItem(Entity target, PrefabGUID guid, int stacks = 1)
        {
            var buffer = VWorld.Server.GetExistingSystem<EntityCommandBufferSystem>().CreateCommandBuffer();
            var IsRemove = InventoryUtilitiesServer.TryRemoveItem(VWorld.Server.EntityManager, target, guid, stacks);
            return IsRemove;
        }

        private struct FakeNull
        {
            public int value;
            public bool has_value;
        }
    }
}