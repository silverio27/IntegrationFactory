using System.Threading.Tasks;
using Flunt.Notifications;
using IntegrationFactory.Domain.Commands.SqlServerCommand;
using IntegrationFactory.Domain.Handlers.Contracts;

namespace IntegrationFactory.Domain.Handlers
{
    public class CopyData<O, D> : Notifiable, ICopyData<SqlServerOrigin<O>, SqlServerDestiny<D>>
    {
        public Result Copy(SqlServerOrigin<O> origin, SqlServerDestiny<D> destiny)
        {
            destiny.Validate();
            if (destiny.Invalid || !destiny.TestConnection())
                return new Result(false, "O destino não é válido.", destiny.Notifications);

            origin.Validate();
            if (origin.Invalid || !origin.TestConnection())
                return new Result(false, "A origem não é válida.", origin.Notifications);

            var originData = origin.Get();
            var sendItems = destiny.Send(originData);

            return new Result(true, "Dados enviados", new
            {
                sendItems
            });
        }
    }
}