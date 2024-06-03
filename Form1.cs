using Examen_Final_P1.Date.Parametros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Examen_Final_P1
{
    public partial class Form1 : Form
    {
        private MyConexyonSQL MyConexyonSQL;

        Jugador jr = new Jugador();
        private Jugador Jugador;
        private string connectionString;
        
        public Form1()
        {
            InitializeComponent();
        InitializeComponent();
        Jugador = new Jugador();
        MyConexyonSQL = new MyConexyonSQL();
    }
  

    private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MyConexyonSQL.Probarconexion())
            { MessageBox.Show("Si se pudo 👌❤️😍😍😘"); }
            else
            {
                MessageBox.Show("Nel Pastel 😢😒🥲😓😔");
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridViewPersonajes.DataSource = MyConexyonSQL.Leer();
        }
        


        private void button3_Click(object sender, EventArgs e)
        {
            int idTarget = (int)numericUpDownActualizar.Value;

            // Encuentra la fila con el ID correspondiente al valor del NumericUpDown
            DataGridViewRow selectedRow = null;
            foreach (DataGridViewRow row in dataGridViewPersonajes.Rows)
            {
                if (Convert.ToInt32(row.Cells["ID"].Value) == idTarget)
                {
                    selectedRow = row;
                    break;
                }
            }

            if (selectedRow != null)
            {
                Jugador.nombre = textBoxNombre.Text;
                Jugador.apellido = textBoxApellido.Text;
                Jugador.fecha_nacimiento = dateTimePickerFecha.Value;
                Jugador.altura =textBoxAltura.Text;
                Jugador.peso = textBoxPeso.Text;
                Jugador.tipo_sanguinio = textBoxSanguinio.Text;
                Jugador.estilo_pelea = textBoxpela.Text;

                // Validar los datos antes de la actualización
                DialogResult result = MessageBox.Show("¿Está seguro de que desea actualizar este registro?", "Confirmación", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int resultado = MyConexyonSQL.ActualizarMyConexyon(idTarget, Jugador.nombre, Jugador.apellido, Jugador.fecha_nacimiento, Jugador.altura, Jugador.peso, Jugador.tipo_sanguinio, Jugador.estilo_pelea);

                        if (resultado > 0)
                        {
                            MessageBox.Show("El personaje se actualizo correctamente.");
                            // Recargar los datos en el DataGridView
                            RecargarDatosDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Error al actualizar el personaje.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar el registro: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No se encontró un personaje con el ID proporcionado.");
            }
        }

        private void RecargarDatosDataGridView()
        {
            dataGridViewPersonajes.DataSource = MyConexyonSQL.Leer();
            dataGridViewPersonajes.Refresh();
        }

        private void buttonInsertar_Click(object sender, EventArgs e)
        {
            Jugador.nombre = textBoxNombre.Text;
            Jugador.apellido = textBoxApellido.Text;
            Jugador.fecha_nacimiento = dateTimePickerFecha.Value;
            Jugador.altura =textBoxAltura.Text;
            Jugador.peso = textBoxPeso.Text;
            Jugador.tipo_sanguinio = textBoxSanguinio.Text;
            Jugador.estilo_pelea = textBoxpela.Text;
            MessageBox.Show("La Información Se a Guardado Correctamente");

            int respuesta = MyConexyonSQL.AñadirMyConexyon(Jugador.nombre, Jugador.apellido, Jugador.fecha_nacimiento, Jugador.altura, Jugador.peso,Jugador.tipo_sanguinio, Jugador.estilo_pelea);

            if (respuesta > 0)
            {
                limpiarTextBox();
                MessageBox.Show("Se añadio correctemente");
                dataGridViewPersonajes.DataSource = MyConexyonSQL.Leer();
            }
            else
            {
                MessageBox.Show("Hubo un error al añadir el personaje");
            }
        }

        private void limpiarTextBox()
        {
            textBoxid.Text = "";
            textBoxNombre.Text = "";
            textBoxApellido.Text = "";
            textBoxAltura.Text = "";
            textBoxPeso.Text = "";
            dateTimePickerFecha.Value = DateTime.Now;
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            int ID = (int)numericUpDownEliminar.Value;

            int respuesta = MyConexyonSQL.EliminarMyConexyon(ID);

            if (respuesta > 0)
            {
                limpiarTextBox();
                MessageBox.Show("Se elimino correctemente");
                dataGridViewPersonajes.DataSource = MyConexyonSQL.Leer();
            }
            else
            {
                MessageBox.Show("Se elimino el personaje pero debe cargar otravez la pagina");
            }
        }

        private void numericUpDownActualizar_ValueChanged(object sender, EventArgs e)
        {
            int idTarget = (int)numericUpDownActualizar.Value;

            // Encuentra la fila con el ID correspondiente al valor del NumericUpDown
            DataGridViewRow selectedRow = null;
            foreach (DataGridViewRow row in dataGridViewPersonajes.Rows)
            {
                if (Convert.ToInt32(row.Cells["ID"].Value) == idTarget)
                {
                    selectedRow = row;
                    break;
                }
            }
            if (selectedRow != null)
            {
                // Actualiza los TextBox y otros controles con los valores de la fila seleccionada
                textBoxNombre.Text = selectedRow.Cells["nombre"].Value?.ToString() ?? string.Empty;
                textBoxApellido.Text = selectedRow.Cells["apellido"].Value?.ToString() ?? string.Empty;
                textBoxSanguinio.Text = selectedRow.Cells["tipo_sanguinio"].Value?.ToString() ?? string.Empty;
                textBoxpela.Text = selectedRow.Cells["estilo_pelea"].Value?.ToString() ?? string.Empty;
                dateTimePickerFecha.Value = selectedRow.Cells["fecha_nacimiento"].Value != null ? Convert.ToDateTime(selectedRow.Cells["fechae"].Value) : DateTime.MinValue;

            }
            else
            {
                // Si no se encuentra la fila, limpia los TextBox y otros controles
                textBoxNombre.Text = string.Empty;
                textBoxApellido.Text = string.Empty;
                textBoxSanguinio.Text = string.Empty;
                textBoxpela.Text = string.Empty;
                dateTimePickerFecha.Value = DateTime.Now;

            }
        }

        private void dataGridViewPersonajes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttoncargar1_Click(object sender, EventArgs e)
        {
            dataGridViewPersonajes.DataSource = MyConexyonSQL.Leer();
        }
    }
}
