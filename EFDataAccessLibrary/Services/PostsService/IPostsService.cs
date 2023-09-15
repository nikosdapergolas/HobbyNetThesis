using EFDataAccessLibrary.Models.DataTransferObjects;
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
    public Task<IEnumerable<Post>> GetPostsFromOneUser(string username);
    // ------------------------------------------------------------------------
    // Trying pagination
    public Task<IEnumerable<Post>> GetPaginatedPosts(int page, int pageSize);
    // ------------------------------------------------------------------------
    public Task<IEnumerable<Post>> SearchPost(string searchTerm);
    public Task<Post> GetOnePost(int id);
    //public Task<Post> CreatePostByAdmin(PostCreateDTO post);
    public Task<Post> CreatePostByUser(PostCreateDTO post);
    public Task<string> EditPost(PostEditDTO postEditDTO);
    public Task<string> DeletePost(int id);
    public Task<Post> LikePost(LikePostDTO likePostDTO);
    public Task<Post> CommentPost(CommentDTO commentDTO);
    Task<IEnumerable<Post>> GetPostsFromOneHobby(string hobby);
}
