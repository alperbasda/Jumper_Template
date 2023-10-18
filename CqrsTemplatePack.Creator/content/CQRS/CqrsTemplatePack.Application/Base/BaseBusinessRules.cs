using Core.ApiHelpers.JwtHelper.Models;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Dynamic;
using Core.Persistence.Models;
using Core.Persistence.Models.Responses;
using Core.Persistence.Repositories;
using Core.Persistence.Requests;


namespace CqrsTemplatePack.Application.Base;

public class BaseBusinessRules
{

    protected readonly TokenParameters TokenParameters;

    public BaseBusinessRules(TokenParameters tokenParameters)
    {
        TokenParameters = tokenParameters;
    }

    public virtual Task ThrowExceptionIfDataNull<TEntity>(TEntity? data)
    {
        if (data == null)
            throw new NotFoundException($"{typeof(TEntity).Name} not found");

        return Task.CompletedTask;
    }

    public virtual Task ThrowExceptionIfDataNullOrEmpty<TEntity>(List<TEntity>? data)
    {
        if (data == null || !data.Any())
            throw new NotFoundException($"{typeof(TEntity).Name} not found");

        return Task.CompletedTask;
    }

    public virtual void FillDynamicFilter<T>(ListModel<T> data, DynamicQuery? dq, PageRequest pr)
    {
        data.DynamicQuery = dq ?? new DynamicQuery();
        data.PageRequest = pr;
    }

    public virtual void SetUserId<T>(T data)
        where T : IUserOwnedEntity
    {
        if (TokenParameters.IsSuperUser && data.UserId != Guid.Empty)
            return;

        data.UserId = TokenParameters.UserId;
    }

    public virtual void ThrowExceptionIfDataOwnerNotLoggedUser<T>(T data)
        where T : IUserOwnedEntity
    {
        if (data.UserId == TokenParameters.UserId || TokenParameters.IsSuperUser)
            return;

        throw new BusinessException("Bu veri üzerinde sadece verinin sahibi işlem yapabilir.");
    }

    public virtual void AddLoggedUserIdInDynamicQuery(DynamicQuery? query)
    {
        if (TokenParameters.IsSuperUser)
            return;

        var attach = new Filter { Field = "UserId", Logic = Logic.And, Operator = FilterOperator.Equals, Value = TokenParameters.UserId.ToString() };
        AddFilterInDynamicQuery(query, attach);
    }

    public virtual void AddFilterInDynamicQuery(DynamicQuery? query, Filter attach)
    {
        if (query == null)
        {
            query = new DynamicQuery();

        }
        if (query.Filter == null)
        {
            query.Filter = attach;
        }
        else if (query.Filter.Filters == null)
        {
            query.Filter.Filters = new List<Filter> { attach };
        }
        else
        {
            query.Filter.Filters.Add(attach);
        }
    }



}
