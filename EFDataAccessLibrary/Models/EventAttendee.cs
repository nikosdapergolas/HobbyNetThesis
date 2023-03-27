﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Models;

public class EventAttendee
{
    public int Id { get; set; }

    [Required]
    public User attendee { get; set; }
}
