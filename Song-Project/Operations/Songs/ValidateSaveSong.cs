namespace Song_Project.Operations.Songs;

using Song_Project.Models;
using Song_Project.Services;
using System.Text.Json;

public class ValidateSaveSong : Validator
{
    public Int32 Id {get; set;}
    public String Title {get; set;}
    public Int32 ArtistId {get; set;}
    public string Lyrics {get; set;}
    public string Album {get; set;}

    public void InitializeParameters(Dictionary<string, object> hash)
    {
        if(hash.GetValueOrDefault("id") != null) {
            this.Id = Int32.Parse(hash["id"].ToString());
        }
        if(hash.GetValueOrDefault("title") != null) {
            this.Title = hash["title"].ToString();
        }
        if(hash.GetValueOrDefault("artistId") != null) {
            this.ArtistId = Int32.Parse(hash["artistId"].ToString());
        }
        if(hash.GetValueOrDefault("lyrics") != null) {
            this.Lyrics = hash["lyrics"].ToString();
        }
        if(hash.GetValueOrDefault("album") != null) {
            this.Album = hash["album"].ToString();
        }

    }
    private readonly ISongsService _songsService;
    private readonly IArtistsService _artistsService;
    public ValidateSaveSong(IArtistsService artistsService, ISongsService songsService)
    {
        _artistsService = artistsService;
        _songsService = songsService;

    }
    public ValidateSaveSong(Dictionary<string, object> hash, ISongsService songsService, IArtistsService artistsService)
    {
        _artistsService = artistsService;
        _songsService = songsService;
        this.InitializeParameters(hash);
    }
    public override void run()
        {
        if(this.Title == null || this.Title.Equals("")) {
            String msg = "Title is required";
            this.AddError(msg, "title");
        }
        if(this.Lyrics == null || this.Lyrics.Equals("")) {
            String msg = "Lyrics is required";
            this.AddError(msg, "lyrics");
        }

        if(this.ArtistId == null || this.ArtistId.Equals("")) {
            String msg = "Artist Id is required";
            this.AddError(msg, "artistId");
        } else if(!_artistsService.Exists(this.ArtistId)) {
            String msg = "Artist not found";
            this.AddError(msg, "artistId");
        }

        }
}