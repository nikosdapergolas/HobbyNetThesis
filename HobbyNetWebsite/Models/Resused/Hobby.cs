﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyNetWebsite.Models.Resused;

public class Hobby
{
    public int Id { get; set; }

    [Required]
    [MaxLength(60)]
    public string hobbyName { get; set; }
}
