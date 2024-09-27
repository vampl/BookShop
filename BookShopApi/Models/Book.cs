﻿using System.Text.Json.Serialization;

namespace BookShopApi.Models;

public class Book
{
    public int Isbn { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime PublishedAt { get; set; }

    public string? Description { get; set; }

    public int Pages { get; set; }

    public int GenreId { get; set; }

    [JsonIgnore]
    public virtual Genre Genre { get; set; }
}