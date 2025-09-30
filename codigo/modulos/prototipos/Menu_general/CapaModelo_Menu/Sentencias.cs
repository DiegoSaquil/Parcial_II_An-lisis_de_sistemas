using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Odbc;

namespace CapaModelo_Menu
{
    public class Sentencia
    {
        private readonly Cls_ConexionBD _cxn = new Cls_ConexionBD();

        // READ
        public DataTable ObtenerEmpleados()
        {
            using (var conn = _cxn.conexion())
            using (var da = new OdbcDataAdapter(@"
                SELECT 
                    Pk_Id_Empleado,
                    Cmp_Nombres_Empleado,
                    Cmp_Apellidos_Empleado,
                    Cmp_Dpi_Empleado,
                    Cmp_Nit_Empleado,
                    Cmp_Correo_Empleado,
                    Cmp_Telefono_Empleado,
                    Cmp_Genero_Empleado,
                    Cmp_Fecha_Nacimiento_Empleado,
                    Cmp_Fecha_Contratacion__Empleado
                FROM empleado;", conn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // CREATE
        public int InsertarEmpleado(string codigo, string nombre, string puesto, string departamento, string estado)
        {
            using (var conn = _cxn.conexion())
            using (var cmd = new OdbcCommand(@"
                INSERT INTO empleados (Cmp_Nombres_Empleado, Cmp_Apellidos_Empleado, Cmp_Dpi_Empleado, Cmp_Nit_Empleado,
                     Cmp_Correo_Empleado, Cmp_Telefono_Empleado, Cmp_Genero_Empleado,
                     Cmp_Fecha_Nacimiento_Empleado, Cmp_Fecha_Contratacion__Empleado)
                VALUES (?,?,?,?,?,?,?,?,?);", conn))
            {
                cmd.Parameters.AddWithValue("@p1", codigo);
                cmd.Parameters.AddWithValue("@p2", nombre);
                cmd.Parameters.AddWithValue("@p3", puesto);
                cmd.Parameters.AddWithValue("@p4", departamento);
                cmd.Parameters.AddWithValue("@p5", estado);
                return cmd.ExecuteNonQuery();
            }
        }

        // UPDATE
        public int ActualizarEmpleado(string codigo, string nombre, string puesto, string departamento, string estado)
        {
            using (var conn = _cxn.conexion())
            using (var cmd = new OdbcCommand(@"
                UPDATE empleados
                   SET nombre_completo = ?, puesto = ?, departamento = ?, estado = ?
                 WHERE codigo_empleado = ?;", conn))
            {
                cmd.Parameters.AddWithValue("@p1", nombre);
                cmd.Parameters.AddWithValue("@p2", puesto);
                cmd.Parameters.AddWithValue("@p3", departamento);
                cmd.Parameters.AddWithValue("@p4", estado);
                cmd.Parameters.AddWithValue("@p5", codigo);
                return cmd.ExecuteNonQuery();
            }
        }

        // DELETE
        public int EliminarEmpleado(string codigo)
        {
            using (var conn = _cxn.conexion())
            using (var cmd = new OdbcCommand(
                "DELETE FROM empleados WHERE codigo_empleado = ?;", conn))
            {
                cmd.Parameters.AddWithValue("@p1", codigo);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
