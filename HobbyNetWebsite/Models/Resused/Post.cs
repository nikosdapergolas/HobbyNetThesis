﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyNetWebsite.Models.Resused;

public class Post
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string HobbyName { get; set; }

    [Required]
    [MaxLength(250)]
    public string body { get; set; }

    [Required]
    [MaxLength(2000)]
    public string image { get; set; }

    [Required]
    public DateTime timestamp { get; set; }

    public List<Comment>? comments { get; set; }

    public List<Like>? postLikes { get; set; }
}
