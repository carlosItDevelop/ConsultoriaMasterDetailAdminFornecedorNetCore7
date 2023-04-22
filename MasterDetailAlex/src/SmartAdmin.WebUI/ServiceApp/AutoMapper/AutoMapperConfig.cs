using AutoMapper;
using Cooperchip.ITDeveloper.Application.ViewModels.Farmacia;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;

namespace Cooperchip.ITDeveloper.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
