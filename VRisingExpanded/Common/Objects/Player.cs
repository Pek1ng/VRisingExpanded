using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Wetstone.API;
using Wetstone.Hooks;
using System.Linq;

namespace VRisingExpanded.Objects
{
    public static class Player
    {
        public static List<ProjectM.Network.User> GetPlayerList(VChatEvent ev)
        {
            var userQuery = VWorld.Server.EntityManager.CreateEntityQuery(ComponentType.ReadOnly<ProjectM.Network.User>());
            var users = userQuery.ToEntityArray(Allocator.Temp).ToArray().Select(u => VWorld.Server.EntityManager.GetComponentData<ProjectM.Network.User>(u)).ToList();

            return users;
        }
    }
}