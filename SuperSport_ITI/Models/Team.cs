using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FinalProject_ITI.Models
{
    public class Team
    {
        public int Id { get; set; }

        [DisplayName("Club")]
        [Required(ErrorMessage = "You have to provide a valid name.")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to provide a valid Description.")]
        [MinLength(2, ErrorMessage = "Description mustn't be less than 2 characters.")]
        [MaxLength(200, ErrorMessage = "Description mustn't exceed 20 characters")]
        public string Description { get; set; }

        [DisplayName("Club Nationality")]
        [Required(ErrorMessage = "You have to provide a valid Nationality.")]
        [MinLength(2, ErrorMessage = "Nationality mustn't be less than 2 characters.")]
        [MaxLength(20, ErrorMessage = "Nationality mustn't exceed 20 characters")]
        public string TeamNationality { get; set; }

        [DisplayName("Date of Establishment")]
        public DateTime EstablishmentDate { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }

        [ValidateNever]
        public List<Player> Players { get; set; }


    }
}
