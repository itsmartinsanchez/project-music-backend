namespace Song_Project.Controllers;

using Microsoft.AspNetCore.Mvc;
using Song_Project.Models;
using System.Text.Json;
using Song_Project.Services;
using Song_Project.Operations;
using Song_Project.Operations.Comments;

[ApiController]
[Route("comments")]
public class CommentsController : ControllerBase
{
    private readonly ILogger<CommentsController> _logger;
    private readonly ISongsService _songsService;
    private readonly IUserService _usersService;
    private readonly ICommentsService _commentsService;
    private readonly ValidateSaveComment _validateSaveComment;
    public CommentsController(
        ILogger<CommentsController> logger,
        ICommentsService commentsService, 
        ISongsService songsService,
        IUserService usersService,
        ValidateSaveComment validateSaveComment
        )
    {
        _logger = logger;
        _commentsService = commentsService;
        _songsService = songsService;
        _usersService = usersService;
        _validateSaveComment = validateSaveComment;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<Comment> comments = _commentsService.GetAll();

        Console.WriteLine("Returning all comments...");
        return Ok(comments);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody]object payload, int id)
    {
        try {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());
            hash["id"] = id;
            _validateSaveComment.InitializeParameters(hash);
            _validateSaveComment.run();

            if(_validateSaveComment.HasErrors) {
                return UnprocessableEntity(_validateSaveComment.Payload);
            } else {
                Comment temp = _commentsService.Save(hash);
                return Ok(temp);
            }
        } catch(Exception e) {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";
            _logger.LogInformation(e.Message);
            _logger.LogInformation(e.StackTrace);

            return StatusCode(StatusCodes.Status500InternalServerError, msg);

        }
    }

    [HttpPost]
    public IActionResult Create([FromBody]object payload)
    {
        try {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            _validateSaveComment.InitializeParameters(hash);
            _validateSaveComment.run();

            if(_validateSaveComment.HasErrors) {
                return UnprocessableEntity(_validateSaveComment.Payload);
            } else {
                Comment temp = _commentsService.Save(hash);
                return Ok(temp);
            }
        } catch(Exception e) {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";
            _logger.LogInformation(e.Message);
            _logger.LogInformation(e.StackTrace);

            return StatusCode(StatusCodes.Status500InternalServerError, msg);

        }
    }
}