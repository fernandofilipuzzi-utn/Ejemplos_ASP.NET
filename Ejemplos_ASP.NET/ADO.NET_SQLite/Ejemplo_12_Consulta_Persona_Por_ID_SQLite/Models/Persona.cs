﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ej11_Consulta_Persona_Por_ID.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public override string ToString()
        {
            return $"{{\"id\"={Id}, \"nombre\"=\"{Nombre}\"}}"; //serializado en json
        }
    }
}
