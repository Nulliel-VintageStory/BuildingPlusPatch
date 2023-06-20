using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;
using VSMods;

static class BuildingPlus {
    [HarmonyPatch]
    static class PatchBuildingPlus {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(BalconyStair), "GetWindowCodeHor")]
        public static bool BalconyStair_GetWindowCodeHor_Prefix(ref string __result, IWorldAccessor world, BlockPos pos, BlockFacing facing) {
            var block = world.BlockAccessor.GetBlock(pos.AddCopy(facing));
            var shouldConnect = block is BalconyStair || (block.SideSolid[facing.Opposite.Index] && !(block.CodeWithoutParts(1) == "woodencrate"));

            __result = "";

            if (shouldConnect) {
                __result = string.Concat(facing.Code[0]);
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BalconyPlatform), "GetWindowCode")]
        public static bool BalconyPlatform_GetWindowCode_Prefix(ref string __result, IWorldAccessor world, BlockPos pos, BlockFacing facing) {
            var block = world.BlockAccessor.GetBlock(pos.AddCopy(facing));
            Block selfblock = world.BlockAccessor.GetBlock(pos);
            BlockPos lowerstair = pos.AddCopy(0, -1, 0);
            Block iflowerstair = world.BlockAccessor.GetBlock(lowerstair.AddCopy(facing));
            string othercode = block.FirstCodePart(0);
            var shouldConnect = (block is BlockTrapdoor && (selfblock.Variant["el"] == "u" || selfblock.Variant["el"] == "rd")) || block is BlockDoor || block is BalconyPlatform || (block.SideSolid[facing.Opposite.Index] && !(block.CodeWithoutParts(1) == "woodencrate")) || othercode == "ladder" || block is BalconyStair || iflowerstair is BalconyStair;

            __result = "";

            if (shouldConnect) {
                __result = string.Concat(facing.Code[0]);
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BalconyPlatform), "GetWindowCodeVert")]
        public static bool BalconyPlatform_GetWindowCodeVert_Prefix(ref string __result, IWorldAccessor world, BlockPos pos, BlockFacing facing) {
            var block = world.BlockAccessor.GetBlock(pos.AddCopy(facing));
            var shouldConnect = block is BalconyPlatform || block is SixDimFence || block.SideSolid[facing.Opposite.Index];

            __result = "";

            if (shouldConnect) {
                __result = string.Concat(facing.Code[0]);
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Multirooftop), "GetWindowCode")]
        public static bool Multirooftop_GetWindowCode_Prefix(ref string __result, IWorldAccessor world, BlockPos pos, BlockFacing facing) {
            var block = world.BlockAccessor.GetBlock(pos.AddCopy(facing));
            var shouldConnect = block is Multiroof || block.SideSolid[facing.Opposite.Index] || block is Multirooftop || block is Roofrailingfront;

            __result = "";

            if (shouldConnect) {
                __result = string.Concat(facing.Code[0]);
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SixDimFence), "GetWindowCode")]
        public static bool SixDimFence_GetWindowCode_Prefix(ref string __result, IWorldAccessor world, BlockPos pos, BlockFacing facing) {
            var block = world.BlockAccessor.GetBlock(pos.AddCopy(facing));
            var selfblock = world.BlockAccessor.GetBlock(pos);

            bool shouldConnect;

            if (selfblock.CodeWithoutParts(4) == "supportfence1" || selfblock.CodeWithoutParts(4) == "supportfence_deb1") {
                shouldConnect = block is SixDimFence;
            } else {
                shouldConnect = block is SixDimFence || (block.SideSolid[facing.Opposite.Index] && !(block.CodeWithoutParts(1) == "woodencrate"));
            }

            __result = "";

            if (shouldConnect) {
                __result = string.Concat(facing.Code[0]);
            }

            return false;
        }
    }
}
