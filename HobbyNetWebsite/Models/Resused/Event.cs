using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyNetWebsite.Models.Resused;

public class Event
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string eventName { get; set; }

    [Required]
    [MaxLength(50)]
    public string Location { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(250)]
    public string description { get; set; }

    [Required]
    public Hobby hobby { get; set; }

    public List<EventAttendee> attendees { get; set; } = new List<EventAttendee>();
}
