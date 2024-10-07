using AutoMapper;
using System;
using System.Threading.Tasks;
using OnlineStore.Models;
using OnlineStore.Services;
using Google.Cloud.Firestore;

namespace OnlineStore.DTOs.Item.CriarItem;

public class CriarItem : ICriarItem
{
    private readonly FirestoreDb _firestoreDb;

    public CriarItem(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
    }
    
    public async Task<CriarItemOutput> Executar(CriarItemInput input)
    {
        try
        {
            var newItem = new Item
            {
                Nome = input.Nome,
                Preco = input.Preco,
                Descricao = input.Descricao
            };

            DocumentReference docRef = await _firestoreDb.Collection("Items").AddAsync(newItem);
            newItem.Id = docRef.Id;  // Guarda o ID gerado

            return new CriarItemOutput
            {
                Sucesso = true,
                Mensagem = "Item criado com sucesso.",
                ItemId = newItem.Id
            };
        }
        catch (Exception ex)
        {
            return new CriarItemOutput
            {
                Sucesso = false,
                Mensagem
}