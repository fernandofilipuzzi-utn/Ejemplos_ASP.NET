﻿namespace Ejemplo_01_0_CRUD_MVC_Simple.DALs;

public interface IBaseDAL<T, K>
{
    List<T> GetAll();
    T? GetByKey(K key);

    bool Insert(T nuevo);
    bool Update(T actualizar);

    void Delete(K id);
}
