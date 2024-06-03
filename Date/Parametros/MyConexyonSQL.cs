using Examen_Final_P1.Date.Parametros;
using Google.Protobuf.Reflection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Examen_Final_P1.Date.Parametros
{
    internal class MyConexyonSQL 
    {
        string connectionString = "Server = localhost; Database=examen_final;Uid=root;Pwd=1234byto";
        private MySqlConnection connection;

        public bool Probarconexion()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }


        }
        public DataTable Leer()
        {
           
            DataTable the_king_of_fighters = new DataTable();
            using (MySqlConnection connection= new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM the_king_of_fighters";
                using (MySqlCommand commnd = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataAdapter adapter= new MySqlDataAdapter(commnd))
                    {
                        adapter.Fill(the_king_of_fighters);
                    }
                }
            }
            return the_king_of_fighters;
        }
        public int AñadirMyConexyon(string nombre, string apellido, DateTime fechae_nacimiento, string altura, string peso, string tipo_sanguinio, string estilo_pelea)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if (string.IsNullOrEmpty(nombre))
                    {
                        MessageBox.Show("Error: El campo 'Nombre' no puede estar vacío.");
                        return -1;
                    }

                    if (string.IsNullOrEmpty(apellido))
                    {
                        MessageBox.Show("Error: El campo 'Apellido' no puede estar vacío.");
                        return -1;
                    }

                    if (fechae_nacimiento == DateTime.MinValue)
                    {
                        MessageBox.Show("Error: Debe ingresar una fecha de nacimiento valida.");
                        return -1;
                    }

                    if (string.IsNullOrEmpty(estilo_pelea) || !double.TryParse(estilo_pelea, out _))
                    {
                        MessageBox.Show("Error: El campo estilo pelea no puede estar valido");
                        return -1;
                    }

                    if (string.IsNullOrEmpty(tipo_sanguinio) || !double.TryParse(tipo_sanguinio, out _))
                    {
                        MessageBox.Show("Error: El campo tipo sanguinio no puede quedar vacio");
                        return -1;
                    }
                    string query = "INSERT INTO examen_final(nombre, apellido, fecha_nacimiento, altura, peso, tipo sanguinio, estilo pelea) VALUES (@nombre, @apellido, @fecha_nacimineto, @altura, @peso, @tipo_sanguinio, @estilo_pelea)";

                    using (MySqlCommand commnd = new MySqlCommand(query, connection))
                    {
                        commnd.Parameters.AddWithValue("@nombre", nombre);
                        commnd.Parameters.AddWithValue("@apellido", apellido);
                        commnd.Parameters.AddWithValue("@fecha_nacimiento", fechae_nacimiento);
                        commnd.Parameters.AddWithValue("@altura", altura);
                        commnd.Parameters.AddWithValue("@peso", peso);
                        commnd.Parameters.AddWithValue("@tipo_sanguinio", tipo_sanguinio);
                        commnd.Parameters.AddWithValue("@estilo_pelea", estilo_pelea);

                        return commnd.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al añadir personaje: " + ex.Message);
                    throw;
                }
                finally { connection.Close(); }
            }

        }
        public int EliminarMyConexyon(int ID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sql = "DELETE FROM examen_final WHERE ID = @ID";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        int no = command.ExecuteNonQuery();
                        if (no == 0)
                        {
                            MessageBox.Show("Error: No se encontro ningun personaje con el id ingresado");
                        }

                        return command.ExecuteNonQuery();
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Error al eliminar personaje " + ex.Message);
                    throw;
                }
                finally { connection.Close(); }
            }
        }

        public int ActualizarMyConexyon(int ID, string nombre, string apellido, DateTime fecha_nacimiento, string altura, string peso, string tipo_sanguinio, string estilo_pelea)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                // Actualizar el registro en la base de datos
                try
                {
                    connection.Open();

                    string query = "UPDATE examen_final SET nombre = @nombre, apellido = @apellido, fecha_nacimeinto " +
                        "= @fecha_nacimiento, altura = @altura, peso = @peso, tipo_sanguinio = @tipo_sanguinio, estilo_pelea = @estilo_pelea WHERE ID = @ID";

                    using (MySqlCommand commnd = new MySqlCommand(query, connection))
                    {
                        commnd.Parameters.AddWithValue("@nombre", nombre);
                        commnd.Parameters.AddWithValue("@apellido", apellido);
                        commnd.Parameters.AddWithValue("@fecha_nacimiento", fecha_nacimiento);
                        commnd.Parameters.AddWithValue("@altura", altura);
                        commnd.Parameters.AddWithValue("@peso", peso);
                        commnd.Parameters.AddWithValue("@tipo_sanguinio", tipo_sanguinio);
                        commnd.Parameters.AddWithValue("@estilo_pelea", estilo_pelea);
                        commnd.Parameters.AddWithValue("@ID", ID);

                        return commnd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el registro: " + ex.Message);
                    return -1;
                }
            }
        }
    }
}




