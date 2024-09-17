using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models;

public class Item
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    public string Nome { get; set; }

    [Required]
    [Range(0.01, 10000.00, ErrorMessage = "O preço deve estar entre 0.01 e 10000.00")]
    [DataType(DataType.Currency)]
    public decimal Preco { get; set; }

    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string Descricao { get; set; }
}