using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;

namespace xskillmmleaves
{
    public sealed class xskillmmleavesModSystem : ModSystem
    {
        private static Harmony? _harmony;
        internal static ILogger? Logger { get; private set; }

        // Called on server and client
        // Useful for registering block/entity classes on both sides
        public override void Start(ICoreAPI api)
        {
            Logger = Mod.Logger;
            Mod.Logger.Notification("XSkills? more like x skill issue");
            if (Harmony.HasAnyPatches(Mod.Info.ModID)) return;
            _harmony = new Harmony(Mod.Info.ModID);
            _harmony.PatchCategory(Mod.Info.ModID);
        }

        public override void Dispose()
        {
            _harmony?.UnpatchAll(Mod.Info.ModID);
        }

    }
}