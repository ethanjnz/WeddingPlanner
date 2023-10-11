#pragma warning disable CS8618
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models;

public class Decision
{
    [Key]
    public int DecisionId { get; set; }


    //fk
    public int UserId { get; set; }

    // NAV PROP
    public User? DecisionMaker { get; set; }

    //FK
    public int WeddingId { get; set; }
    public Wedding? Wedding { get; set; }



}
