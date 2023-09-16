using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Models;
using EFDataAccessLibrary.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.ChatsService;

public class ChatsService : IChatsService
{
    private readonly DatabaseContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChatsService(
        DatabaseContext context,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Chat>> GetAllChats()
    {
        var chats = await _context.Chats
            .Include(m => m.members)
            .ThenInclude(m => m.member)
            .Include(cm => cm.chatMessages)
            .ToListAsync();
        return chats;
    }

    public async Task<IEnumerable<Chat>> GetAllChatsOfOneUser(string username)
    {
        var chats = await _context.Chats
           .Include(c => c.members)
           .ThenInclude(m => m.member)
           .Include(c => c.chatMessages)
           .Where(c => c.members.Any(m => m.member.username == username))
           .ToListAsync();
        return chats;
    }

    public async Task<Chat> CreateANewChat(CreateNewMessageInChatDTO newMessageInChatDTO)
    {
        var sender = await _context.Users
            .Where(u => u.username.Equals(newMessageInChatDTO.sender))
            .FirstOrDefaultAsync();
        var receiver = await _context.Users
            .Where(u => u.username.Equals(newMessageInChatDTO.receiver))
            .FirstOrDefaultAsync();

        if (sender is null || receiver is null)
        {
            return null!;
        }
        //--------------------------------------------
        var chats = await _context.Chats
           .Include(c => c.members)
           .ThenInclude(m => m.member)
           .Include(c => c.chatMessages)
           .Where(c =>
               c.members.Any(m => m.member.username == newMessageInChatDTO.sender) &&
               c.members.Any(m => m.member.username == newMessageInChatDTO.receiver))
           .FirstOrDefaultAsync();
        //--------------------------------------------

        if(chats is null)
        {
            Chat chat = new();
            chat.lastMessageSender = sender;

            Message message = new();
            message.messageBody = newMessageInChatDTO.messageBody;
            message.timestamp = DateTime.Now;

            ChatMember member1 = new ChatMember();
            member1.member = sender;
            ChatMember member2 = new ChatMember();
            member2.member = receiver;

            await _context.AddAsync(chat);

            await _context.AddAsync(message);
            await _context.AddAsync(member1);
            await _context.AddAsync(member2);

            chat.chatMessages.Add(message);
            chat.members.Add(member1);
            chat.members.Add(member2);

            await _context.SaveChangesAsync();

            return chat;
        }
        else
        {
            Message message = new();
            message.messageBody = newMessageInChatDTO.messageBody;
            message.timestamp = DateTime.Now;


            await _context.AddAsync(message);
            chats.chatMessages.Add(message);

            await _context.SaveChangesAsync();

            return chats;
        }
    }
}
