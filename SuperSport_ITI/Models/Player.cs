using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FinalProject_ITI.Models
{
    public class Player
    {
        public int Id { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "You have to provide a valid name.")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20 characters")]
        public string PlayerName { get; set; }

        [DisplayName("Position")]
        [Required(ErrorMessage = "You have to provide a valid Position.")]
        [MinLength(2, ErrorMessage = "Position mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Position mustn't exceed 20 characters")]
        public string PlayerPosition { get; set; }

        [DisplayName("Nationality")]
        [Required(ErrorMessage = "You have to provide a valid Nationality.")]
        [MinLength(2, ErrorMessage = "Nationality mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Nationality mustn't exceed 20 characters")]
        public string PlayerNationality { get; set; }

        [DisplayName("Player Number")]
        [Required(ErrorMessage = "You have to provide a valid PlayerNumber.")]
        public int PlayerNumber { get; set; }

        [DisplayName("Birth Date")]
        [Required(ErrorMessage = "You have to provide a valid BirthDate.")]
        public DateTime BirthDatePlayer { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Select a valid Team.")]
        [DisplayName("Team")]
        public int TeamId { get; set; }
        [ValidateNever]
        public Team team { get; set; }


    }
}
