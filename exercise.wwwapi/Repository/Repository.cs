﻿using exercise.wwwapi.Data;
using Microsoft.EntityFrameworkCore;

namespace exercise.wwwapi.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private DataContext _db;
    private DbSet<T> _table = null;
    public Repository(DataContext db)
    {
        _db = db;
        _table = _db.Set<T>();
    }

    public async Task<T> Create(T entity)
    {
        _table.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> GetById(int id)
    {
        return await _table.FindAsync(id);
    }
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _table.ToListAsync();
    }
    
    public async Task<T?> Delete(int id)
    {
        T? entity = await _table.FindAsync(id);
        if (entity == null) return null;
        _table.Remove(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> Update(T entity)
    {
        _table.Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return entity;
    }
}
