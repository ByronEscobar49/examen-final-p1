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
using System.Linq.Expressions;

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
        public int AñadirMyConexyon(string Nombre, string Apellido, DateTime Fecha_Nacimiento, string Altura, string Peso, string Tipo_Sanguineo, string Estilo_Pelea)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    if (string.IsNullOrEmpty(Nombre))
                    {
                        MessageBox.Show("Error: El campo 'Nombre' no puede estar vacío.");
                        return -1;
                    }

                    if (string.IsNullOrEmpty(Apellido))
                    {
                        MessageBox.Show("Error: El campo 'Apellido' no puede estar vacío.");
                        return -1;
                    }

                    if (Fecha_Nacimiento == DateTime.MinValue)
                    {
                        MessageBox.Show("Error: Debe ingresar una fecha de nacimiento valida.");
                        return -1;
                    }

                    if (string.IsNullOrEmpty(Estilo_Pelea)) 
                    {
                        MessageBox.Show("Error: El campo estilo pelea no puede estar valido");
                        return -1;
                    }

                    if (string.IsNullOrEmpty(Tipo_Sanguineo)) 
                    {
                        MessageBox.Show("Error: El campo tipo sanguinio no puede quedar vacio");
                        return -1;
                    }
                    if (string.IsNullOrEmpty(Altura))
                    {
                    MessageBox.Show("Error: El campo tipo altura no puede quedar vacio");
                        return -1;
                    }
                    if (string.IsNullOrEmpty(Peso))
                    {
                        MessageBox.Show("Error: El campo tipo peso no puede quedar vacio");
                        return -1;
                    }
                    string query = "INSERT INTO the_king_of_fighters (Nombre, Apellido, Fecha_Nacimiento, Altura, Peso, Tipo_Sanguineo, Estilo_Pelea) VALUES (@Nombre, @Apellido, @Fecha_Nacimiento, @Altura, @Peso, @Tipo_Sanguineo, @Estilo_Pelea)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", Nombre);
                        command.Parameters.AddWithValue("@Apellido", Apellido);
                        command.Parameters.AddWithValue("@Fecha_Nacimiento", Fecha_Nacimiento);
                        command.Parameters.AddWithValue("@Altura", Altura);
                        command.Parameters.AddWithValue("@Peso", Peso);
                        command.Parameters.AddWithValue("@Tipo_Sanguineo", Tipo_Sanguineo);
                        command.Parameters.AddWithValue("@Estilo_Pelea",Estilo_Pelea);
                        return command.ExecuteNonQuery();

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

                    string sql = "DELETE FROM the_king_of_fighters WHERE ID = @ID";
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

        public int ActualizarMyConexyon(int ID, string Nombre, string Apellido, DateTime Fecha_Nacimiento, string Altura, string Peso, string Tipo_Sanguineo, string Estilo_Pelea)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                // Actualizar el registro en la base de datos
                try
                {
                    connection.Open();

                    string query = "UPDATE the_king_of_fighters SET  Nombre = @Nombre, Apellido = @Apellido, Fecha_Nacimiento = @Fecha_Nacimiento, Altura = @Altura, Peso = @Peso, Tipo_Sanguineo = @Tipo_Sanguineo, Estilo_Pelea = @Estilo_Pelea WHERE ID = @ID";

                    using (MySqlCommand commnd = new MySqlCommand(query, connection))
                    {
                        commnd.Parameters.AddWithValue("@Nombre", Nombre);
                        commnd.Parameters.AddWithValue("@Apellido", Apellido);
                        commnd.Parameters.AddWithValue("@Fecha_Nacimiento", Fecha_Nacimiento);
                        commnd.Parameters.AddWithValue("@Altura", Altura);
                        commnd.Parameters.AddWithValue("@Peso", Peso);
                        commnd.Parameters.AddWithValue("@Tipo_Sanguineo", Tipo_Sanguineo);
                        commnd.Parameters.AddWithValue("@Estilo_Pelea", Estilo_Pelea);
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




