﻿using NRaas.CommonSpace.Options;
using NRaas.WoohooerSpace.Skills;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.Autonomy;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.Interactions;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using System;
using System.Collections.Generic;
using System.Text;

namespace NRaas.WoohooerSpace.Options.KamaSimtra.MinNotches
{
#if DISABLED
    public class MilleniumMinNotchesSetting : IntegerSettingOption<GameObject>, IMinNotchesOption
    {
        protected override int Value
        {
            get
            {
                return Skills.KamaSimtra.Settings.mMilleniumMinNotches;
            }
            set
            {
                Skills.KamaSimtra.Settings.mMilleniumMinNotches = value;
            }
        }

        public override string GetTitlePrefix()
        {
            return "MilleniumMinNotches";
        }

        public override ITitlePrefixOption ParentListingOption
        {
            get { return new ListingOption(); }
        }
    }
#endif
}
