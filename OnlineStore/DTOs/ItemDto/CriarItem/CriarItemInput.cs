using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.DTOs.ItemDto.CriarItem;

public class CriarItemInput
{
    [Required(ErrorMessage = "O nome � obrigat�rio.")]
    [StringLength(100, ErrorMessage = "O nome n�o pode exceder 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O pre�o � obrigat�rio.")]
    [Range(0.01, 10000.00, ErrorMessage = "O pre�o deve estar entre 0.01 e 10000.00")]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = "A descri��o n�o pode exceder 500 caracteres.")]
    public string Description { get; set; }

    public int StockQuantity { get; set; }
}