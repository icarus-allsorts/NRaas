using NRaas.CommonSpace.Options;
using NRaas.StoryProgressionSpace.Booters;
using NRaas.StoryProgressionSpace.Helpers;
using NRaas.StoryProgressionSpace.Interfaces;
using NRaas.StoryProgressionSpace.Managers;
using NRaas.StoryProgressionSpace.Options;
using NRaas.StoryProgressionSpace.Scenarios.Lots;
using Sims3.Gameplay.Actors;
using Sims3.Gameplay.ActorSystems;
using Sims3.Gameplay.CAS;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.UI;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using System;
using System.Collections.Generic;

namespace NRaas.StoryProgressionSpace.Options.Immigration
{
    public class CustomNamesOnlyOption<TManager> : BooleanBaseManagerOptionItem<TManager>, ISimFromBinOption<TManager>
        where TManager : StoryProgressionObject, ISimFromBinManager
    {
        SimFromBinController mController;

        public CustomNamesOnlyOption()
            : base(false)
        { }

        public override string GetTitlePrefix()
        {
            return "CustomNamesOnly";
        }

        public SimFromBinController Controller
        {
            set { mController = value; }
        }

        public override bool ShouldDisplay()
        {
            //if (!mController.ShouldDisplayImmigrantOptions()) return false;

            if (!FirstNameListBooter.HasCustomNames()) return false;

            return base.ShouldDisplay();
        }
    }
}

