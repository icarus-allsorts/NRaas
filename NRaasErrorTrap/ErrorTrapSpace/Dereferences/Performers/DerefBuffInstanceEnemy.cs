﻿using NRaas.CommonSpace.Booters;
using NRaas.CommonSpace.Helpers;
using Sims3.Gameplay.Abstracts;
using Sims3.Gameplay.ActorSystems;
using Sims3.Gameplay.Interfaces;
using Sims3.Gameplay.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NRaas.ErrorTrapSpace.Dereferences
{
    public class DerefBuffInstanceEnemy : Dereference<BuffEnemy.BuffInstanceEnemy>
    {
        protected override DereferenceResult Perform(BuffEnemy.BuffInstanceEnemy reference, FieldInfo field, List<ReferenceWrapper> objects)
        {
            if (Matches(reference, "EnemySim", field, objects))
            {
                Remove(ref reference.EnemySim);
                return DereferenceResult.Continue;
            }

            return DereferenceResult.Failure;
        }
    }
}
