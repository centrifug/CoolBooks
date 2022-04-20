﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    public class Comment
    {
        public int Id { get; set; } 
        public string Text { get; set; }
        public int? reviewId { get; set; }
        public Review? Review { get; set; }
        public int? commentId { get; set; }
        public Comment? comment { get; set; }

        public List<Comment>? comments { get; set; } = new List<Comment>();

        public bool IsDeleted { get; set; } = false;

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string? CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]

        public virtual CoolBooksUser CoolBooksUser { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}