using System;
using System.Collections.Generic;
using System.Linq;
using Handbook.Pages;


namespace Handbook.Entities;

public class Post
{
    public Dictionary<string, List<string>> Errors { get; } = new Dictionary<string, List<string>>();
    public Guid Id { get; set; } = Guid.NewGuid();
    private string _title;
    private string _author;

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            ValidTitle();
        }
    }
    public string Author
    {
        get => _author;
        set
        {
            _author = value;
            ValidAuthor();
        }
    }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? ImagePath { get; set; }

    private const int MaxTitleLength = 128;
    private const int MaxAuthorLength = 256;

    public Post(string title, string author, string description = "", string imagePath = null)
    {
        Title = title;
        Author = author;
        Description = description;
        ImagePath = imagePath;

        if (!IsValid()) throw new ValidationException(Errors);

    }

    private void ValidTitle()
    {
        Errors.Remove(nameof(Title));
        Errors[nameof(Title)] = new List<string>();

        if (string.IsNullOrWhiteSpace(_title))
        {
            Errors[nameof(Title)].Add("Назва книги є обов'язковою.");
        }

        if (_title.Length > MaxTitleLength)
        {
            Errors[nameof(Title)].Add($"Назва книги не повинна перевищувати {MaxTitleLength} символів.");
        }

    }

    private void ValidAuthor()
    {
        Errors.Remove(nameof(Author));
        Errors[nameof(Author)] = new List<string>();
        if (string.IsNullOrWhiteSpace(_author))
        {
            Errors[nameof(Author)].Add("Автор не може бути пустим");
        }
        if (_author.Length > MaxAuthorLength)
        {
            Errors[nameof(Author)].Add($"Автор не може бути довшим за {MaxAuthorLength} символів");
        }
        var authorWords = _author.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (authorWords.Length < 2 || authorWords.Length > 3)
        {
            Errors[nameof(Author)].Add("Автор має містити від двох до трьох слів");
        }
    }

    public bool IsValid()
    {
        ValidTitle();
        ValidAuthor();
        return Errors.Values.All(errorList => errorList.Count == 0);
    }
}
