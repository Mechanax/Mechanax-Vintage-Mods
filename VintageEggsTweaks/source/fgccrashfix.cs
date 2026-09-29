using HarmonyLib;
using vintageeggstweaks.source;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace VintageEggsTweaks.source
{
    [HarmonyPatchCategory("vintageeggstweaks")]
    public class FGCCrashFix
    {
        //Add null checks to Crop Break push event
        [HarmonyPatch("FromGoldenCombs.BlockBehaviors.PushEventOnCropBreakBehavior", "OnBlockBroken")]
        internal static class Patch_PushEventOnCropBreakBehavior_OnBlockBroken
        {
            public static void Postfix(object __instance, IWorldAccessor world, BlockPos pos, IPlayer byPlayer, ref EnumHandling handling)
            {
                try
                {
                    if (world == null || pos == null)
                    {
                        return;
                    }

                    dynamic? instance = __instance;
                    
                    string? eventName = null;
                    try
                    {
                        eventName = instance._eventName;
                    }
                    catch
                    {
                        return;
                    }

                    if (byPlayer != null && !string.IsNullOrEmpty(eventName))
                    {
                        TreeAttribute tree = new();
                        tree.SetInt("x", pos.X);
                        tree.SetInt("y", pos.Y);
                        tree.SetInt("z", pos.Z);
                        world.Api.Event.PushEvent(eventName, tree);
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCEventOnCropBreakPatch] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        //Add null checks for boosts from Ceramic Brood Pot
        [HarmonyPatch("FromGoldenCombs.BlockEntities.BECeramicBroodPot", "manageCropBoost")]
        internal static class Patch_BECeramicBroodPot_manageCropBoost
        {
            public static bool Prefix(object __instance, BlockPos cropPos)
            {
                try
                {
                    dynamic instance = __instance;
                    
                    // Get world accessor from Api property instead
                    IWorldAccessor? world = instance.Api?.World;
                    
                    if (cropPos == null || world == null)
                    {
                        return false;
                    }

                    Block cropBlock = world.BlockAccessor.GetBlock(cropPos);

                    if (cropBlock == null || cropBlock.Code.Path == "air")
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCCermicCropB] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
                return true;
            }
        }

        [HarmonyPatch("FromGoldenCombs.BlockEntities.BECeramicBroodPot", "manageBerryBoost")]
        internal static class Patch_BECeramicBroodPot_manageBerryBoost
        {
            public static bool Prefix(object __instance, BlockPos bushPos)
            {
                try
                {
                    dynamic instance = __instance;
                    IWorldAccessor? world = instance.Api?.World;

                    if (bushPos == null || world == null)
                    {
                        return false;
                    }

                    Block bushBlock = world.BlockAccessor.GetBlock(bushPos);

                    if (bushBlock == null || bushBlock.Code.Path == "air")
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCCermicBerryB] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
                return true;
            }
        }

        [HarmonyPatch("FromGoldenCombs.BlockEntities.BECeramicBroodPot", "manageFruitBoost")]
        internal static class Patch_BECeramicBroodPot_manageFruitBoost
        {
            public static bool Prefix(object __instance, BlockPos fruitFoliagePos)
            {
                try
                {
                    dynamic instance = __instance;
                    IWorldAccessor? world = instance.Api?.World;

                    if (fruitFoliagePos == null || world == null)
                    {
                        return false;
                    }

                    Block fruitFoliagePosBlock = world.BlockAccessor.GetBlock(fruitFoliagePos);

                    if (fruitFoliagePosBlock == null || fruitFoliagePosBlock.Code.Path == "air")
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCCermicFruitB] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
                return true;
            }
        }

        //Add null checks for boosts from Langstroth Stack
        [HarmonyPatch("FromGoldenCombs.BlockEntities.BELangstrothStack", "manageCropBoost")]
        internal static class Patch_BELangstrothStack_manageCropBoost
        {
            public static bool Prefix(object __instance, BlockPos cropPos)
            {
                try
                {
                    dynamic instance = __instance;
                    IWorldAccessor? world = instance.Api?.World;

                    if (cropPos == null || world == null)
                    {
                        return false;
                    }

                    Block cropBlock = world.BlockAccessor.GetBlock(cropPos);

                    if (cropBlock == null || cropBlock.Code.Path == "air")
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCLangstrothCropB] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
                return true;
            }
        }

        [HarmonyPatch("FromGoldenCombs.BlockEntities.BELangstrothStack", "manageBerryBoost")]
        internal static class Patch_BELangstrothStack_manageBerryBoost
        {
            public static bool Prefix(object __instance, BlockPos bushPos)
            {
                try
                {
                    dynamic instance = __instance;
                    IWorldAccessor? world = instance.Api?.World;

                    if (bushPos == null || world == null)
                    {
                        return false;
                    }

                    Block bushBlock = world.BlockAccessor.GetBlock(bushPos);

                    if (bushBlock == null || bushBlock.Code.Path == "air")
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCLangstrothBerryB] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
                return true;
            }
        }

        [HarmonyPatch("FromGoldenCombs.BlockEntities.BELangstrothStack", "manageFruitBoost")]
        internal static class Patch_BELangstrothStack_manageFruitBoost
        {
            public static bool Prefix(object __instance, BlockPos fruitFoliagePos)
            {
                try
                {
                    dynamic instance = __instance;
                    IWorldAccessor? world = instance.Api?.World;

                    if (fruitFoliagePos == null || world == null)
                    {
                        return false;
                    }

                    Block fruitFoliagePosBlock = world.BlockAccessor.GetBlock(fruitFoliagePos);

                    if (fruitFoliagePosBlock == null || fruitFoliagePosBlock.Code.Path == "air")
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCLangstrothFruitB] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
                return true;
            }
        }

        //Add null checks for boosts from Beehive
        [HarmonyPatch("FromGoldenCombs.BlockEntities.BEFGCBeehive", "manageCropBoost")]
        internal static class Patch_BEFGCBeehive_manageCropBoost
        {
            public static bool Prefix(object __instance, BlockPos cropPos)
            {
                try
                {
                    dynamic instance = __instance;
                    IWorldAccessor? world = instance.Api?.World;

                    if (cropPos == null || world == null)
                    {
                        return false;
                    }

                    Block cropBlock = world.BlockAccessor.GetBlock(cropPos);

                    if (cropBlock == null || cropBlock.Code.Path == "air")
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCBeehiveCropB] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
                return true;
            }
        }

        [HarmonyPatch("FromGoldenCombs.BlockEntities.BEFGCBeehive", "manageBerryBoost")]
        internal static class Patch_BEFGCBeehive_manageBerryBoost
        {
            public static bool Prefix(object __instance, BlockPos bushPos)
            {
                try
                {
                    dynamic instance = __instance;
                    IWorldAccessor? world = instance.Api?.World;

                    if (bushPos == null || world == null)
                    {
                        return false;
                    }

                    Block bushBlock = world.BlockAccessor.GetBlock(bushPos);

                    if (bushBlock == null || bushBlock.Code.Path == "air")
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCBeehiveBerryB] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
                return true;
            }
        }

        [HarmonyPatch("FromGoldenCombs.BlockEntities.BEFGCBeehive", "manageFruitBoost")]
        internal static class Patch_BEFGCBeehive_manageFruitBoost
        {
            public static bool Prefix(object __instance, BlockPos fruitFoliagePos)
            {
                try
                {
                    dynamic instance = __instance;
                    IWorldAccessor world = instance.Api?.World;

                    if (fruitFoliagePos == null || world == null)
                    {
                        return false;
                    }

                    Block fruitFoliagePosBlock = world.BlockAccessor.GetBlock(fruitFoliagePos);

                    if (fruitFoliagePosBlock == null || fruitFoliagePosBlock.Code.Path == "air")
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    VintageeggstweaksModSystem.Logger?.Error("[FGCBeehiveFruitB] Error:" + ex.Message + "\n" + ex.StackTrace);
                }
                return true;
            }
        }
    }
}
