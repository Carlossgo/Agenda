using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.Configuration.Install;
using System.IO;
using System.Diagnostics;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmAgenda : Form
    {
        public frmAgenda()
        {
            InitializeComponent();
        }

        //Evento al hacer click en el botón Listar
        private void btnListar_Click(object sender, EventArgs e)
        {

            string agenda = ConfigurationManager.AppSettings["agenda"];
            //Comprobamos que el fichero existe
            if (File.Exists(agenda))
            {
                errorP.Clear();
                Mostrar(agenda);
            }
            else
            {
                errorP.SetError(this.btnListar, "Error: No se ha podido encontrar el fichero.");
            }
        }

        //Función Mostrar rellenará los datos del DataGridView
        private void Mostrar(string agenda)
        {
            this.dtgListado.Columns.Clear();
            //Cargamos los datos en el Data Grid View
            this.dtgListado.DataSource = NPersona.Mostrar(agenda);
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dtgListado.Rows.Count);
        }


        //Evento al hacer click en el botón Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
                errorP.SetError(this.txtBuscar, "El campo de texto de búsqueda debe de contener algo.");
            else
            {
                BuscarLINQ(this.txtBuscar.Text, this.cboFiltro.Text, this.dtgListado);
                errorP.Clear();
            }
        }
        
        //Sistema de búsqueda de LINQ para buscar el dato de la columna que queremos en el DataGridView
        void  BuscarLINQ(string TextoABuscar, string Columna, DataGridView grid)
        {
            bool datafound = false;

            //Filtramos las filas
            List<DataGridViewRow> rows = (from item in grid.Rows.Cast<DataGridViewRow>()
                let colName = Convert.ToString(item.Cells[Columna].Value ?? string.Empty)
                where colName.Contains(TextoABuscar)
                select item).ToList<DataGridViewRow>();

            //Recorremos todas las filas
            foreach (DataGridViewRow row in rows)
            {
                //Filtramos las columnas
                List<DataGridViewCell> cells = (from item in row.Cells.Cast<DataGridViewCell>()
                    let cell = Convert.ToString(item.Value)
                    where cell.Contains(TextoABuscar)
                    select item).ToList<DataGridViewCell>();
                //Seleccionamos solo la primera posición encontrada
                if (datafound ==false)
                    foreach (DataGridViewCell item in cells)
                    {
                        item.Selected = true;
                        datafound = true;
                    }
           }
            //Si no encuentra nada dejamos un aviso
            if (datafound==false)
                MessageBox.Show("No se ha encontrado el dato buscado!", "Aviso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        }

    }
}
