using System.ComponentModel.DataAnnotations;
namespace AplicativoWebAcademia.Models
{
    public class PessoaModel
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Quantidade de filhos")]
        public int QuantidadeFilhos { get; set; }
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Display(Name = "Salário")]
        public decimal Salario { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Situação")]
        public string Alterar { get; set; }
    }
}