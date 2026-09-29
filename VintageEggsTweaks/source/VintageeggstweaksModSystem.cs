using HarmonyLib;
using System.Reflection;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace vintageeggstweaks.source
{
    public class VintageeggstweaksModSystem : ModSystem
    {
        private static Harmony? harmony;
        internal static ILogger? Logger { get; private set; }
        public static ICoreAPI? Api { get; private set; }

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            Api = api;
            if (Harmony.HasAnyPatches(Mod.Info.ModID))
            {
                api.Logger.Warning("[VintageEggs] Patches already applied, skipping");
                return;
            }
            harmony = new Harmony(Mod.Info.ModID);
            api.Logger.Notification("[VintageEggs] Harmony instance created");
            harmony.PatchCategory(Mod.Info.ModID);
        }

        public override void AssetsFinalize(ICoreAPI api)
        {
            api.Logger.Notification("[VintageEggs] AssetsFinalize() called");
            base.AssetsFinalize(api);
            api.Logger.Notification($"[VintageEggs] Harmony instance null check: {harmony == null}");
            if (harmony == null)
            {
                api.Logger.Error("[VintageEggs] Harmony instance is null, cannot apply patches");
                return;
            }
            try
            {
                api.Logger.Notification("[VintageEggs] Applying patches after assets finalized...");
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                harmony.PatchAll(executingAssembly);
                IEnumerable<MethodBase> patchedMethods = harmony.GetPatchedMethods();
                int num = 0;
                foreach (MethodBase item in patchedMethods)
                {
                    num++;
                    api.Logger.Debug("[VintageEggs] Patched: " + item.DeclaringType?.Name + "." + item.Name);
                }
                api.Logger.Notification($"[VintageEggs] Successfully applied {num} patches");
            }
            catch (Exception ex)
            {
                api.Logger.Error("[VintageEggs] Failed to apply patches: " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            ((ICoreAPI)api).Logger.Notification("[VintageEggs] Server side init complete");
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);
            ((ICoreAPI)api).Logger.Notification("[VintageEggs] Client side init complete");
        }

        public override void Dispose()
        {
            harmony?.UnpatchAll(Mod.Info.ModID);
        }
    }
}
