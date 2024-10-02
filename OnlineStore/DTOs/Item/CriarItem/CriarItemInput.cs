using System.ComponentModel.DataAnnotations;

namespace OnlineStore.DTOs.Item.CriarItem;

public class CriarItemInput
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, 10000.00, ErrorMessage = "O preço deve estar entre 0.01 e 10000.00")]
    public decimal Preco { get; set; }

    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string Descricao { get; set; }
}