using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//para usar la capa de datos
using CapaDatos;
//para trabajar con tipo de datos SQL
using System.Data;

namespace CapaNegocio
{
    public class NPersona
    {
        //Método Mostrar que llama al método Mostrar de la clase DPersona 
        //de la CapaDatos
        public static DataTable Mostrar(string agenda)
        {

            return new DPersona().Mostrar(agenda);
        }

    }
}
