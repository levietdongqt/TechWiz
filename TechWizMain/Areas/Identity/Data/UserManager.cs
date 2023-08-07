using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TechWizMain.Areas.Identity.Data;

// Add profile data for application users by adding properties to the UserManager class
public class UserManager : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string UserName { get; set; }

    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}

