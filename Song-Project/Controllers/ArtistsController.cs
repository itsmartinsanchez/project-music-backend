namespace Song_Project.Controllers;

using Microsoft.AspNetCore.Mvc;
using Song_Project.Models;
using System.Text.Json;
using Song_Project.Services;
using Song_Project.Operations;
using Song_Project.Operations.Artists;

[ApiController]
[Route("artists")]
public class ArtistsController : ControllerBase
{
    private readonly ILogger<ArtistsController> _logger;
    private readonly IArtistsService _artistsService;
    private readonly ValidateSaveArtists _validateSaveArtists;
    private readonly ValidateDeleteArtist _validateDeleteArtists;

    public ArtistsController(
        ILogger<ArtistsController> logger, 
        IArtistsService artistsService,
        ValidateSaveArtists validateSaveArtists,
        ValidateDeleteArtist validateDeleteArtist
        )
    {
        _logger = logger;
        _artistsService = artistsService;
        _validateSaveArtists = validateSaveArtists;
        _validateDeleteArtists = validateDeleteArtist;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        List<Artists> artists = _artistsService.GetAll();

        Console.WriteLine("Returning all Artists...");
        return Ok(artists);
    }
    
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        Artists artists = _artistsService.FindById(id);

        Validator validator = new ValidateGetArtist(artists); 
        validator.run();

        if(validator.HasErrors) {
            return NotFound(validator.Payload);
        } else {
            return Ok(artists);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody]object payload, int id)
    {
        try {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            hash["id"] = id;
            _validateSaveArtists.InitializeParameters(hash);
            _validateSaveArtists.run();

            if(_validateSaveArtists.HasErrors) {
                return UnprocessableEntity(_validateSaveArtists.Payload);
            } else {
                return Ok(_artistsService.Save(hash));
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

            _validateSaveArtists.InitializeParameters(hash);
            _validateSaveArtists.run();

            if(_validateSaveArtists.HasErrors) {
                return UnprocessableEntity(_validateSaveArtists.Payload);
            } else {
                Artists temp = _artistsService.Save(hash);
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
    [HttpDelete]
    public IActionResult Delete([FromBody]object payload, int id)
    {
        try {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            hash["id"] = id;
           _validateDeleteArtists.run();

            if(_validateDeleteArtists.HasErrors) {
                return UnprocessableEntity(_validateDeleteArtists.Payload);
            } else {
                return Ok(_artistsService.Save(hash));
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