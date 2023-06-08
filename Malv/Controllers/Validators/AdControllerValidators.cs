using Malv.Controllers.Exceptions;
using Malv.Models;
using Newtonsoft.Json;

namespace Malv.Controllers.Validators;

public static class AdControllerValidators
{
    public static CarAd_Mod ValidateAdModel(this string? carAdModJson)
    {
        if (carAdModJson == null)
            throw new BusinessMalvException(new Error_Res().AddError("Invalid json content", ""));

        CarAd_Mod? carAdMod = JsonConvert.DeserializeObject<CarAd_Mod>(carAdModJson);

        if (carAdMod == null)
            throw new BusinessMalvException(new Error_Res().AddError("Invalid json content", ""));

        return carAdMod;
    }
}