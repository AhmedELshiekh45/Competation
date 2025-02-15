using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess_Layer.Base;

namespace DataAccess_Layer.Models
{
    public class Exam : BaseClass
    {
        [ForeignKey("Contestant")]
        public string ContestantId { get; set; }
        public virtual Contestant? Contestant { get; set; }

        [ForeignKey("Competition")]
        public string CompetitionId { get; set; }

        public float PartsNumber { get; set; }

        public int SerialNumber { get; set; }
        public virtual Competition? Competition { get; set; }
        [ForeignKey("Teacher")]
        public string? TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }

        [ForeignKey("Entry")]
        public string? EntryId { get; set; }
        public virtual User? Entry { get; set; }

        [Range(1, 100)]
        public float TotalScore { get; set; }

    }




}

