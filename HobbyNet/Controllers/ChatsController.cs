using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Services.ChatsService;
using EFDataAccessLibrary.Services.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HobbyNet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatsController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IChatsService _chatsService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IConfiguration _config;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChatsController(
        IChatsService chatsService,
        IWebHostEnvironment webHostEnvironment,
        IConfiguration config,
        IHttpContextAccessor httpContextAccessor)
    {
        _chatsService = chatsService;
        _webHostEnvironment = webHostEnvironment;
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET: api/Chats
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Chat>>> GetAllChats()
    {
        var chats = await _chatsService.GetAllChats();

        if (chats is not null)
        {
            return Ok(chats);
        }
        else
        {
            return BadRequest();
        }
    }

    // GET: api/Chats/Conversation
    [HttpGet("Conversation/{username1}/{username2}")]
    [Authorize]
    public async Task<ActionResult<Chat>> GetConversationBetweenTwoUsers(string username1, string username2)
    {
        var chat = await _chatsService.GetConversationBetweenTwoUsers(username1, username2);

        if (chat is not null)
        {
            return Ok(chat);
        }
        else
        {
            return BadRequest(new Chat());
        }
    }

    // GET: api/Chats/SendNewMessage
    [HttpPost("SendNewMessage")]
    [Authorize]
    public async Task<ActionResult<Chat>> CreateANewChat(CreateNewMessageInChatDTO newMessageInChatDTO)
    {
        var newChat = await _chatsService.CreateANewChat(newMessageInChatDTO);

        if (newChat is not null)
        {
            return Ok(newChat);
        }
        else
        {
            return BadRequest();
        }
    }

    // GET: api/Chats/chatsOfUser/user1
    [HttpGet("chatsOfUser")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Chat>>> GetAllChatsOfOneUser(string username)
    {
        var newChats = await _chatsService.GetAllChatsOfOneUser(username);

        if (newChats is not null)
        {
            return Ok(newChats);
        }
        else
        {
            return BadRequest();
        }
    }
}
