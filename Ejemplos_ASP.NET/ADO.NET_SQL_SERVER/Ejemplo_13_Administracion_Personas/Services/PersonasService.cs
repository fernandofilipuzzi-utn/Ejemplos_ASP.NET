﻿

using Ejemplo_13.DALs.MSSDALs;
using Ejemplo_13.Models;

namespace Ejemplo_13.Services;

public class PersonasService
{
    private PersonasMSSDAL _dao = new ();

    async public Task<List<PersonaModel>> GetAll()
    {
        return await _dao.GetAll();
    }

    async public Task<PersonaModel?> GetById(int id)
    {
        return await _dao.GetByKey(id);
    }

    async public Task CrearNuevo(PersonaModel objeto)
    {
        await _dao.Insert(objeto);
    }

    async public Task Actualizar(PersonaModel objeto)
    {
        await _dao.Update(objeto);
    }

    async public Task Eliminar(int id)
    {
        var objeto = await GetById(id);
        if (objeto != null)
        {
            await _dao.Delete(id);
        }
    }
}
