﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Domain.Entities
{
    [Table("TB_Users")]
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
    }
}
