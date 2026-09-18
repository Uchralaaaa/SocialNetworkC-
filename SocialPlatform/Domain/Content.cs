using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPlatform.Domain;

public abstract class Content
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AuthorId { get; set; }
    public string ContentText { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // EF Core-д зориулсан байгуулагч
    protected Content() { }

    protected Content(Guid authorId, string contentText)
    {
        AuthorId = authorId;
        ContentText = contentText;
    }
}
// content bur uuriin gesen dahin davtagdahgui id=tai baih ystoi.// manually uguhguigeer datetime.now geed tuhain tsagiig n tuhai burt n avna.