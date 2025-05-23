﻿using System.Threading.Tasks;

namespace Ejemplo_13.DALs;

public interface IBaseDAL<T, K>
{
    Task<List<T>> GetAll();
    Task<T?> GetByKey(K key);

    Task<bool> Insert(T nuevo);
    Task<bool> Update(T actualizar);

    Task<bool> Delete(K id);
}
