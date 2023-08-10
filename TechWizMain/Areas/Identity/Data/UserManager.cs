using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TechWizMain.Models;

namespace TechWizMain.Areas.Identity.Data;

// Add profile data for application users by adding properties to the UserManager class
[Table("Users")]
public class UserManager : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string? Address { get; set; }

    public bool status { get; set; } = true;
    public DateTime? DateOfBirth { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Bill> Bills { get; set; }
    [InverseProperty("User")]
    public virtual ICollection<Feedback> Feedbacks { get; set; }
    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; }





}

