﻿
using Ejemplo_15_personas_datoslib.Models;
using Ejemplo_15_personas_datoslib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ejemplo_01_0_CRUD_MVC_Simple.Controllers;

public class PersonasController : Controller
{
    readonly private PersonasService _personasService;

    public PersonasController(PersonasService personaService)
    {
        _personasService = personaService;
    }

    // GET: PersonasController
    [HttpGet]
    async public Task<IActionResult> Index()
    {
        return View(await _personasService.GetAll());
    }

    // GET: PruebaController1/Details/5
    [HttpGet]
    async public Task<ActionResult> Details(int id)
    {
        var persona = await _personasService.GetById(id);
        return View(persona);
    }

    // GET: PruebaController1/Create
    [HttpGet]
    async public Task<ActionResult> Create()
    {
        return View();
    }

    // POST: PruebaController1/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    //https://learn.microsoft.com/es-es/aspnet/core/security/anti-request-forgery?view=aspnetcore-9.0
    // http://go.microsoft.com/fwlink/?LinkId=317598
    async public Task<ActionResult> Create(PersonaModel nuevo)
    {
        try
        {
            await _personasService.CrearNuevo(nuevo);
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", "Error al crear la persona. " + ex.Message);
            return View(nuevo);
        }
    }

    // GET: PersonaController1/Edit/5
    //http://localhost:5033/Personas/Editar/1
    [HttpGet]
    async public Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return BadRequest();

        var persona = await _personasService.GetById(Convert.ToInt32(id));
        return View(persona);
    }

    // POST: PersonaController/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    async public Task<ActionResult> Edit(int id, PersonaModel? persona)
    {
        if (persona == null || id<=0)
            return BadRequest();

        if (id != persona.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await _personasService.Actualizar(persona);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            { 
                ModelState.AddModelError("", "Error al actualizar la persona. " + ex.Message);
                return View(persona);
            }
        }

        ModelState.AddModelError("", "Error en el modelo");
        return View(persona);
    }

    // GET: PruebaController1/Delete/5
    [HttpGet]
    async public Task<ActionResult> Delete(int? id)
    {
        if (id == null || id <= 0)
            return BadRequest();

        var persona = await _personasService.GetById(Convert.ToInt32(id));

        if (persona == null)
            return NotFound();

        return View(persona);
    }

    // POST: PruebaController1/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    async public Task<ActionResult> Delete(int? id,  PersonaModel persona)
    {
        if (id == null || id <= 0)
            return BadRequest();

        PersonaModel objeto = null;
        try
        {
            objeto = await _personasService.GetById(Convert.ToInt32(id));
            if (objeto == null)
                return NotFound();

            await _personasService.Eliminar(Convert.ToInt32(id));

            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", "Error al eliminar la persona. " + ex.Message);
            return View(objeto); 
        }
    }
}
