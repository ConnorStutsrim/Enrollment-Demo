using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enrollment.Models
{
    public class Beneficiary
    {
        [Key]
        public int BeneficiaryID { get; set; }

        public int PersonID { get; set; }
        public virtual Person Person { get; set; }

        [Display(Name = "Relationship")]
        [ForeignKey("RelationshipType")]
        public int RelationshipTypeID { get; set; }
        public virtual RelationshipType RelationshipType { get; set; }

        [ForeignKey("Participant")]
        public int ParticipantID { get; set; }
        public virtual Participant Participant { get; set; }
    }
}