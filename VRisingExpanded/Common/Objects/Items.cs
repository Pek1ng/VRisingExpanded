using ProjectM;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Entities;
using Wetstone.API;

namespace VRisingExpanded.Objects
{
    public class Items
    {
        public static PrefabGUID GetGUIDFromName(string name)
        {
            var gameDataSystem = VWorld.Server.GetExistingSystem<GameDataSystem>();
            var managed = gameDataSystem.ManagedDataRegistry;

            foreach (var entry in gameDataSystem.ItemHashLookupMap)
            {
                try
                {
                    var item = managed.GetOrDefault<ManagedItemData>(entry.Key);
                    if (item.PrefabName.StartsWith("Item_VBloodSource") || item.PrefabName.StartsWith("GM_Unit_Creature_Base") || item.PrefabName == "Item_Cloak_ShadowPriest") continue;
                    if (item.Name.ToString().ToLower() == name.ToLower())
                    {
                        return entry.Key;
                    }
                }
                catch { }
            }

            return new PrefabGUID(0);
        }

        public static void DumpItems()
        {
            try
            {
                World world = GlobalData.CurrentWorld;

                var entityManager = world.EntityManager;
                var gameData = world.GetExistingSystem<ProjectM.GameDataSystem>();
                var managed = gameData.ManagedDataRegistry;

                foreach (var entry in gameData.ItemHashLookupMap)
                {
                    try
                    {
                        var guid = entry.Key;
                        var itemData = managed.GetOrDefault<ManagedItemData>(guid);

                        if (itemData != null)
                        {
                            GlobalData.Logger.LogInfo($"{entry.Key.GuidHash}  |  {itemData.PrefabName}  |  {itemData.Name}");
                        }
                    }
                    catch (Exception e)
                    {
                        GlobalData.Logger.LogError(e);
                    }
                }
            }
            catch (Exception e)
            {
                GlobalData.Logger.LogError(e);
            }
        }
    }
}