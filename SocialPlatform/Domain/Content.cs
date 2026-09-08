using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPlatform.Domain;

public abstract class Content
    {
        // content bur uuriin gesen dahin davtagdahgui id=tai baih ystoi.
        public Guid ContentId { get; protected set; } = Guid.NewGuid();
        public Guid AuthorId { get; protected set; }
        
        // manually uguhguigeer datetime.now geed tuhain tsagiig n tuhai burt n avna.
        public DateTime CreatedAt { get; protected set; } = DateTime.Now;
        public string ContentText { get; set; } = string.Empty;

        protected Content(Guid authorId, string contentText)
        {
            AuthorId = authorId;
            ContentText = contentText;
        }
    }    
