using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels.Article
{
    public class UpdateArticleViewModel
    {
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "Author's username is required")]
        public string? AuthorUsername { get; set; }
    }
}
