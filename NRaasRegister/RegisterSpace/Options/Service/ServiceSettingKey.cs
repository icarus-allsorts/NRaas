﻿using NRaas.CommonSpace.Helpers;
using NRaas.RegisterSpace.Helpers;
using Sims3.Gameplay.Services;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.SimIFace.CAS;
using Sims3.SimIFace.Enums;
using System;
using System.Collections.Generic;

namespace NRaas.RegisterSpace.Options.Service
{
    [Persistable]
    public class ServiceSettingKey : IPersistence
    {
        public ServiceType type;
        public CASAgeGenderFlags validAges;
        public int numInPool = 0;
        public int cost = 0;
        public bool reoccuring = false;
        public bool useBots = false;

        [Persistable(false)]
        public Dictionary<int, string[]> uniforms;

        [Persistable(false)]
        public ServiceSettingKey tuningDefault = null;
       
        public ServiceSettingKey()
        { }
        public ServiceSettingKey(Sims3.Gameplay.Services.Service service)
        {
            type = service.ServiceType;            
            validAges = Sims3.Gameplay.Services.ServiceNPCSpecifications.GetAppropriateAges(service.ServiceType.ToString());
            numInPool = service.Tuning.kMaxNumNPCsInPool;
            cost = service.Tuning.kCost;
            reoccuring = service.Tuning.kIsRecurrent;
            useBots = Sims3.Gameplay.Services.ServiceNPCSpecifications.ShouldUseServobot(service.ServiceType.ToString());

            ServiceNPCSpecifications.ServiceSpecifications spec;
            if (ServiceNPCSpecifications.sServiceSpecifications.TryGetValue(service.ServiceType.ToString(), out spec))
            {
                uniforms = spec.Uniforms;
            }
        }
        public ServiceSettingKey(Sims3.Gameplay.Services.Service service, CASAgeGenderFlags flags, int poolSetting, int serviceCost, bool reoccur, bool bots)
        {
            type = service.ServiceType;
            validAges |= flags;
            poolSetting = numInPool;
            cost = serviceCost;
            reoccuring = reoccur;
            useBots = bots;            

            tuningDefault = new ServiceSettingKey(service);
        }

        public bool ValidAge(CASAgeGenderFlags ages)
        {
            return (ages & validAges) != CASAgeGenderFlags.None;
        }

        public List<CASAgeGenderFlags> AgeSpeciesToList()
        {
            List<CASAgeGenderFlags> results = new List<CASAgeGenderFlags>();            

            foreach (CASAgeGenderFlags ageSpecies in Enum.GetValues(typeof(CASAgeGenderFlags)))
            {
                if ((this.validAges & ageSpecies) == ageSpecies)
                {
                    if (ageSpecies != CASAgeGenderFlags.None)
                    {
                        // I'm not sure why this is needed but it throws the list count off otherwise
                        results.Add(ageSpecies);
                    }
                }
            }

            return results;
        }

        public void SetSettings(Sims3.Gameplay.Services.Service service)
        {
            if (Register.Settings.serviceSettings.ContainsKey(service.ServiceType))
            {
                Register.Settings.serviceSettings[service.ServiceType] = this;
            }
            else
            {
                this.tuningDefault = new ServiceSettingKey(service);
                Register.Settings.serviceSettings.Add(service.ServiceType, this);
            }

            Register.InitDefaultServiceTunings();
            ServiceCleanup.Task.Perform();
            ServicePoolCleanup.Task.Perform();
        }

        public void Import(Persistence.Lookup settings)
        {            
            // unfortunately no easy way to get defaults for these..            
            if (settings.GetEnum<ServiceType>("ServiceType", ServiceType.None) != ServiceType.None)
            {
                type = settings.GetEnum<ServiceType>("ServiceType", ServiceType.None);

                if (settings.Exists("ValidAges"))
                {
                    string[] ages = settings.GetStringList("ValidAges");
                    validAges = CASAgeGenderFlags.None;
                    foreach (string age in ages)
                    {
                        CASAgeGenderFlags flag;
                        if (ParserFunctions.TryParseEnum<CASAgeGenderFlags>(age, out flag, CASAgeGenderFlags.None))
                        {                            
                            validAges |= flag;
                        }
                    }
                }

                if (settings.Exists("Reoccuring"))
                {                    
                    reoccuring = settings.GetBool("Reoccuring", false);
                }

                if (settings.Exists("PoolSize"))
                {
                    numInPool = settings.GetInt("PoolSize", 2);
                }

                if (settings.Exists("Cost"))
                {                    
                    cost = settings.GetInt("Cost", 0);
                }

                // Unfortunately EA has this hard coded so using these settings in a base world wouldn't work
                if (settings.Exists("UseBots") && GameUtils.GetCurrentWorld() == WorldName.FutureWorld)
                {
                    useBots = settings.GetBool("UseBots", false);
                }                
            }
        }

        public void Export(Persistence.Lookup settings)
        {
            settings.Add("ServiceType", type.ToString());

            List<CASAgeGenderFlags> ages = AgeSpeciesToList();
            List<string> agesString = new List<string>();
            foreach (CASAgeGenderFlags ageSpecies in ages)
            {
                agesString.Add(ageSpecies.ToString());
            }

            if (agesString.Count > 0)
            {
                settings.Add("ValidAges", String.Join(",", agesString.ToArray()));
            }

            settings.Add("Reoccuring", reoccuring);

            settings.Add("PoolSize", numInPool);

            settings.Add("Cost", cost);

            settings.Add("UseBots", useBots);
        }

        public string PersistencePrefix
        {
            get { return null; }
        }

        public override string ToString()
        {
            return ToXMLString(null);
        }

        public string ToXMLString(ServiceSettingKey key)
        {
            string result = null;

            result += "  <Service>";

            result += Common.NewLine + "    <Name>" + type.ToString() + "</Name>";

            List<CASAgeGenderFlags> ages = AgeSpeciesToList();
            List<string> agesString = new List<string>();
            foreach (CASAgeGenderFlags ageSpecies in ages)
            {
                agesString.Add(ageSpecies.ToString());
            }

            if (agesString.Count > 0)
            {
                result += Common.NewLine + "    <ValidAges>" + String.Join(",", agesString.ToArray()) + "</ValidAges>";                
            }

            result += Common.NewLine + "    <Reoccuring>" + reoccuring + "</Reoccuring>";

            result += Common.NewLine + "    <PoolSize>" + numInPool + "</PoolSize>";

            result += Common.NewLine + "    <Cost>" + cost + "</Cost>";

            result += Common.NewLine + "    <useBots>" + useBots + "</useBots>";            

            result += Common.NewLine + "  </Service>";

            return result;
        }
    }
}
