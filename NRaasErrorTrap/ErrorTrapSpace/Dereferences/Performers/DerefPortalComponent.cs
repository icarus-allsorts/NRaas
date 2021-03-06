﻿using NRaas.CommonSpace.Booters;
using NRaas.CommonSpace.Helpers;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.ObjectComponents;
using Sims3.Gameplay.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NRaas.ErrorTrapSpace.Dereferences
{
    public class DerefPortalComponent : Dereference<PortalComponent>
    {
        protected override DereferenceResult Perform(PortalComponent reference, FieldInfo field, List<ReferenceWrapper> objects)
        {
            if (Matches(reference, "mLockedLaneIndices", field, objects))
            {
                RemoveKeys(reference.mLockedLaneIndices, objects);
                return DereferenceResult.End;
            }

            return DereferenceResult.Failure;
        }
    }
}
