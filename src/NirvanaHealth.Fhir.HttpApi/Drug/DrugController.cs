using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NirvanaHealth.Fhir.Drug;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace NirvanaHealth.Fhir.Controllers;

[ControllerName("Drug")]
[Route("api/app/drug")]
public class DrugController : AbpController
{
 


    [HttpGet]
    public async Task<List<DrugCoveragePlanDto>> GetAllPlanCoverages(string tierName)
    {
        var coveragePlans = new List<DrugCoveragePlanDto>();
        var filetredPlans = new List<DrugCoveragePlanDto>();
        var filePath = "JsonFiles/drug.json";

        if (System.IO.File.Exists(filePath))
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                coveragePlans = JsonConvert.DeserializeObject<List<DrugCoveragePlanDto>>(json);
            }
        }

        if (string.IsNullOrEmpty(tierName))
        {
            filetredPlans = coveragePlans;
        }
        else
        {
            foreach (var plan in coveragePlans)
            {
                DrugCoveragePlanDto temp = null;
                if(plan.Tiers != null && plan.Tiers.Any(x => x.Name == tierName))
                {
                    temp = new DrugCoveragePlanDto() { Name = plan.Name, Tiers = new List<TierDto>()};
                }


                foreach (var tier in plan.Tiers)
                {
                    if (tier.Name == tierName)
                    {
                        if (!temp.Tiers.Any(x => x.Name == tier.Name))
                        {
                            temp.Tiers.Add(tier);
                        }
                    }
                }

                if(temp != null)
                {
                    filetredPlans.Add(temp);
                }

            }

        }
        return filetredPlans;
    }

    [Route("searchDrug")]
    [HttpGet]
    public async Task<List<DrugDetailDto>> SearchDrug(string DrugName,string coveragePlan)
    {
        var coveragePlans = new List<DrugCoveragePlanDto>();
        var result = new List<DrugDetailDto>();
        var filePath = "JsonFiles/drug.json";

        if (System.IO.File.Exists(filePath))
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                coveragePlans = JsonConvert.DeserializeObject<List<DrugCoveragePlanDto>>(json);
            }
        }

        coveragePlans = coveragePlans.Where(x => string.IsNullOrEmpty(coveragePlan) || x.Name == coveragePlan).ToList();
        foreach (var plan in coveragePlans)
        {
            foreach (var tier in plan.Tiers)
            {
                foreach (var drug in tier.Drugs)
                {
                    if (drug.Name.ToLower().Contains(DrugName.ToLower()))
                    {
                        if (!result.Any(x => x.DrugName == drug.Name))
                        {
                            var association = new List<DrugAssociationDto>();
                            association.Add(new DrugAssociationDto() { 
                                TierName = tier.Name, 
                                CoveragePlanName = plan.Name,
                                PA = drug.PA,
                                ST = drug.ST,
                                QL = drug.QL
                            });
                            result.Add(new DrugDetailDto { DrugName = drug.Name , DrugAssociations = association });
                        }
                        else
                        {
                            if(!result.First(x => x.DrugName == drug.Name).DrugAssociations.Any(x => x.CoveragePlanName == plan.Name && x.TierName == tier.Name)){
                                result.First(x => x.DrugName == drug.Name).DrugAssociations.Add(new DrugAssociationDto()
                                {
                                    CoveragePlanName = plan.Name,
                                    TierName = tier.Name,
                                    PA = drug.PA,
                                    ST = drug.ST,
                                    QL = drug.QL
                                });
                            }
                        }
                    }
                }
            }
        }

        return result;
    }

    [Route("drugsByTier")]
    [HttpGet]
    public async Task<List<DrugDto>> GetDrugsByTier(string coveragePlan, string tierName)
    {
        var coveragePlans = new List<DrugCoveragePlanDto>();
        var result = new List<DrugDto>();
        var filePath = "JsonFiles/drug.json";

        if (System.IO.File.Exists(filePath))
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                coveragePlans = JsonConvert.DeserializeObject<List<DrugCoveragePlanDto>>(json);
            }
        }

        var selectedPlan = coveragePlans.
                        FirstOrDefault(x =>  x.Name == coveragePlan);

        if(selectedPlan != null){
            var selectedTier = selectedPlan.Tiers.FirstOrDefault(x => x.Name == tierName);
            if(selectedTier != null)
            {
                result = selectedTier.Drugs;
            }
        }
                        
        
        return result;
    }
}
