using System.ComponentModel.DataAnnotations;

namespace academico.Models
{
    public class Projeto : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome do projeto não deverá ultrapassar 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "A sigla não poderá ter espaços.")]
        [Display(Name = "Sigla")]
        public string Sigla { get; set; } = string.Empty;

        public int Ano { get; set; }
        public Status Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Ano != DateTime.Now.Year)
            {
                yield return new ValidationResult(
                    "Não deverá ser permitido cadastrar projetos com ano diferente do atual.",
                    new[] { nameof(Ano) });
            }
        }
    }
}
