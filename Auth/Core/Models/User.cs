using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

[Table(nameof(User))]
public sealed class User : IdentityUser<int>
{
}