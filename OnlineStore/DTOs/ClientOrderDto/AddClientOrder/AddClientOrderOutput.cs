using Google.Cloud.Firestore;
using OnlineStore.Models;

namespace OnlineStore.DTOs.ClientOrderDto.AddClientOrder;

public class AddClientOrderOutput
{
    public string OrderId { get; set; }
    public string UserId { get; set; }
    public List<ItemOrder> Items { get; set; }
    public double TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public bool Sucesso { get; set; } // Nova propriedade para indicar o sucesso da operação
    public string MensagemErro { get; set; } // Propriedade para armazenar mensagens de erro, se houver
}
