using BlogService.Domain.Entities;
using BlogService.Persistence.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Testing : ControllerBase
{
    public readonly AppDbContext _context;
    
    public Testing(AppDbContext context)
    {
        _context = context;
    }
    
    // GET
    [HttpGet]
    public List<Blog> GetAll()
    {
        return _context.Blogs.AsQueryable().ToList();
    }
}