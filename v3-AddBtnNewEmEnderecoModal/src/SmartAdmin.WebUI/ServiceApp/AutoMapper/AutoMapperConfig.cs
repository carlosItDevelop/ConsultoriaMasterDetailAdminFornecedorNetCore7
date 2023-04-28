using AutoMapper;
using Cooperchip.ITDeveloper.Farmacia.Domain.Entities;
using Cooperchip.ITDeveloper.Mvc.ServiceApp.ViewModels;

namespace Cooperchip.ITDeveloper.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<RepresentanteLegal, RepresentanteLegalViewModel>().ReverseMap();
        }
    }
}
