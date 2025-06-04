using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using System.Data.SqlClient;

namespace negocio
{
    public class DiscoNegocio
    { 
        public List<Disco> listar()
        {
            List<Disco> lista = new List<Disco>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select D.Id ,D.Titulo, D.FechaLanzamiento, D.CantidadCanciones, D.UrlImagenTapa, E.Descripcion as Estilo, T.Descripcion as Tipo, D.IdEstilo, D.IdTipoEdicion From DISCOS D, ESTILOS E, TIPOSEDICION T where D.IdEstilo = E.Id and D.IdTipoEdicion = T.Id";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)lector["Id"];
                    aux.Titulo = (string)lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)lector["CantidadCanciones"];
                    if (!(lector["UrlImagenTapa"] is DBNull))
                    {
                        aux.UrlImagen = (string)lector["UrlImagenTapa"];
                    }
                    aux.Estilo = new Estilo();
                    aux.Estilo.Descripcion = (string)lector["Estilo"];
                    aux.Tipo = new Tipo();
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.IdEstilo = (int)lector["IdEstilo"];
                    aux.IdTipoEdicion = (int)lector["IdTipoEdicion"];
                    
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
  
        }

        public void agregar(Disco nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into Discos (Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, IdEstilo, IdTipoEdicion) values (@titulo, @fechaLanzamiento, @cantidadCanciones, @urlImagen, @idEstilo, @idTipo)");
                datos.setearParametros("@titulo", nuevo.Titulo);
                datos.setearParametros("@fechaLanzamiento", nuevo.FechaLanzamiento);
                datos.setearParametros("@cantidadCanciones", nuevo.CantidadCanciones);
                datos.setearParametros("@urlImagen", nuevo.UrlImagen);
                datos.setearParametros("@idEstilo", nuevo.Estilo.Id);
                datos.setearParametros("@idTipo", nuevo.Tipo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConeccion();
            }
        }

        public void modificar(Disco modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update Discos set Titulo = @titulo, FechaLanzamiento = @fechaLanzamiento, CantidadCanciones = @cantidadCanciones, UrlImagenTapa = @urlImagen, IdEstilo = @idEstilo, IdTipoEdicion = @idTipo where Id = @id");
                datos.setearParametros("@titulo", modificado.Titulo);
                datos.setearParametros("@fechaLanzamiento", modificado.FechaLanzamiento);
                datos.setearParametros("@cantidadCanciones", modificado.CantidadCanciones);
                datos.setearParametros("@urlImagen", modificado.UrlImagen);
                datos.setearParametros("@idEstilo", modificado.Estilo.Id);
                datos.setearParametros("@idTipo", modificado.Tipo.Id);
                datos.setearParametros("@id", modificado.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConeccion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from discos where Id = @id");
                datos.setearParametros("@id", id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConeccion();
            }
        }

        public List<Disco> filtrar(string campo, string criterio, string valor)
        {
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "Select D.Id ,D.Titulo, D.FechaLanzamiento, D.CantidadCanciones, D.UrlImagenTapa, E.Descripcion as Estilo, T.Descripcion as Tipo, D.IdEstilo, D.IdTipoEdicion From DISCOS D, ESTILOS E, TIPOSEDICION T where D.IdEstilo = E.Id and D.IdTipoEdicion = T.Id and ";

                if (campo == "Cantidad de Canciones")
                {
                    if (criterio == "Mayor a")
                    {
                        consulta += "D.CantidadCanciones > " + valor;
                    }
                    else if (criterio == "Menor a")
                    {
                        consulta += "D.CantidadCanciones < " + valor;
                    }
                    else
                    {
                        consulta += "D.CantidadCanciones = " + valor;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];
                    if (!(datos.Lector["UrlImagenTapa"] is DBNull))
                    {
                        aux.UrlImagen = (string)datos.Lector["UrlImagenTapa"];
                    }
                    aux.Estilo = new Estilo();
                    aux.Estilo.Descripcion = (string)datos.Lector["Estilo"];
                    aux.Tipo = new Tipo();
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.IdEstilo = (int)datos.Lector["IdEstilo"];
                    aux.IdTipoEdicion = (int)datos.Lector["IdTipoEdicion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConeccion();
            }

        }
    }
}
