using System.Threading.Tasks;
using IntegrationFactory.Domain.Commands.Contracts;

namespace IntegrationFactory.Domain.Handlers.Contracts
{
    public interface ICopyData<O, D> 
    {
        Result Copy(O origin, D destiny);
    }
}