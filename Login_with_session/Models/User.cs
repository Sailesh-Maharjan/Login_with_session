using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Login_with_session.Models;

public partial class User
{
    public string Name { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public int Id { get; set; }
}
