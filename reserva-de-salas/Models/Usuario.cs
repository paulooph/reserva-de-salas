using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace reserva_de_salas.Models
{
    
    [Index(nameof(Email), IsUnique = true)]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "O e‑mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e‑mail inválido.")]
        [StringLength(100, ErrorMessage = "O e‑mail deve ter no máximo 100 caracteres.")]
        public string Email { get; set; }

        [Required]
        public bool Administrador { get; set; }
    }
}
