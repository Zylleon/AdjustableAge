using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AdjustableAge
{
    [StaticConstructorOnStartup]
    static class Patches
    {
        static Patches()
        {
            Harmony harmony = new Harmony("zylle.AdjustableAge");
            harmony.PatchAll();
            Log.Message("Adjustable Age initialized");
        }
    }


    [HarmonyPatch(typeof(Verse.PawnGenerator), "GenerateRandomAge")]
    static class AgePatch
    {
        static bool Prefix(Pawn pawn, ref PawnGenerationRequest request)
        {

            if (pawn.RaceProps.Humanlike && !request.AllowedDevelopmentalStages.Newborn())
            {
                IntRange ages = LoadedModManager.GetMod<AgeMod>().GetSettings<AgeSettings>().allowedAges;

                request.FixedBiologicalAge = Rand.Range(ages.min, ages.max);

            }

            return true;
        }
       
    }

}
