using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class BlogController : Controller
{
    public readonly ILogger<BlogController> _logger;
    
    public BlogController(ILogger<BlogController> logger)
    {
        _logger = logger;
    }

    private static readonly List<BlogArticleViewModel> _articles = new List<BlogArticleViewModel>
    {
        new BlogArticleViewModel { Id = 1, Title = "First Article", Description = "Intro to the blog.", Content = "Full content of the first article." },
        new BlogArticleViewModel { Id = 2, Title = "Second Article", Description = "Another post.", Content = "Full content of the second article." }
    };

    public IActionResult Index()
    {
        return View(_articles);
    }

    public IActionResult Article(int id)
    {
        var post = _articles.FirstOrDefault(p => p.Id == id);
        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateBlogArticleModel());
    }

    [HttpPost]
    public IActionResult Create(CreateBlogArticleModel model)
    {
        if(model.Id == 0) return Content("Provide a valid Id");
        var stored = new BlogArticleViewModel
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Content = model.Content
        };
        _articles.Add(stored);
        return RedirectToAction(nameof(Article), new { id = stored.Id });
    }
}