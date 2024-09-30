using System.Text.Json.Serialization;

namespace BookShopApi.Models;

public class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    /* Navigation properties */
    [JsonIgnore]
    public virtual List<Book> Books { get; set; } = new ();
}
