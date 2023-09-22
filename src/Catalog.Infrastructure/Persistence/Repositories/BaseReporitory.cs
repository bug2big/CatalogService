using Catalog.Application.Common.Interfaces;
using Catalog.Domain.Common;
using Catalog.Infrastructure.Persistence.AppDbContext;

namespace Catalog.Infrastructure.Persistence.Repositories;

public abstract class BaseReporitory<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public BaseReporitory(ApplicationDbContext applicationDbContext)
    {
        Context = applicationDbContext;
        DbSet = applicationDbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var storedEntity = await DbSet.FirstOrDefaultAsync(e => e.Id == entity.Id, cancellationToken);
        var updatedEntity = CheckUpdateObject(storedEntity, entity);

        DbSet.Update(updatedEntity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    private TEntity CheckUpdateObject(TEntity storedEntity, TEntity updatedEntity)
    {
        var changedPropertiesList = updatedEntity.GetType().GetProperties();
        var entry = Context.Entry(storedEntity);

        foreach (var checkProperty in changedPropertiesList)
        {
            var oldValue = storedEntity.GetType().GetProperty(checkProperty.Name).GetValue(storedEntity);
            var newValue = updatedEntity.GetType().GetProperty(checkProperty.Name).GetValue(updatedEntity);
            if (oldValue == null && newValue != null)
            {
                entry.Property(checkProperty.Name).OriginalValue = oldValue;
            }

            if (oldValue != null && newValue != null) 
            { 
                entry.Property(checkProperty.Name).OriginalValue = newValue;
            }
        }

        return entry.Entity;
    }
}