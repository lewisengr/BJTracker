using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BJTracker.Models;

public class Session
{
    public int Id { get; set; }
    [Required]
    public IdentityUser User { get; set; }
    public string UserId { get; set; }
    public string Casino { get; set; }
    public DateOnly Date { get; set; }
    public decimal Result { get; set; }

}
