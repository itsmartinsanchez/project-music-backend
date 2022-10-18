namespace Song_Project.Controllers;

using Microsoft.AspNetCore.Mvc;
using Song_Project.Models;
using System.Text.Json;
using Song_Project.Services;
using Song_Project.Operations;
using Song_Project.Operations.Songs;

[ApiController]
[Route("songs")]
public class SongsController : ControllerBase
{
    private readonly ILogger<SongsController> _logger;
    private readonly ISongsService _songsService;
    private readonly ICommentsService _commentsService;
    private readonly IArtistsService _artistsService;
    private readonly ValidateSaveSong _validateSaveSong;
    public SongsController(
        ILogger<SongsController> logger,
        ICommentsService commentsService,  
        ISongsService songsService,
        ValidateSaveSong validateSaveSong
        )
    {
        _logger = logger;
        _commentsService = commentsService;
        _songsService = songsService;
        _validateSaveSong = validateSaveSong;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<Song> songs = _songsService.GetAll();

        Console.WriteLine("Returning all songs...");
        return Ok(songs);
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        Song songs = _songsService.FindById(id);

        Validator validator = new ValidateGetSong(songs); 
        validator.run();

        if(validator.HasErrors) {
            return NotFound(validator.Payload);
        } else {
            return Ok(songs);
        }
    }
    
    [HttpPut("{id}")]
    public IActionResult Update([FromBody]object payload, int id)
    {
        try {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());
            hash["id"] = id;
            _validateSaveSong.InitializeParameters(hash);
            _validateSaveSong.run();

            if(_validateSaveSong.HasErrors) {
                return UnprocessableEntity(_validateSaveSong.Payload);
            } else {
                Song temp = _songsService.Save(hash);
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

            _validateSaveSong.InitializeParameters(hash);
            _validateSaveSong.run();

            if(_validateSaveSong.HasErrors) {
                return UnprocessableEntity(_validateSaveSong.Payload);
            } else {
                Song temp = _songsService.Save(hash);
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
    
    [HttpGet("id/comments")]
    public IActionResult ShowComments(int songId)
    {
        List<Comment> comments = _commentsService.FindBySongId(songId);

        Console.WriteLine("Returning song comments...");
        return Ok(comments);
    }

    [HttpDelete("id")]
    public IActionResult Delete(int id)
    {
        try
        {
            Song song = _songsService.FindById(id);
            Validator validator = new ValidateGetSong(song);
            validator.run();

            if(validator.HasErrors){
            return UnprocessableEntity(validator.Payload);
            } else {
            bool songDeleted = _songsService.Delete(song);
            Dictionary<string, object> item = new Dictionary<string, object>();
            if (songDeleted)
            {
                item["message"] = "Song with id: " + id + " was deleted.";    
            }
            else
            {
                item["message"] = "Song with id: " + id + "is not deleted.";
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
}