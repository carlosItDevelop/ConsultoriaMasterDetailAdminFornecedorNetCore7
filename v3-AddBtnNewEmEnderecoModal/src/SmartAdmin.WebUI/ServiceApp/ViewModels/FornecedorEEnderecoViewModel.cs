namespace Cooperchip.ITDeveloper.Mvc.ServiceApp.ViewModels
{
    public class FornecedorEEnderecoViewModel
    {
        public FornecedorEEnderecoViewModel(FornecedorViewModel fornecedorVModel, EnderecoViewModel enderecoVModel)
        {
            FornecedorVModel = fornecedorVModel;
            EnderecoVModel = enderecoVModel;
        }
        public FornecedorViewModel FornecedorVModel { get; set; }
        public EnderecoViewModel EnderecoVModel { get; set; }
    }
}
