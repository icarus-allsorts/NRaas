﻿using NRaas.CommonSpace.Options;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRaas.DresserSpace.Options.Settings.CheckOutfits.Hair.RandomBodyHair
{
    public class RandomFemaleBodyHairChanceSetting : IntegerSettingOption<GameObject>, IRandomBodyHairOption
    {
        protected override int Value
        {
            get
            {
                return Dresser.Settings.mRandomBodyHair.mFemaleChance;
            }
            set
            {
                Dresser.Settings.mRandomBodyHair.mFemaleChance = value;
            }
        }

        public override string GetTitlePrefix()
        {
            return "FemaleChance";
        }

        public override ITitlePrefixOption ParentListingOption
        {
            get { return new ListingOption(); }
        }
    }
}
