using Database.Entities.Interfaces;
using Domain.Models.Interfaces;

namespace Logic.Logics.Interfaces
{
    public interface ILogic<TModel> where TModel : IModel, new()
    {
        Response<TModel> Create(TModel model);

        Response<TModel> Get(long id);

        Response<TModel> Update(TModel model);

        void Delete(long id);
    }
}