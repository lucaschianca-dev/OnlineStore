using OnlineStore.Models;
using OnlineStore.DTOs.Item.CriarItem;
using AutoMapper;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;

namespace OnlineStore.Services;

public class ItemService
{
    private readonly IMapper _mapper;
    private readonly IFirebaseClient _firebaseClient;

    public ItemService(IMapper mapper, IFirebaseClient firebaseClient)
    {
        _mapper = mapper;
        _firebaseClient = firebaseClient;
    }

    public async Task<CriarItemOutput> AddItemAsync(CriarItemInput input)
    {
        try
        {
            Item newItem = _mapper.Map<Item>(input);
            string itemId = newItem.Id.ToString();

            FirebaseResponse response = await _firebaseClient.SetAsync($"Items/{itemId}", newItem);

            CriarItemOutput output = _mapper.Map<CriarItemOutput>(newItem);
            output.Sucesso = response.StatusCode == System.Net.HttpStatusCode.OK;
            output.MensagemErro = output.Sucesso ? null : "Erro ao adicionar item.";
            return output;
        }
        catch (Exception ex)
        {
            return new CriarItemOutput { Sucesso = false, MensagemErro = $"Erro ao adicionar item: {ex.Message}" };
        }
    }

    public async Task<CriarItemOutput> GetItemByIdAsync(string id)
    {
        try
        {
            FirebaseResponse response = await _firebaseClient.GetAsync($"Items/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Body != "null")
            {
                Item item = response.ResultAs<Item>();
                return _mapper.Map<CriarItemOutput>(item);
            }
            return null;
        }
        catch (Exception ex)
        {
            return new CriarItemOutput { Sucesso = false, MensagemErro = $"Erro ao buscar item: {ex.Message}" };
        }
    }
}