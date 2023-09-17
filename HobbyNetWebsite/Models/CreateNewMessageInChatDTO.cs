namespace HobbyNetWebsite.Models;

public class CreateNewMessageInChatDTO
{
    public string sender { get; set; }
    public string receiver { get; set; }
    public string messageBody { get; set; }
}
