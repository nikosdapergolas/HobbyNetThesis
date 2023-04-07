﻿using EFDataAccessLibrary.Models.DataTransferObjects;
using EFDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Services.PostsService;

public interface IPostsService
{
    public Task<IEnumerable<Post>> GetAllPosts();
    public Task<Post> GetOnePost(int id);
    public Task<string> CreatePostByAdmin();
    public Task<string> CreatePostByUser();
    public Task<string> EditPost(PostEditDTO postEditDTO);
    public Task<string> DeletePost(int id);
}