namespace Song_Project.Controllers;

using Microsoft.AspNetCore.Mvc;
using Song_Project.Models;
using System.Text.Json;
using Song_Project.Services;
using Song_Project.Operations;
using Song_Project.Operations.Comments;
using Song_Project.Operations.Users;

[ApiController]
[Route("comments")]
public class CommentsController : ControllerBase
{
    private readonly ILogger<CommentsController> _logger;
    private readonly ISongsService _songsService;
    private readonly IUserService _usersService;
    private readonly ICommentsService _commentsService;
    private readonly ValidateSaveComment _validateSaveComment;
    private readonly ValidateDeleteComment _validateDeleteComment;
    public CommentsController(
        ILogger<CommentsController> logger,
        ICommentsService commentsService, 
        ISongsService songsService,
        IUserService usersService,
        ValidateSaveComment validateSaveComment,
        ValidateDeleteComment validateDeleteComment
        )
    {
        _logger = logger;
        _commentsService = commentsService;
        _songsService = songsService;
        _usersService = usersService;
        _validateSaveComment = validateSaveComment;
        _validateDeleteComment = validateDeleteComment;
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

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            Comment comment = _commentsService.FindById(id);
            Validator validator = new ValidateGetComment(comment);
            validator.run();

            if(validator.HasErrors){
                return UnprocessableEntity(validator.Payload);
            } else {
                bool commentDeleted = _commentsService.Delete(comment);
                Dictionary<string, object> item = new Dictionary<string, object>();
                if (commentDeleted)
                {
                    item["message"] = "Comment with id: " + id + " was deleted.";
                }
                else 
                {
                    item["message"] = "Comment with id: " + id + " is not deleted.";
                }
                return Ok(item);
            }     
        }
        catch(Exception e){
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";
            _logger.LogInformation(e.Message);
            _logger.LogInformation(e.StackTrace);
            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }

    [HttpGet("username")]
    public IActionResult ShowUser(int userId)
    {
        try{
            System.Console.WriteLine("Returning username of id: " + userId);
            User user = _usersService.FindByUserId(userId);
            return Ok(user);
        }
        catch(Exception e){
            _logger.LogInformation(e.Message);
            //_logger.LogInformation(e.StackTrace);
            return UnprocessableEntity(e.StackTrace);

        }
    }

    [HttpGet("id")]
    public IActionResult FindById(int commentId)
    {
        try {
            Comment comment = _commentsService.FindById(commentId);
            return Ok(comment);
        }
        catch(Exception e){
            _logger.LogInformation(e.Message);
            //_logger.LogInformation(e.StackTrace);
            return UnprocessableEntity(e.StackTrace);
    }

    }
}