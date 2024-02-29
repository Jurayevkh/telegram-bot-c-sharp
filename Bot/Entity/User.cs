namespace Bot.Entity;

public class User
{
    public long UserId { get; set; }
    public long ChatId { get; set; }
    public bool IsBot { get; set; }
    public string FirstName{get;set;}
    public string? LastName{get;set;}
    public string? UserName { get; set; }
    public string? LanguageCode { get; set; } = "uz";
    public DateTimeOffset JoinedAt { get; set; }
    public DateTimeOffset LastInteractionAt { get; set; }
}