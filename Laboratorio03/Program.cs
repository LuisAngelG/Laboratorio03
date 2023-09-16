﻿
//Librerias del ADO .NET
using System.Data.SqlClient;
using System.Data;

using Laboratorio03;
class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-25\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=123456";


    static void Main()
    {
        DataTable dt = new DataTable();

        foreach (DataRow row in dt.Rows)
        {
            int id = (int)row["idTrabajador"];
            string nombre = (string)row["Nombre"];
            string apellido = (string)row["Apellido"];
            decimal sueldo = (decimal)row["Sueldo"];
            DateTime fecha = (DateTime)row["Fecha_de_Nacimiento"];

            Console.WriteLine($"ID: {id}, Nombre: {nombre}, Apellido: {apellido}, Sueldo: {sueldo}, Fecha:{fecha}");

        }

        List<Trabajador> list = ListarTrabajadoresListaObjetos();
        foreach (var item in list)
        {
            Console.WriteLine(item.idTrabajador);
            Console.WriteLine(item.Nombre);
            Console.WriteLine(item.Apellido);
            Console.WriteLine(item.Sueldo);
            Console.WriteLine(item.Fecha_de_Nacimiento);

        }
    }

    //De forma desconectada
    private static DataTable ListarEmpleadosDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Trabajadores";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);


            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }

    //De forma conectada
    private static List<Trabajador> ListarTrabajadoresListaObjetos()
    {
        List<Trabajador> trabajadores = new List<Trabajador>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT idTrabajador, Nombre, Apellido, Sueldo, Fecha_de_Nacimiento FROM Trabajadores";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            trabajadores.Add(new Trabajador
                            {
                                idTrabajador = (int)reader["idTrabajador"],
                                Nombre = (string)reader["Nombre"],
                                Apellido = (string)reader["Apellido"],
                                Sueldo = (decimal)reader["Sueldo"],
                                Fecha_de_Nacimiento = (DateTime)reader["Fecha_de_Nacimiento"]
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();


        }
        return trabajadores;

    }


}