using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_ITI.Models
{
    public class TopLeague
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You have to provide a valid name.")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to provide a valid Description.")]
        [MinLength(2, ErrorMessage = "Description mustn't be less than 2 characters.")]
        [MaxLength(200, ErrorMessage = "Description mustn't exceed 20 characters")]
        public string Description { get; set; }

    }
}
