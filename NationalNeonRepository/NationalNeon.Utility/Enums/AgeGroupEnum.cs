
using System;
using System.ComponentModel.DataAnnotations;

namespace NationalNeon.Utility.Enums
{
    public enum AgeGroupEnum
    {
        [Display(Name = "18 - 24 Years")]
        EighteentoTwentyFourYears = 1,
        [Display(Name = "25 - 34 Years")]
        TwentyFivetoThirtyFour,
        [Display(Name = "35 - 49 Years")]
        ThirtyFivetoFourtyNine,
        [Display(Name = "Over 50 Years")]
        OverFiftyYears
    }
}
