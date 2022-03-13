using System;
using System.Collections.Generic;

namespace healthcare_visuzlier25.Models
{
    public partial class DrugReport
    {
        public Guid Id { get; set; }
        public bool DrugDiscontinued { get; set; }
        public string DrugCharacterization { get; set; } = null!;
        public string? BatchNumber { get; set; }
        public int? DosageAmount { get; set; }
        public string? DosageAmountUnit { get; set; }
        public int? NumSepatateDoses { get; set; }
        public int? NumUnitsPerInterval { get; set; }
        public string? IntervalType { get; set; }
        public int? CumulativeDoesTillFirstReaction { get; set; }
        public string? DosageText { get; set; }
        public int? GestationPeriodAtExposure { get; set; }
        public string? GestationPeriodAtExposureUnit { get; set; }
        public string? MeddraVersion { get; set; }
        public string? IndicationForUseInCase { get; set; }
        public DateOnly DrugAdminstrationStartDate { get; set; }
        public DateOnly DrugAdminstrationEndDate { get; set; }
        public TimeSpan? TimeIntervalFromFirstDoseTillReaction { get; set; }
        public TimeSpan? TimeIntervalFromLastDoseTillReaction { get; set; }
        public TimeSpan? DurationOfDrugAdminstration23 { get; set; }
        public string? ActionTakenWithDrug { get; set; }
        public bool? ReactionReoccurOnAdminstration { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
