using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace AdjustableAge
{
    public class AgeSettings : ModSettings
    {
        public IntRange allowedAges = new IntRange(15, 80);


        /// <summary>
        /// The part that writes our settings to file. Note that saving is by ref.
        /// </summary>
        public override void ExposeData()
        {
            Scribe_Values.Look(ref allowedAges, "allowedAges", new IntRange(15, 80));
        }
    }


    public class AgeMod : Mod
    {
        public AgeSettings settings;

        public AgeMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<AgeSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {

            //inRect.width = 450f;
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("ZAA_ageLabel".Translate());

            Rect ageRangeRect = listingStandard.GetRect(35);
            Widgets.IntRange(ageRangeRect, 130, ref settings.allowedAges, 0, 150);

            listingStandard.Gap();
            if (settings.allowedAges.min < 13)
            {
                GUI.color = new Color(255, 180, 0);
                listingStandard.Label("ZAA_ageWarningAdult".Translate());
                GUI.color = Color.white;
            }
            if (settings.allowedAges.min < 3)
            {
                listingStandard.Gap();
                GUI.color = new Color(255, 180, 0);
                listingStandard.Label("ZAA_ageWarningBaby".Translate());
                GUI.color = Color.white;
            }

            listingStandard.End();

            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "ZAA_ModName".Translate();
        }

    }




}