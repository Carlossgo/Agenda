using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//para trabajar con tipo de datos SQL
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DPersona
    {
        //Constructor Vacio
        public DPersona()
        {
        }

        //Método Mostrar
        public DataTable Mostrar(string agenda)
        {
            string fileName = agenda;
            string[] rows = System.IO.File.ReadAllLines(fileName);
            Char[] separator = new Char[] {'|'};
            Char[] separator2 = new Char[] { ' ' };
            DataTable DtResultado = new DataTable(fileName);
            int rowIndex = 0, colIndex = 0;

            try
            {
                //Verificamos que hay datos en el fichero
                if (rows.Length != 0)
                {
                    //Precargamos los nombres de las columnas
                    DtResultado.Columns.Add(new DataColumn("Nombre"));
                    DtResultado.Columns.Add(new DataColumn("Apellidos"));
                    DtResultado.Columns.Add(new DataColumn("Ciudad"));
                    DtResultado.Columns.Add(new DataColumn("Teléfono"));

                    if (rows.Length > 1)
                    {
                        //Recorremos filas
                        for (rowIndex = 0; rowIndex < rows.Length; rowIndex++)
                        {
                            DataRow newRow = DtResultado.NewRow();
                            string[] cols = rows[rowIndex].Split(separator);
                            int colNum = cols.Length;

                            //Recorremos columnas
                            for (colIndex = 0; colIndex < colNum; colIndex++)
                            {
                                //El nombre lo volvemos a separar en Nombre y Apellidos
                                if (colIndex == 0)
                                {
                                    string[] cols2 = cols[colIndex].Split(separator2, 2);
                                    
                                    newRow[colIndex] = cols2[0].Trim();
                                    newRow[colIndex+1] = cols2[1].Trim();
                                }
                                else
                                    newRow[colIndex+1] = cols[colIndex].Trim();
                            }

                            DtResultado.Rows.Add(newRow);
                        }
                    }
                }
                
            }catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

    }
}
