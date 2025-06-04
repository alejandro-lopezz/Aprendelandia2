using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Disco
    {
        //atributos
        private int id;
        private string titulo;
        private DateTime fechaLanzamiento;
        private int cantidadCanciones;
        private string urlImagen;
        private Estilo estilo;
        private Tipo tipo;
        private int idEstilo;
        private int idTipoEdicion;

        //propiedades
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }
        [DisplayName("Fecha Lanzamiento")]
        public DateTime FechaLanzamiento
        {
            get { return fechaLanzamiento; }
            set { fechaLanzamiento = value; }
        }
        [DisplayName("Cantidad Canciones")]
        public int CantidadCanciones
        {
            get { return cantidadCanciones; }
            set { cantidadCanciones = value; }
        }
        public string UrlImagen
        {
            get { return urlImagen; }
            set { urlImagen = value; }
        }
        public Estilo Estilo
        {
            get { return estilo; }
            set { estilo = value; }
        }
        public Tipo Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public int IdEstilo
        {
            get { return idEstilo; }
            set { idEstilo = value; }
        }
        public int IdTipoEdicion
        {
            get { return idTipoEdicion; }
            set { idTipoEdicion = value; }
        }
    }
}
