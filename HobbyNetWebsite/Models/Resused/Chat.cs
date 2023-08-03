using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyNetWebsite.Models.Resused;

public class Chat
{
    public int Id { get; set; }
    public User lastMessageSender { get; set; }

    public List<ChatMember> members { get; set; } = new List<ChatMember>();
    public List<Message> chatMessages { get; set; } = new List<Message>();
}
